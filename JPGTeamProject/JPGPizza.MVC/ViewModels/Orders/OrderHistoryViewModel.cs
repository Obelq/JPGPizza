namespace JPGPizza.MVC.ViewModels.Orders
{
    using JPGPizza.MVC.Dtos;
    using JPGPizza.MVC.Utility;
    using System.Collections.Generic;

    public class OrderHistoryViewModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }
        public Pager Pager { get; set; }
        public string CustomerId { get; set; }
    }
}