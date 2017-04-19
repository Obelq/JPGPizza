namespace JPGPizza.MVC.ViewModels.ApplicationUser
{
    using JPGPizza.MVC.Dtos;
    using JPGPizza.MVC.Utility;
    using Models;
    using System.Collections.Generic;

    public class ApplicationUserIndexViewModel
    {
        public IEnumerable<ApplicationUserDto> Users { get; set; } = new HashSet<ApplicationUserDto>();
        public string SearchText { get; set; }
        public int? CurrentPage { get; set; }
        public Pager Pager { get; set; }
    }
}