﻿namespace JPGPizza.MVC.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.ViewModels.ApplicationUser;
    using JPGPizza.MVC.Services;
    using JPGPizza.MVC.Utility;
    using System.Linq;
    using AutoMapper;
    using JPGPizza.MVC.Dtos;
    using Microsoft.AspNet.Identity;
    using JPGPizza.MVC.ViewModels.Orders;

    [Authorize]
    public class ApplicationUsersController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly ApplicationUsersRepository _applicationUserRepository;
        private readonly OrdersRepository _ordersRepository;

        public ApplicationUsersController()
        {
            _context = new JPGPizzaDbContext();
            _applicationUserRepository = new ApplicationUsersRepository(_context);
            _ordersRepository = new OrdersRepository(_context);
        }

        // GET: ApplicationUsers
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index(string searchText, int? page)
        {
            var totalUsers = await _applicationUserRepository.GetTotalCountAsync();

            Pager pager = new Pager(totalUsers, page);

            var users = _applicationUserRepository.GetAll(searchText)
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize)
                .Select(u => new ApplicationUserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                });

            var viewModel = new ApplicationUserIndexViewModel()
            {
                Users = users,
                Pager = pager,
                SearchText = searchText
            };

            return View(viewModel);
        }

        // GET: ApplicationUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = await _applicationUserRepository.GetByIdAsync(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ApplicationUserDetailsViewModel()
            {
                User = Mapper.Map<ApplicationUserDto>(applicationUser),
                TotalOrders = await _applicationUserRepository.GetToalOrdersByIdAsync(applicationUser.Id),
                OrdersTotalCost = await _applicationUserRepository.GetTotalOrdersCostByIdAsync(applicationUser.Id)
            };

            return View(viewModel);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = _applicationUserRepository.GetById(id);

            if (applicationUser.Id != User.Identity.GetUserId() && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<EditApplicationUserViewModel>(applicationUser);

            return View(viewModel);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,Age,Gender,PhoneNumber")]
            EditApplicationUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userEntity = _applicationUserRepository.GetById(user.Id);

                if (userEntity.Id != User.Identity.GetUserId() && !User.IsInRole("Administrator"))
                {
                    return RedirectToAction("Index", "Home");
                }

                Mapper.Map(user, userEntity);

                _applicationUserRepository.Update(userEntity);

                if (!_applicationUserRepository.Save())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Details", "ApplicationUsers", new { id = userEntity.Id });
            }

            return View(user);
        }

        // GET: ApplicationUsers/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = _applicationUserRepository.GetById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            if (applicationUser.Id != User.Identity.GetUserId() && !User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = Mapper.Map<DeleteApplicationUserViewModel>(applicationUser);

            return View(viewModel);
        }

        public async Task<ActionResult> Orders(string id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var totalOrders = await _ordersRepository.GetAll()
                   .Where(o => o.CustomerId == id)
                   .OrderByDescending(o => o.Date)
                   .CountAsync();

            var pager = new Pager(totalOrders, page, 10);

            var orders = await _ordersRepository.GetAll()
                .Select(o => new OrderDto()
                {
                    Id = o.Id,
                    Date = o.Date,
                    CustomerId = o.CustomerId,
                    TotalCost = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price),
                    NumberOfProducts = o.OrderProducts.Count()
                })
                .Where(o => o.CustomerId == id)
                .OrderBy(o => o.Date)
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize)
                .ToListAsync();

            var viewModel = new OrderHistoryViewModel()
            {
                Orders = orders,
                CustomerId = id,
                Pager = pager
            };

            return View(viewModel);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = _applicationUserRepository.GetById(id);

            _applicationUserRepository.Remove(user);

            if (!_applicationUserRepository.Save())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
