namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.Data;
    using JPGPizza.MVC.ViewModels.Homepage;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {

        private readonly JPGPizzaDbContext _context;

        public HomeController()
        {
            _context = new JPGPizzaDbContext();
        }

        public ActionResult Index()
        {
            var products = _context.Products.Include("Ingredients")
                .Where(p => p.Type != Models.ProductType.Drink && p.IsDeleted == false)
                .OrderByDescending(i => i.Id)
                .Take(4);

            var feedbacks = _context.Feedbacks
                .OrderByDescending(f => f.CreatedOn)
                .Take(5);

            var model = new IndexViewModel()
            {
                Products = products,
                Feedbacks = feedbacks
            };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "JPGPizza";

            return View();
        }
    }
}