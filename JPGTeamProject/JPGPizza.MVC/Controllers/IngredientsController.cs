using JPGPizza.Data;
using JPGPizza.MVC.Dtos;
using JPGPizza.MVC.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this._ingredientsRepository = new IngredientsRepository(_context);
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