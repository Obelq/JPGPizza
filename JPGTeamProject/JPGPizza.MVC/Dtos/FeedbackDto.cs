namespace JPGPizza.MVC.Dtos
{
    using System;

    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
    }
}