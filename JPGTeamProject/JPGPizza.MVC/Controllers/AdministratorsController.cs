namespace JPGPizza.MVC.Controllers
{
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.ViewModels.Administrators;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Data.Entity;
    using JPGPizza.Data;
    using System.Collections.Generic;
    using JPGPizza.Models;

    public class AdministratorsController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly AdministratorsRepository _administratorsRepository;
        private readonly ProductsRepository _productsRepository;

        public AdministratorsController()
        {
            _context = new JPGPizzaDbContext();
            _administratorsRepository = new AdministratorsRepository(_context);
            _productsRepository = new ProductsRepository(_context);
        }

        [Authorize(Roles = "Administrator")]
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


        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Products(string searchText)
        {
            var products = new List<Product>();

            if (!string.IsNullOrEmpty(searchText))
            {
                products = await _productsRepository
                    .GetAll()
                    .Include(p => p.Ingredients)
                    .Where(p => p.Name.Contains(searchText))
                    .ToListAsync();
            }
            else
            {
                products = await _productsRepository
                    .GetAll()
                    .Include(p => p.Ingredients)
                    .ToListAsync();
            }

            var viewModel = new AdministratorsProductsViewModel()
            {
                Products = products,
                SearchText = searchText
            };

            return View(viewModel);
        }
    }
}