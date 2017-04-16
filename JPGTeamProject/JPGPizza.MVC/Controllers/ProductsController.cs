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
using JPGPizza.MVC.Utility;

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

            productEntity.Image = ImageHelper.GetBytesFromFile(viewModel.Picture);

            if (productEntity.Image == null)
            {
                ModelState.AddModelError("Невалиден файл", "Невалиден файл: Може да качите само файлове от тип: jpg, jpeg, png, gif");
                return View(viewModel);
            }

            productEntity.ImageUrl = ImageHelper.GetImageUrl(productEntity.Image);

            AddIngredientsToProduct(productEntity, viewModel.Ingredients);

            _productsRepository.Add(productEntity);
            _productsRepository.SaveChanges();

            return RedirectToAction("Products", "Administrators");
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