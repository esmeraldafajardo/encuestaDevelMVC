using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncuestasMVC.Models
{
    public class Encuesta
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string Token { get; set; }
        public virtual ICollection<Campo> Campos { get; set; }


    }
}
