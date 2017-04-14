namespace JPGPizza.MVC.ViewModels.Administrators
{
    using JPGPizza.Models;
    using System.Collections.Generic;

    public class AdministratorsProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
    }
}