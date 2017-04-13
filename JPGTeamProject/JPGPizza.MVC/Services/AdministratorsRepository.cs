namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.MVC.Dtos;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdministratorsRepository
    {
        private readonly JPGPizzaDbContext _context;

        public AdministratorsRepository()
        {
            this._context = new JPGPizzaDbContext();
        }

        public IEnumerable<TopUserDto> GetTopCustomers()
        {
            var users = _context.Users
                .Select(u => new TopUserDto()
                {
                    UserName = u.UserName,
                    TotalOrders = u.Orders.Count(),
                    TotalCost = u.Orders.SelectMany(o => o.OrderProducts).Sum(op => op.Quantity * op.Product.Price)
                })
                .OrderByDescending(u => u.TotalCost)
                .ThenByDescending(u => u.TotalOrders)
                .Take(10);

            return users;
        }

        public IEnumerable<FeedbackForAdminDto> GetNewestFeedbacks()
        {
            var feedbacks = _context.Feedbacks
                .Select(f => new FeedbackForAdminDto()
                {
                    Id = f.Id,
                    Content = f.Content,
                    CreatedOn = f.CreatedOn,
                    ProductId = f.Product.Id,
                    ProductName = f.Product.Name,
                    CustomerId = f.CustomerId,
                    UserName = f.Customer.UserName
                })
                .OrderByDescending(f => f.CreatedOn)
                .Take(10);

            return feedbacks;
        }

        public IEnumerable<TopSellingProductDto> GetTopSellingProducts()
        {
            var products = _context.Products
                .Select(p => new TopSellingProductDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    TotalOrders = p.OrderProducts.Select(op => op.Order.Id).Distinct().Count(),
                    AverageFeedbacksRate = p.Feedbacks.Average(f => f.Rate)
                })
                .OrderByDescending(p => p.TotalOrders)
                .ThenByDescending(p => p.AverageFeedbacksRate)
                .Take(10);

            return products;
        }

        public IEnumerable<MostUsedIngredientDto> GetMostUsedIngredients()
        {
            var ingredients = _context.Ingredinets
                .Select(i => new MostUsedIngredientDto()
                {
                    Name = i.Name,
                    NumberOfProducts = i.Products.Count()
                })
                .OrderByDescending(i => i.NumberOfProducts)
                .Take(10);

            return ingredients;
        }

        public async Task<int> GetTotalCustomers()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalOrders()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetTotalFeedbacks()
        {
            return await _context.Feedbacks.CountAsync();
        }
    }
}