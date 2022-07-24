using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List.Domain.Entities
{
    public class Tarea : BaseEntity
    {
        [Required(ErrorMessage = "El Nombre de la Tarea es necesario")]
        [StringLength(50, ErrorMessage = "Debe contener minimo 5 caracteres", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR (50)")]
        public string Nombre { get; set; }        
        public bool Estado { get; set; } = true;
    }
}
