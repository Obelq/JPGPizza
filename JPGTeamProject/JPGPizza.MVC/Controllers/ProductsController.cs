using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using JPGPizza.Data;
using JPGPizza.Models;
using JPGPizza.MVC.Services;
using JPGPizza.MVC.ViewModels.Products;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;

namespace JPGPizza.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly IngredientsRepository _ingredientsRepository;
        private readonly ProductsRepository _productsRepository;

        public ProductsController()
        {
            _context = new JPGPizzaDbContext();
            this._ingredientsRepository = new IngredientsRepository(_context);
            this._productsRepository = new ProductsRepository(_context);
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult List(ProductType category)
        {
            using (var context = new JPGPizzaDbContext())
            {
                var items = context.Products.Where(p => p.Type == category).Include("Ingredients").ToList();
                return View(items);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productEntity = new Product()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                Type = viewModel.Type
            };

            productEntity.Image = GetImageAsByteArray(viewModel.Picture);
            productEntity.ImageUrl = GetImageUrl(productEntity.Image);

            AddIngredientsToProduct(productEntity, viewModel.Ingredients);

            _productsRepository.Add(productEntity);
            _productsRepository.SaveChanges();

            return RedirectToAction("Products", "Administrators");
        }

        public byte[] GetImageAsByteArray(HttpPostedFileBase image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.InputStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public string GetImageUrl(byte[] imageAsByteArray)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(imageAsByteArray);
        }

        public void AddIngredientsToProduct(Product product, IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient == null || ingredient?.Name == null)
                {
                    continue;
                }

                var ingredientFromDb = _ingredientsRepository.GetByName(ingredient.Name);

                if (ingredientFromDb == null)
                {
                    ingredientFromDb = new Ingredient() { Name = ingredient.Name };
                }

                product.Ingredients.Add(ingredientFromDb);
            }
        }
    }
}