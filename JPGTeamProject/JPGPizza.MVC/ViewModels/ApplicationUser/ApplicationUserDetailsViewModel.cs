namespace JPGPizza.MVC.ViewModels.ApplicationUser
{
    using JPGPizza.MVC.Dtos;

    public class ApplicationUserDetailsViewModel
    {
        public ApplicationUserDto User { get; set; }
        public int TotalOrders { get; set; }
        public decimal OrdersTotalCost { get; set; }
    }
}