﻿namespace JPGPizza.MVC.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.ViewModels.Products;
    using System.Net;
    using System.Collections.Generic;
    using JPGPizza.MVC.Utility;
    using JPGPizza.MVC.Extensions;

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
            var items = _productsRepository.GetByType(category);

            return View(items);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
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
                this.AddNotification("Невалиден файл: Може да качите само файлове от тип: jpg, jpeg, png, gif", NotificationType.ERROR);
                return View(viewModel);
            }

            productEntity.ImageUrl = ImageHelper.GetImageUrl(productEntity.Image);

            AddIngredientsToProduct(productEntity, viewModel.Ingredients);

            _productsRepository.Add(productEntity);
            _productsRepository.Save();

            this.AddNotification("Продуктът е създаден успешно.", NotificationType.SUCCESS);
            return RedirectToAction("Products", "Administrators");
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productsRepository.GetById((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new OrderProduct
            {
                Product = product,
                ProductId = product.Id,
                Quantity = 0
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var product = _productsRepository.GetById((int)id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditProductViewModel()
            {
                ProductId = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Ingredients = product.Ingredients.ToList(),
                Price = product.Price,
                Type = product.Type
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(EditProductViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (viewModel.ProductId == 0)
            {
                this.AddNotification("Грешка при промяна на продуктът. Пордуктът не беше намерен.", NotificationType.ERROR);
                return View(viewModel);
            }

            var productEntity = _productsRepository.GetById(viewModel.ProductId);

            if (productEntity == null)
            {
                this.AddNotification("Грешка при промяна на продуктът. Пордуктът не беше намерен.", NotificationType.ERROR);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productEntity.Name = viewModel.Name;
            productEntity.Price = viewModel.Price;
            productEntity.Type = viewModel.Type;

            if (viewModel.Image != null)
            {
                productEntity.Image = ImageHelper.GetBytesFromFile(viewModel.Image);
                productEntity.ImageUrl = ImageHelper.GetImageUrl(productEntity.Image);
            }

            viewModel.Ingredients = viewModel.Ingredients.Where(i => i != null).ToList();
            var productIngredientNamesToAdd = viewModel.Ingredients.Select(i => i.Name);
            productEntity.Ingredients = productEntity.Ingredients
                .Where(i => productIngredientNamesToAdd.Contains(i.Name)).ToList();

            var productExistingIngredients = productEntity.Ingredients.Select(i => i.Name);
            viewModel.Ingredients = viewModel.Ingredients
                .Where(i => i != null)
                .Where(i => !productExistingIngredients.Contains(i.Name))
                .ToList();

            AddIngredientsToProduct(productEntity, viewModel.Ingredients);
            _context.Entry(productEntity).State = EntityState.Modified;
            _context.SaveChanges();

            this.AddNotification("Продуктът е променен успешно.", NotificationType.SUCCESS);
            return RedirectToAction("Products", "Administrators");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productsRepository.GetById((int)id);

            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new DeleteProductViewModel()
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Ingredients = product.Ingredients.ToList(),
                Name = product.Name,
                Price = product.Price,
                Type = product.Type
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productsRepository.GetById((int)id);

            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            product.IsDeleted = true;

            if (!_productsRepository.Save())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.AddNotification("Продуктът е изтрит успешно.", NotificationType.SUCCESS);
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