using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JPGPizza.Data;
using JPGPizza.Models;

namespace JPGPizza.MVC.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Categories()
        {
            var categories = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            return View(categories);
        }

        public ActionResult List(ProductType category)
        {
            using (var context = new JPGPizzaDbContext())
            {
                var items = context.Products.Where(p => p.Type == category).Include("Ingredients").ToList();
                return View(items);
            }
        }
    }
}