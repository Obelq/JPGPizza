namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationUsersRepository
    {
        private readonly JPGPizzaDbContext _context;

        public ApplicationUsersRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll(string searchText)
        {
            IEnumerable<ApplicationUser> users;

            if (string.IsNullOrEmpty(searchText))
            {
                users = _context.Users.OrderBy(u => u.UserName);
            }
            else
            {
                users = _context.Users
                    .OrderBy(u => u.UserName)
                    .Where(u => 
                        u.UserName.Contains(searchText) ||
                        u.FirstName.Contains(searchText) ||
                        u.LastName.Contains(searchText) ||
                        u.Email.Contains(searchText));
            }

            return users;
        }

        public async Task<int> GetToalOrdersByIdAsync(string id)
        {
            return await _context.Orders.CountAsync(o => o.CustomerId == id);
        }

        public async Task<decimal> GetTotalOrdersCostByIdAsync(string id)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == id)
                .Select(o => o.OrderProducts.Sum(op => op.Quantity * op.Product.Price))
                .SumAsync();
        }

        public void Update(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public void Remove(ApplicationUser user)
        {
            var ordersForDelete = _context.Orders.Where(o => o.CustomerId == user.Id).ToList();
            _context.Orders.RemoveRange(ordersForDelete);
            _context.Users.Remove(user);
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