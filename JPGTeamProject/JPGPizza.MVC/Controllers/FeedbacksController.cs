namespace JPGPizza.MVC.Controllers
{
    using AutoMapper;
    using JPGPizza.Data;
    using JPGPizza.Models;
    using JPGPizza.MVC.Dtos;
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class FeedbacksController : Controller
    {
        private readonly JPGPizzaDbContext _context;

        public FeedbacksController()
        {
            _context = new JPGPizzaDbContext();
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

            var product = _context.Products.FirstOrDefault(p => p.Id == feedback.ProductId);

            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = _context.Users.FirstOrDefault(u => u.Id == feedback.CustomerId);

            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feedbackEntity = Mapper.Map<Feedback>(feedback);

            feedbackEntity.CreatedOn = DateTime.Now;
            _context.Feedbacks.Add(feedbackEntity);
            _context.SaveChanges();

            var feedbackToReturn = Mapper.Map<FeedbackDto>(feedbackEntity);
            feedbackToReturn.CustomerName = _context.Users.FirstOrDefault(u => u.Id == feedbackToReturn.CustomerId)?.UserName;
            feedbackToReturn.Date = feedbackEntity.CreatedOn.ToString();

            return this.Json(feedbackToReturn, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(FeedbackDto feedback)
        {
            if (feedback == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feedbackEntity = _context.Feedbacks.Find(feedback.Id);

            if (feedbackEntity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            feedbackEntity.Content = feedback.Content;
            feedbackEntity.CreatedOn = DateTime.Now;
            _context.Entry(feedbackEntity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var feedback = _context.Feedbacks.Find(id);

            if (feedback == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();

            return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
