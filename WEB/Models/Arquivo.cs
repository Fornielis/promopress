using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class Arquivo
    {
        [Key]
        public int ArquivoId { get; set; }
        public string ClienteId { get; set; }

        [Required(ErrorMessage = "Informe o nome do Cliente")]
        [Display(Name = "CLIENTE")]
        public string ClienteNome { get; set; }

        [Required(ErrorMessage = "Descrição Obrigatória")]
        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        [NotMapped]
        public string Usuario { get; set; }

        public virtual List<ArquivoItem> ArquivoItems { get; set; }
        public virtual List<ArquivoOp> ArquivoOps { get; set; }
        public virtual List<ArquivoHistorico> ArquivoHistoricos { get; set; }
    }
}
