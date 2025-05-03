using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasMVC.Models
{
    [Table("Respuestas")]
    public class RespuestaCampo
    {
        public int Id { get; set; }

        public int CampoId { get; set; }

        [ForeignKey("CampoId")]
        public virtual Campo Campo { get; set; }


        public string Valor { get; set; }

        public string TokenEncuesta { get; set; }
    }
}
