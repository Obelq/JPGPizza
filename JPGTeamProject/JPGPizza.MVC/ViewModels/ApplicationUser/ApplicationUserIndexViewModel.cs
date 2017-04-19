namespace JPGPizza.MVC.ViewModels.ApplicationUser
{
    using JPGPizza.MVC.Utility;
    using Models;
    using System.Collections.Generic;

    public class ApplicationUserIndexViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
        public string SearchText { get; set; }
        public int? CurrentPage { get; set; }
        public Pager Pager { get; set; }
    }
}