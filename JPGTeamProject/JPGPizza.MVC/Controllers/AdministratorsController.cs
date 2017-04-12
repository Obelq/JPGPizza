namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.Data;
    using JPGPizza.MVC.ViewModels.Administrators;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class AdministratorsController : Controller
    {
        private readonly JPGPizzaDbContext _context;

        public AdministratorsController()
        {
            _context = new JPGPizzaDbContext();
        }

        [HttpGet()]
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.Select(u => new TopUserViewModel()
            {
                Name = u.FirstName + " " + u.LastName,
                TotalOrders = u.Orders.Count(),
                TotalCost = u.Orders.SelectMany(o => o.OrderProducts).Sum(op => op.Quantity * op.Product.Price)
            })
            .OrderByDescending(u => u.TotalCost)
            .ThenByDescending(u => u.TotalOrders)
            .ToListAsync();

            var feedbacks = await _context.Feedbacks
                .Include(f => f.Product)
                .Include(f => f.Customer)
                .OrderByDescending(f => f.CreatedOn)
                .Take(5)
                .ToListAsync();

            var viewModel = new AdministratorsIndexViewModel()
            {
                TopUsers = users,
                Feedbacks = feedbacks
            };

            return View(viewModel);
        }
    }
}