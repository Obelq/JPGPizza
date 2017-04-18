using JPGPizza.Data;
using JPGPizza.Models;
using JPGPizza.MVC.Dtos;
using JPGPizza.MVC.Extensions;
using JPGPizza.MVC.Services;
using JPGPizza.MVC.Utility;
using JPGPizza.MVC.ViewModels.Ingredients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JPGPizza.MVC.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly IngredientsRepository _ingredientsRepository;

        public IngredientsController()
        {
            _context = new JPGPizzaDbContext();
            _ingredientsRepository = new IngredientsRepository(_context);
        }

        public async Task<ActionResult> Index(int? page, string searchText)
        {
            var totalIngredients = await _ingredientsRepository.GetTotalCountAsync(searchText);

            Pager pager = new Pager(totalIngredients, page);

            var ingredients = _ingredientsRepository.GetAll(searchText)
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize);

            var viewModel = new IngredientsIndexViewModel()
            {
                Ingredients = ingredients,
                Pager = pager,
                SearchText = searchText
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditIngredientViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!User.IsInRole("Administrator"))
            {
                return View("Error");
            }

            var ingredientEntity = _ingredientsRepository.GetById(viewModel.Id);

            ingredientEntity.Name = viewModel.Name;

            TryValidateModel(ingredientEntity);

            if (!ModelState.IsValid)
            {
                this.AddNotification("Грешка при запазване на промените: Името на съставката не е валидно.", NotificationType.ERROR);
                return RedirectToAction("Index", new { page = viewModel.CurrentPage, searchText = viewModel.SearchText });
            }

            _ingredientsRepository.Update(ingredientEntity);

            if (!_ingredientsRepository.Save())
            {
                this.AddNotification("Грешка при запазване на промените: Името на съставката не е валидно.", NotificationType.ERROR);
                return RedirectToAction("Index", new { page = viewModel.CurrentPage, searchText = viewModel.SearchText });
            }

            this.AddNotification("Съставката е променена.", NotificationType.SUCCESS);
            return RedirectToAction("Index", new { page = viewModel.CurrentPage, searchText = viewModel.SearchText});
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(DeleteIngredientViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ingredient = _ingredientsRepository.GetById((int)viewModel.Id);

            if (ingredient == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _ingredientsRepository.Remove(ingredient);

            if (!_ingredientsRepository.Save())
            {
                this.AddNotification("Грешка при опти за изтриване на съставката.", NotificationType.ERROR);
                return RedirectToAction("Index", new { page = viewModel.CurrentPage, searchText = viewModel.SearchText });
            }

            this.AddNotification("Съставката е изтрита.", NotificationType.SUCCESS);
            return RedirectToAction("Index", new { page = viewModel.CurrentPage, searchText = viewModel.SearchText });
        }

        // GET: Ingredients
        [HttpGet]
        public ActionResult GetIngredients()
        {
            var ingredients = _ingredientsRepository.GetAll()
                .Select(i => new IngredientDto
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .OrderBy(i => i.Name);

            return this.Json(ingredients, JsonRequestBehavior.AllowGet);
        }
    }
}