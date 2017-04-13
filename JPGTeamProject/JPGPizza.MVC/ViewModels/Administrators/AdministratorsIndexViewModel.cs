namespace JPGPizza.MVC.ViewModels.Administrators
{
    using JPGPizza.MVC.Dtos;
    using System.Collections.Generic;

    public class AdministratorsIndexViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalFeedbacks { get; set; }
        public IEnumerable<MostUsedIngredientDto> MostUsedIngredients { get; set; } = new HashSet<MostUsedIngredientDto>();
        public IEnumerable<FeedbackForAdminDto> Feedbacks { get; set; } = new HashSet<FeedbackForAdminDto>();
        public IEnumerable<TopUserDto> TopUsers { get; set; } = new HashSet<TopUserDto>();
        public IEnumerable<TopSellingProductDto> TopSellingProducts { get; set; } = new HashSet<TopSellingProductDto>();
    }
}