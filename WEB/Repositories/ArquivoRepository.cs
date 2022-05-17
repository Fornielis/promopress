using Microsoft.Extensions.Options;
using System.Linq;
using WEB.Context;
using WEB.Models;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;

namespace WEB.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly AppDbContext _context;
        private readonly Configuracao _configuracao;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ArquivoRepository(AppDbContext context, IOptions<Configuracao> configuracao, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _configuracao = configuracao.Value;
            _hostingEnvironment = hostingEnvironment;
        }
        public IEnumerable<ArquivoTipo> ListaTipoArquivos()
        {
            return _context.ArquivoTipos.OrderBy(t => t.Tipo);
        }
        public IEnumerable<Cliente> ListaClientes()
        {
            return _context.Clientes.OrderBy(c => c.Nome);
        }
        private void CriaHistorico(int arquivoId, string usuario, string atividade)
        {
            ArquivoHistorico arquivoHistorico = new ArquivoHistorico
            {
                ArquivoId = arquivoId,
                Usuario = usuario,
                Atividade = atividade,
                Data = DateTime.Now
            };

            _context.Add(arquivoHistorico);
            _context.SaveChanges();
        }
        private void CriarPasta(string nomePasta)
        {
            string caminho = Path.Combine(_hostingEnvironment.WebRootPath, _configuracao.Caminho, nomePasta);
            Directory.CreateDirectory(caminho);
        }
        private string SalvarArquivo(ArquivoItem arquivoItem)
        {
            string extensao = Path.GetExtension(arquivoItem.IFormFile.FileName);
            string nomeArquivo = string.Concat("PROMO-ID-", arquivoItem.ArquivoId.ToString(), "_", arquivoItem.Tipo, extensao);
            string caminho = Path.Combine(_hostingEnvironment.WebRootPath, _configuracao.Caminho, _configuracao.Prefixo + arquivoItem.ArquivoId.ToString(), nomeArquivo);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                arquivoItem.IFormFile.CopyTo(stream);
            }

            return nomeArquivo;
        }
        private int ObterIdChapa(string tipoChapa)
        {
            int retorno = 0;

            ArquivoChapaDiscartada arquivoChapaDiscartada = _context.ArquivoChapaDiscartadas.FirstOrDefault(tp => tp.Tipo == tipoChapa);

            if (arquivoChapaDiscartada == null)
            {
                ArquivoItem arquivoItem = _context.ArquivoItems
                                            .Where(tp => tp.Tipo == tipoChapa)
                                            .OrderBy(id => id.ChapaId)
                                            .LastOrDefault();

                if (arquivoItem != null)
                {
                    retorno = arquivoItem.ChapaId + 1;
                }
                else
                {
                    retorno = 1;
                }
            }
            else
            {
                retorno = arquivoChapaDiscartada.Posicao;
                _context.ArquivoChapaDiscartadas.Remove(arquivoChapaDiscartada);
                _context.SaveChanges();
            }

            return retorno;
        }
        public ArquivosListaViewModel ListaArquivos()
        {
            ArquivosListaViewModel arquivosListaViewModel = new ArquivosListaViewModel
            {
                Arquivos = (from arq in _context.Arquivos
                            orderby arq.ArquivoId descending
                            select arq).Take(15).ToList()
            };

            return arquivosListaViewModel;
        }
        public int NovoArquivo(Arquivo arquivo)
        {
            arquivo.Data = DateTime.Now;
            _context.Add(arquivo);
            _context.SaveChanges();

            CriarPasta(new string(_configuracao.Prefixo + arquivo.ArquivoId.ToString()));

            string atividade = string.Concat("Criou novo PROMO-ID ", arquivo.ArquivoId.ToString());
            CriaHistorico(arquivo.ArquivoId, arquivo.Usuario, atividade);

            return arquivo.ArquivoId;
        }
        public ArquivosViewModel ArquivoDetalheId(int arquivoId)
        {
            ArquivosViewModel arquivosViewModel = new ArquivosViewModel
            {
                Arquivo = _context.Arquivos.FirstOrDefault(a => a.ArquivoId == arquivoId),
                ArquivoOps = _context.ArquivoOps.Where(op => op.ArquivoId == arquivoId),

                ArquivoItens = _context.ArquivoItems
                                .Where(it => it.ArquivoId == arquivoId)
                                .OrderBy(tp => tp.Tipo)
                                .OrderBy(id => id.ChapaId),

                ArquivoHistoricos = _context.ArquivoHistoricos
                                    .Where(h => h.ArquivoId == arquivoId)
                                    .OrderBy(h => h.ArquivoHistoricoId)
            };

            return arquivosViewModel;
        }
        public Arquivo ArquivoId(int arquivoId)
        {
            Arquivo arquivo = _context.Arquivos.FirstOrDefault(a => a.ArquivoId == arquivoId);
            return arquivo;
        }
        public void ArquivoEdita(Arquivo arquivo)
        {
            arquivo.Data = DateTime.Now;
            _context.Update(arquivo);
            _context.SaveChanges();

            CriaHistorico(arquivo.ArquivoId, arquivo.Usuario, "Alterou informações de cadastro.");
        }
        public void NovaOp(ArquivoOp arquivoOp)
        {
            arquivoOp.Data = DateTime.Now;
            _context.Add(arquivoOp);
            _context.SaveChanges();

            string atividade = string.Concat("Inseriru histórico de OP-", arquivoOp.OP, " | ", arquivoOp.Calculo);
            CriaHistorico(arquivoOp.ArquivoId, arquivoOp.Usuario, atividade);
        }
        public ArquivoOp OpId(int arquivoOpId)
        {
            ArquivoOp arquivoOp = _context.ArquivoOps.FirstOrDefault(op => op.ArquivoOPId == arquivoOpId);
            return arquivoOp;
        }
        public void OpEdita(ArquivoOp arquivoOp)
        {
            arquivoOp.Data = DateTime.Now;
            _context.Update(arquivoOp);
            _context.SaveChanges();

            string atividade = string.Concat("Editou informações OP-", arquivoOp.OP, " | ", arquivoOp.Calculo);
            CriaHistorico(arquivoOp.ArquivoId, arquivoOp.Usuario, atividade);
        }
        public void OpDelete(int opId, int arquivoID, string usuario)
        {
            ArquivoOp op = OpId(opId);
            _context.ArquivoOps.Remove(op);
            _context.SaveChanges();

            string atividade = string.Concat("Deletou OP-", op.OP, " | ", op.Calculo);
            CriaHistorico(arquivoID, usuario, atividade);
        }
        public void NovoItem(ArquivoItem arquivoItem)
        {
            if (arquivoItem.Tipo.Contains("CHAPA"))
                arquivoItem.ChapaId = ObterIdChapa(arquivoItem.Tipo);

            arquivoItem.Data = DateTime.Now;
            _context.Add(arquivoItem);
            _context.SaveChanges();

            // INCLUI ID NOME DO ARQUIVO
            arquivoItem.NomeArquivo = SalvarArquivo(arquivoItem);
            _context.Update(arquivoItem);
            _context.SaveChanges();

            string atividade = string.Concat("Adicionou novo ARQUIVO-", arquivoItem.Tipo);
            CriaHistorico(arquivoItem.ArquivoId, arquivoItem.Usuario, atividade);
        }
        public ArquivoItem ItemId(int arquivoItemId)
        {
            ArquivoItem arquivoItem = _context.ArquivoItems.FirstOrDefault(it => it.ArquivoItemId == arquivoItemId);
            arquivoItem.URL = string.Concat(_configuracao.Caminho, "/", _configuracao.Prefixo + arquivoItem.ArquivoId.ToString(), "/", arquivoItem.NomeArquivo);
            return arquivoItem;
        }
        public void ItemEdita(ArquivoItem arquivoItem)
        {
            if (arquivoItem.IFormFileAlterar != null)
            {
                arquivoItem.IFormFile = arquivoItem.IFormFileAlterar;
                string arquivoDeletar = Path.Combine(_hostingEnvironment.WebRootPath, arquivoItem.URL);
                System.IO.File.Delete(Path.Combine(arquivoDeletar));

                arquivoItem.Data = DateTime.Now;
                _context.Update(arquivoItem);
                _context.SaveChanges();

                // INCLUI ID NOME DO ARQUIVO
                arquivoItem.NomeArquivo = SalvarArquivo(arquivoItem);
                _context.Update(arquivoItem);
                _context.SaveChanges();
            }
            else
            {
                arquivoItem.Data = DateTime.Now;
                _context.Update(arquivoItem);
                _context.SaveChanges();
            }

            string atividade = string.Concat("Alterou DADOS/ARQUIVO-", arquivoItem.Tipo);
            CriaHistorico(arquivoItem.ArquivoId, arquivoItem.Usuario, atividade);
        }
        public void ItemDelete(int arquivoItemId, int arquivoId, string usuario)
        {
            ArquivoItem arquivoItem = ItemId(arquivoItemId);

            if (arquivoItem.Tipo.Contains("CHAPA"))
            {
                ArquivoChapaDiscartada arquivoChapaDiscartada = new ArquivoChapaDiscartada
                {
                    Tipo = arquivoItem.Tipo,
                    Posicao = arquivoItem.ChapaId
                };

                _context.Add(arquivoChapaDiscartada);
            }

            string arquivoDeletar = Path.Combine(_hostingEnvironment.WebRootPath, arquivoItem.URL);
            System.IO.File.Delete(Path.Combine(arquivoDeletar));


            _context.ArquivoItems.Remove(arquivoItem);
            _context.SaveChanges();

            string atividade = string.Concat("Deletou ARQUIVO-", arquivoItem.Tipo);
            CriaHistorico(arquivoId, usuario, atividade);
        }
        public ArquivosListaViewModel PesquisaArquivo(ArquivosListaViewModel valorPesquisa)
        {
            ArquivosListaViewModel resultado = new ArquivosListaViewModel();

            switch (valorPesquisa.TipoPesquisa)
            {
                case "PromoId":
                    resultado.Arquivos = _context.Arquivos.Where(arq => arq.ArquivoId == valorPesquisa.PromoId);
                    break;

                case "CodigoCliente":
                    resultado.Arquivos = _context.Arquivos.Where(arq => arq.ClienteId.Contains(valorPesquisa.CodigoCliente));
                    break;

                case "Calculo":
                    resultado.Arquivos = (from arq in _context.Arquivos
                                          join op in _context.ArquivoOps on arq.ArquivoId equals op.ArquivoId
                                          where op.Calculo == valorPesquisa.Calculo
                                          select arq).ToList();
                    break;

                case "Op":
                    resultado.Arquivos = (from arq in _context.Arquivos
                                          join op in _context.ArquivoOps on arq.ArquivoId equals op.ArquivoId
                                          where op.OP == valorPesquisa.Op
                                          select arq).ToList();
                    break;
            }

            return resultado;
        }
    }
}

