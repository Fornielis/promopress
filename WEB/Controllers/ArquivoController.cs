using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;

namespace WEB.Controllers
{
    public class ArquivoController : Controller
    {
        private readonly IArquivoRepository _arquivoRepository;
        public ArquivoController(IArquivoRepository ArquivoRepository)
        {
            _arquivoRepository = ArquivoRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Lista()
        {
            ArquivosListaViewModel arquivosListaViewModel = _arquivoRepository.ListaArquivos();
            return View(arquivosListaViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Novo()
        {
            Arquivo arquivo = new Arquivo();
            ViewBag.ListaClientes = _arquivoRepository.ListaClientes();
            return View(arquivo);
        }
   
        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult Novo(Arquivo arquivo)
        {
            int idArquivoSalvo = _arquivoRepository.NovoArquivo(arquivo);
            return RedirectToAction("Detalhe", new { arquivoId = idArquivoSalvo });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Detalhe(int arquivoId)
        {
            ArquivosViewModel arquivosViewModel = _arquivoRepository.ArquivoDetalheId(arquivoId);
            return View(arquivosViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Editar(int arquivoId)
        {
            Arquivo arquivo = _arquivoRepository.ArquivoId(arquivoId);
            ViewBag.ListaClientes = _arquivoRepository.ListaClientes();
            return View(arquivo);
        }

        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult Editar(Arquivo arquivo)
        {
            _arquivoRepository.ArquivoEdita(arquivo);
            return RedirectToAction("Detalhe", new { arquivoId  = arquivo.ArquivoId});
        }

        [HttpGet]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult NovaOp(int arquivoId)
        {
            ArquivoOp arquivoOp = new ArquivoOp();
            arquivoOp.ArquivoId = arquivoId;
            return View(arquivoOp);
        }

        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult NovaOp(ArquivoOp arquivoOp)
        {
            _arquivoRepository.NovaOp(arquivoOp);
            return RedirectToAction("Detalhe", new { arquivoId =arquivoOp.ArquivoId});
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditarOp(int opId)
        {
            ArquivoOp arquivoOp = _arquivoRepository.OpId(opId);   
            return View(arquivoOp);
        }

        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult EditarOp(ArquivoOp arquivoOp)
        {
            _arquivoRepository.OpEdita(arquivoOp);
            return RedirectToAction("Detalhe", new { arquivoId = arquivoOp.ArquivoId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult DeleteOp(int opId, int arquivoId, string usuario)
        {
            _arquivoRepository.OpDelete(opId, arquivoId, usuario);
            return RedirectToAction("Detalhe", new { arquivoId = arquivoId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult NovoItem(int arquivoId)
        {
            ArquivoItem arquivoItem = new ArquivoItem
            {
                ArquivoId = arquivoId
            };

            ViewBag.ListaTipos = _arquivoRepository.ListaTipoArquivos();
            return View(arquivoItem);
        }

        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult NovoItem(ArquivoItem arquivoItem)
        {
            _arquivoRepository.NovoItem(arquivoItem);
            return RedirectToAction("Detalhe", new { arquivoId = arquivoItem.ArquivoId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditarIItem(int arquivoItemId)
        {
            ArquivoItem arquivoItem =  _arquivoRepository.ItemId(arquivoItemId);
            ViewBag.ListaTipos = _arquivoRepository.ListaTipoArquivos();
            return View(arquivoItem);
        }

        [HttpPost]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult EditarIItem(ArquivoItem arquivoItem)
        {
            _arquivoRepository.ItemEdita(arquivoItem);
            return RedirectToAction("Detalhe", new { arquivoId = arquivoItem.ArquivoId });
        }

        [HttpGet]
        [Authorize(Roles = "usu,adm,mst")]
        public IActionResult DeleteItem(int arquivoItemId, int arquivoId, string usuario)
        {
            _arquivoRepository.ItemDelete(arquivoItemId, arquivoId, usuario);
            return RedirectToAction("Detalhe", new { arquivoId = arquivoId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Pesquisa(ArquivosListaViewModel arquivosListaViewModel)
        {
            ArquivosListaViewModel retorno =  _arquivoRepository.PesquisaArquivo(arquivosListaViewModel);

            if (retorno.Arquivos.Count() == 0)
                ViewBag.Lista = false;
  
            return View("Lista", retorno);
        }
    }
}
