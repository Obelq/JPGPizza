namespace JPGPizza.MVC.Services
{
    using JPGPizza.Data;
    using JPGPizza.Models;
    using System.Linq;

    public class FeedbacksRepository
    {
        private readonly JPGPizzaDbContext _context;

        public FeedbacksRepository(JPGPizzaDbContext context)
        {
            _context = context;
        }

        public Feedback GetById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(f => f.Id == id);
        }

        public void Add(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
        }

        public void Remove(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}