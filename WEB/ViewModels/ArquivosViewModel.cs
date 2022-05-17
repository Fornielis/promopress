using WEB.Models;

namespace WEB.ViewModels
{
    public class ArquivosViewModel
    {
        public Arquivo Arquivo { get; set; }
        public IEnumerable<Arquivo> Arquivos { get; set; }
        public IEnumerable<ArquivoItem> ArquivoItens { get; set; }
        public IEnumerable<ArquivoOp> ArquivoOps { get; set; }
        public IEnumerable<ArquivoHistorico> ArquivoHistoricos { get; set; }
    }
}
