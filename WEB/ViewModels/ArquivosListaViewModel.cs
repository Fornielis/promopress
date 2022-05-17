using WEB.Models;

namespace WEB.ViewModels
{
    public class ArquivosListaViewModel
    {
        public string TipoPesquisa { get; set; }
        public int PromoId { get; set; }
        public string CodigoCliente { get; set; }
        public string Calculo { get; set; }
        public string Op { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Arquivo> Arquivos { get; set; }
        public IEnumerable<ArquivoOp> ArquivoOps { get; set; }
    }
}
