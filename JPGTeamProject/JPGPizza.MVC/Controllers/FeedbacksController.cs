namespace JPGPizza.MVC.Controllers
{
    using AutoMapper;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.Dtos;
    using JPGPizza.MVC.Services;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class FeedbacksController : Controller
    {
        private readonly JPGPizzaDbContext _context;
        private readonly FeedbacksRepository _feedbacksRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly ApplicationUsersRepository _applicationUsersRepository;

        public FeedbacksController()
        {
            _context = new JPGPizzaDbContext();
            _feedbacksRepository = new FeedbacksRepository(_context);
            _productsRepository = new ProductsRepository(_context);
            _applicationUsersRepository = new ApplicationUsersRepository(_context);
        }

        // GET: Feedbacks
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(FeedbackDto feedback)
        {
            if (feedback == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _productsRepository.GetById(feedback.ProductId);

            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = _applicationUsersRepository.GetById(feedback.CustomerId);

            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feedbackEntity = Mapper.Map<Feedback>(feedback);
            feedbackEntity.CreatedOn = DateTime.Now;

            _feedbacksRepository.Add(feedbackEntity);
            _feedbacksRepository.Save();

            var feedbackToReturn = Mapper.Map<FeedbackDto>(feedbackEntity);
            feedbackToReturn.CustomerName = User.Identity.Name;
            feedbackToReturn.Date = feedbackEntity.CreatedOn.ToString();

            return this.Json(feedbackToReturn, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feedback = _feedbacksRepository.GetById((int)id);

            if (feedback == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!User.IsInRole("Administrator") && User.Identity.GetUserId() != feedback.CustomerId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _feedbacksRepository.Remove(feedback);
            _feedbacksRepository.Save();

            return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
