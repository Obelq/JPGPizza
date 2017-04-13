namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.ViewModels.Administrators;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class AdministratorsController : Controller
    {
        private readonly AdministratorsRepository _administratorsRepository;

        public AdministratorsController()
        {
            _administratorsRepository = new AdministratorsRepository();
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
    }
}