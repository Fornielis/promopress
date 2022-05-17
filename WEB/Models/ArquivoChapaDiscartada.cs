using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class ArquivoChapaDiscartada
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Posicao { get; set; }
    }
}
