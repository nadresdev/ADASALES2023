using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Country
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre de Pais es obligatorio")]
        [MaxLength(20)]
        [Display(Name = "País")]
        public string Name { get; set; } = null!;
    }
}
