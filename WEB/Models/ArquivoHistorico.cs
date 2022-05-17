namespace WEB.Models
{
    public class ArquivoHistorico
    {
        public int ArquivoHistoricoId { get; set; }
        public string Usuario { get; set; }
        public string Atividade { get; set; }
        public DateTime Data { get; set; }

        public int ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}
