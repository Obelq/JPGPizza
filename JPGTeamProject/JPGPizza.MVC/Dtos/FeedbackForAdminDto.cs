using System;

namespace JPGPizza.MVC.Dtos
{
    public class FeedbackForAdminDto
    {
        public string CustomerId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public TimeSpan TimeSpan
        {
            get
            {
                return DateTime.Now - this.CreatedOn;
            }
        }
    }
}