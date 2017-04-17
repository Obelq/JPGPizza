namespace JPGPizza.MVC.ViewModels.Administrators
{
    using JPGPizza.Models;
    using System.Collections.Generic;

    public class AdministratorsProductsViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
    }
}