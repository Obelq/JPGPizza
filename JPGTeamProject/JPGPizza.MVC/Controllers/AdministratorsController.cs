namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.Data;
    using JPGPizza.MVC.Dtos;
    using JPGPizza.MVC.ViewModels.Administrators;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Helpers;
    using System.Web.Mvc;

    public class AdministratorsController : Controller
    {
        private readonly JPGPizzaDbContext _context;

        public AdministratorsController()
        {
            _context = new JPGPizzaDbContext();
        }

        [HttpGet()]
        public ActionResult Index()
        {
            var users = _context.Users
                .Select(u => new TopUserViewModel()
                {
                    UserName = u.UserName,
                    TotalOrders = u.Orders.Count(),
                    TotalCost = u.Orders.SelectMany(o => o.OrderProducts).Sum(op => op.Quantity * op.Product.Price)
                })
                .OrderByDescending(u => u.TotalCost)
                .ThenByDescending(u => u.TotalOrders);

            var feedbacks = _context.Feedbacks
                .Include(f => f.Product)
                .Include(f => f.Customer)
                .OrderByDescending(f => f.CreatedOn)
                .Take(5);

            var topSellingProducts = _context.Products
                .Select(p => new ProductByOrderDto()
                {
                    Name = p.Name,
                    NumberOfOrders = p.OrderProducts.Count()
                });

            var viewModel = new AdministratorsIndexViewModel()
            {
                TopUsers = users,
                Feedbacks = feedbacks,
                TopSellingProducts = topSellingProducts
            };

            return View(viewModel);
        }
    }
}