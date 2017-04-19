namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using System.Collections.Generic;
    using JPGPizza.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersRepository
    {
        private readonly JPGPizzaDbContext _context;

        public OrdersRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderProducts.Select(op => op.Product))
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders.AsQueryable();
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
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