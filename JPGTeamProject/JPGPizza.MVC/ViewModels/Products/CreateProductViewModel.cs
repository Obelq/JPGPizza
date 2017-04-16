namespace JPGPizza.MVC.ViewModels.Products
{
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CreateProductViewModel
    {
        [Required]
        [DisplayName("Име")]
        [MaxLength(50, ErrorMessage = "Името на продукта може да бъде по-малко или равно на 50 символа.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Тип")]
        public ProductType Type { get; set; }

        [DisplayName("Снимка")]
        public HttpPostedFileBase Picture { get; set; }

        [DisplayName("Съставки")]
        public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}