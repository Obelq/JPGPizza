namespace JPGPizza.MVC.ViewModels.Homepage
{
    using JPGPizza.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}