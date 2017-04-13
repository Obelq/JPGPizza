namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsRepository
    {
        private readonly JPGPizzaDbContext _context;

        public ProductsRepository()
        {
            _context = new JPGPizzaDbContext();
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.OrderBy(p => p.Name);
        }
    }
}