using WEB.Models;
using WEB.ViewModels;

namespace WEB.Repositories.Interfaces
{
    public interface IArquivoRepository
    {
        IEnumerable<ArquivoTipo> ListaTipoArquivos();
        IEnumerable<Cliente> ListaClientes();
        ArquivosListaViewModel ListaArquivos();
        int NovoArquivo(Arquivo arquivo);
        ArquivosViewModel ArquivoDetalheId(int arquivoId);
        Arquivo ArquivoId(int arquivoId);
        void ArquivoEdita(Arquivo arquivo);
        void NovaOp(ArquivoOp arquivoOp);
        ArquivoOp OpId(int arquivoOpId);
        void OpEdita(ArquivoOp arquivoOp);
        void OpDelete(int opId, int arquivoID, string usuario);
        void NovoItem(ArquivoItem arquivoItem);
        ArquivoItem ItemId(int arquivoItemId);
        void ItemEdita(ArquivoItem arquivoItem);
        void ItemDelete(int arquivoItemId, int arquivoId, string usuario);
        ArquivosListaViewModel PesquisaArquivo(ArquivosListaViewModel arquivosListaViewModel);
    }
}
 