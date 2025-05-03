using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasMVC.Models
{
    [Table("Campos")]
    public class Campo
    {
        public int Id { get; set; }

        [Required]
        public string NombreCampo { get; set; }

        public string Titulo { get; set; }

        public bool EsRequerido { get; set; }

        [Display(Name = "Tipo de Campo")]
        public string TipoCampo { get; set; } // Texto, Número, Fecha

        // Clave foránea a Encuesta
        [ForeignKey("Encuesta")]
        public int EncuestaId { get; set; }

        public virtual Encuesta Encuesta { get; set; }
    }
}
