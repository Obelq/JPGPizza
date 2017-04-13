namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.ViewModels.Administrators;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Data.Entity;

    public class AdministratorsController : Controller
    {
        private readonly AdministratorsRepository _administratorsRepository;
        private readonly ProductsRepository _productsRepository;

        public AdministratorsController()
        {
            _administratorsRepository = new AdministratorsRepository();
            _productsRepository = new ProductsRepository();
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = new AdministratorsIndexViewModel()
            {
                TopUsers = _administratorsRepository.GetTopCustomers(),
                Feedbacks = _administratorsRepository.GetNewestFeedbacks(),
                TopSellingProducts = _administratorsRepository.GetTopSellingProducts(),
                MostUsedIngredients = _administratorsRepository.GetMostUsedIngredients(),
                TotalOrders = await _administratorsRepository.GetTotalOrders(),
                TotalCustomers = await _administratorsRepository.GetTotalCustomers(),
                TotalFeedbacks = await _administratorsRepository.GetTotalFeedbacks()
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Products()
        {
            var viewModel = new AdministratorsProductsViewModel()
            {
                Products = await _productsRepository.GetAll().ToListAsync()
            };

            return View(viewModel);
        }
    }
}