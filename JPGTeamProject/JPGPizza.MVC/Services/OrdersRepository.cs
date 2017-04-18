namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using System.Collections.Generic;
    using JPGPizza.Models;
    using System.Data.Entity;

    public class OrdersRepository
    {
        private readonly JPGPizzaDbContext _context;

        public OrdersRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public Order GetById(int id)
        {
            return _context.Orders.Find(id);
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