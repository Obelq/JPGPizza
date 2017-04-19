namespace JPGPizza.MVC.Dtos
{
    using System;

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public int NumberOfProducts { get; set; }
        public decimal TotalCost { get; set; }
    }
}