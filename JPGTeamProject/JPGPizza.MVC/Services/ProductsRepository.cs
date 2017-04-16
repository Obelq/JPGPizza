namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ProductsRepository
    {
        private readonly JPGPizzaDbContext _context;

        public ProductsRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.OrderBy(p => p.Name);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public Product GetById(int productId)
        {
            return _context.Products.Include(p => p.Ingredients).FirstOrDefault(p => p.Id == productId);
        }

        public bool SaveChanges()
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