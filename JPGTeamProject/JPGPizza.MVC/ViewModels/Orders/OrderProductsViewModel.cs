using JPGPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPGPizza.MVC.ViewModels.Orders
{
    public class OrderProductsViewModel
    {
        public List<ProductType> Types { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public Product Product { get; set; }
    }
}