using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JPGPizza.Data;
using JPGPizza.Models;
using JPGPizza.MVC.ViewModels.Orders;
using JPGPizza.MVC.Services;
using JPGPizza.MVC.Dtos;

namespace JPGPizza.MVC.Controllers
{
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
        public ActionResult Delivery()
        {
            Console.WriteLine();
            return null;
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
            Console.WriteLine(orderProducts);
            Console.WriteLine();
            return null;
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _ordersRepository.GetById((int)id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date")] Order order)
        {
            if (ModelState.IsValid)
            {
                _ordersRepository.Add(order);

                if (!_ordersRepository.Save())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _ordersRepository.GetById((int)id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date")] Order order)
        {
            if (order == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orderEntity = _ordersRepository.GetById(order.Id);

            if (orderEntity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                _ordersRepository.Update(order);
                _ordersRepository.Save();

                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = _ordersRepository.GetById((int)id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = _ordersRepository.GetById((int)id);
            _ordersRepository.Remove(order);
            _ordersRepository.Save();

            return RedirectToAction("Index");
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
