using JPGPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPGPizza.MVC.ViewModels.Homepage
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}