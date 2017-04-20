namespace JPGPizza.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.ViewModels.Orders;
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.Dtos;
    using System.Threading.Tasks;

    public class OrdersController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly ProductsRepository _productsRepository;
        private readonly OrdersRepository _ordersRepository;

        public OrdersController()
        {
            _context = new JPGPizzaDbContext();
            _productsRepository = new ProductsRepository(_context);
            _ordersRepository = new OrdersRepository(_context);
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View(_ordersRepository.GetAll());
        }

        public ActionResult CarryOut()
        {
            var products = _productsRepository.GetAll().ToList();
            var types = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().ToList();
            var model = new OrderProductsViewModel()
            {
                Types = types,
                Products = products
            };

            return View("CarryOut", model);
        }

        [HttpPost]
        public ActionResult ShopingCartList(List<OrderProductDto> orderProducts)
        {
            if (orderProducts == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = new Order()
            {
                Date = DateTime.Now,
                OrderProducts = orderProducts.Select(op => new OrderProduct()
                {
                    ProductId = op.id,
                    Quantity = op.quantity
                })
                .ToList()
            };

            user.Orders.Add(order);

            _context.SaveChanges();

            return this.Json(order.Id, JsonRequestBehavior.AllowGet);
        }


        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await _ordersRepository.GetByIdAsync((int)id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
