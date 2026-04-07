using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaRossmonApi.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Precio")]
        [Range(0, 100000000, ErrorMessage = "Precio inválido")]
        public decimal Price { get; set; }

        [Range(0, 1000, ErrorMessage = "Stock inválido")]
        public int Stock { get; set; }
    }
}
