using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class ArquivoItem
    {
        public int ArquivoItemId { get; set; }

        [Required(ErrorMessage = "Informe TIPO do arquivo")]
        [Display(Name = "TIPO ARQUIVO")]
        public string Tipo { get; set; }
        public int ChapaId { get; set; }

        [Required(ErrorMessage = "Informe quantidade chapas")]
        [Display(Name = "QUANTIDADE CHAPAS")]
        public int QtdChapa { get; set; }

        [Required(ErrorMessage = "Descrição Obrigatória")]
        [Display(Name = "DESCRIÇÃO")]
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string NomeArquivo { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Selecione um Arquivo")]
        public IFormFile IFormFile { get; set; }

        [NotMapped]
        public IFormFile IFormFileAlterar { get; set; }

        [NotMapped]
        public string URL { get; set; }

        [NotMapped]
        public string Usuario { get; set; }

        public int ArquivoId { get; set; }
        public virtual Arquivo Arquivo { get; set; }
    }
}
