namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class IngredientsRepository
    {
        private readonly JPGPizzaDbContext _context;

        public IngredientsRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredinets;
        }

        public Ingredient  GetByName(string name)
        {
            return _context.Ingredinets.FirstOrDefault(i => i.Name == name);
        }

        public Ingredient AddByName(string name)
        {
            var ingredient = new Ingredient() { Name = name };
            _context.Ingredinets.Add(ingredient);

            return ingredient;
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}