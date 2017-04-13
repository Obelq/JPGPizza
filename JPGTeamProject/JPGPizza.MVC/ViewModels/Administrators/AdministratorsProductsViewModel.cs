using JPGPizza.Models;
using System.Collections.Generic;

namespace JPGPizza.MVC.ViewModels.Administrators
{
    public class AdministratorsProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
    }
}