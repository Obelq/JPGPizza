namespace JPGPizza.MVC.ViewModels.Products
{
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;


    public class EditProductViewModel
    {
        public EditProductViewModel()
        {
        }

        public int ProductId { get; set; }

        [Required]
        [DisplayName("Име")]
        [MaxLength(50, ErrorMessage = "Името на продукта може да бъде по-малко или равно на 50 символа.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Тип")]
        public ProductType Type { get; set; }

        [Required]
        [DisplayName("Снимка")]
        public HttpPostedFileBase Image { get; set; }

        public string ImageUrl { get; set; }

        [DisplayName("Съставки")]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}