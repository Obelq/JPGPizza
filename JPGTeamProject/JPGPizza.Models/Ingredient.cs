namespace JPGPizza.Models
{
    using System.Collections.Generic;

    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
