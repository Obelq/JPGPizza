namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class IngredientsRepository
    {
        private readonly JPGPizzaDbContext _context;

        public IngredientsRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCountAsync(string searchText = "")
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return await _context.Ingredinets.CountAsync();
            }
            else
            {
                return await _context.Ingredinets.Where(i => i.Name.Contains(searchText)).CountAsync();
            }
        }

        public IEnumerable<Ingredient> GetAll(string searchText = "")
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return _context.Ingredinets.OrderBy(i => i.Name);
            }
            else
            {
                return _context.Ingredinets.Where(i => i.Name.Contains(searchText)).OrderBy(i => i.Name);
            }
        }

        public Ingredient  GetByName(string name)
        {
            return _context.Ingredinets.FirstOrDefault(i => i.Name == name);
        }

        public Ingredient GetById(int id)
        {
            return _context.Ingredinets.Find(id);
        }

        public Ingredient AddByName(string name)
        {
            var ingredient = new Ingredient() { Name = name };
            _context.Ingredinets.Add(ingredient);

            return ingredient;
        }

        public void Update(Ingredient ingredient)
        {
            _context.Entry(ingredient).State = EntityState.Modified;
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