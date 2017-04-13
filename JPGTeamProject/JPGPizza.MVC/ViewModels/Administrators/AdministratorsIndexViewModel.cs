namespace JPGPizza.MVC.ViewModels.Administrators
{
    using JPGPizza.Models;
    using JPGPizza.MVC.Dtos;
    using System.Collections.Generic;
    using System.Web.Helpers;

    public class AdministratorsIndexViewModel
    {
        public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
        public IEnumerable<TopUserViewModel> TopUsers { get; set; } = new HashSet<TopUserViewModel>();
        public int TotalUsersCount { get; set; }
        public int TotalOrdersCount { get; set; }
        public IEnumerable<ProductByOrderDto> TopSellingProducts { get; set; } = new HashSet<ProductByOrderDto>();
    }
}