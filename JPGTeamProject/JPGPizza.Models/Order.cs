namespace JPGPizza.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string CustomerId { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

        public virtual ApplicationUser Customer { get; set; }
    }
}
