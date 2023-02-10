using System.ComponentModel.DataAnnotations;

namespace AlfredoService.Models
{
    public enum PizzaSize { Small, Medium, Large }
    public class Pizza
    {
        public int Id { get; set; }

        [Required] //attributo per indicare che i campi nel form html devono essere richiesti
        public string? Name { get; set; }
        public PizzaSize Size { get; set; }
        public bool IsGlutenFree { get; set; }

        [Range(0.01, 9999.99)]
        public decimal Price { get; set; }

    }
}

