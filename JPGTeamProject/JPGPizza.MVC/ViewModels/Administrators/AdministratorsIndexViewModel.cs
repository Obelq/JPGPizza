namespace JPGPizza.MVC.ViewModels.Administrators
{
    using JPGPizza.Models;
    using System.Collections.Generic;

    public class AdministratorsIndexViewModel
    {
        public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
        public ICollection<TopUserViewModel> TopUsers { get; set; } = new HashSet<TopUserViewModel>();
        public int TotalUsersCount { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}