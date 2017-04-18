namespace JPGPizza.MVC.Dtos
{
    public class TopSellingProductDto
    {
        private int? totalOrders;
        public decimal Price { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int? TotalOrders
        {
            get
            {
                return this.totalOrders == null ? 0 : this.totalOrders;
            }

            set
            {
                this.totalOrders = value;
            }
        }
    }
}