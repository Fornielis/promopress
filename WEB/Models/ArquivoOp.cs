using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class ArquivoOp
    {
        public int ArquivoOPId { get; set; }

        [Required(ErrorMessage = "Informe número da OP")]
        [Display(Name = "NÚMERO DA OP")]
        public string OP { get; set; }

        [Required(ErrorMessage = "Informe número Calculo")]
        [Display(Name = "CALCULO")]
        public string Calculo { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade")]
        [Display(Name = "QUANTIDADE")]
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }

        [NotMapped]
        public string Usuario { get; set; }

        public int ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}
