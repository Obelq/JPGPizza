namespace JPGPizza.MVC.ViewModels.Products
{
    using JPGPizza.Models;
    using System.Collections.Generic;

    public class DeleteProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public string ImageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}