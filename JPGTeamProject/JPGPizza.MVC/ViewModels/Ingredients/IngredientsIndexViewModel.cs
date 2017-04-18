namespace JPGPizza.MVC.ViewModels.Ingredients
{
    using JPGPizza.Models;
    using JPGPizza.MVC.Utility;
    using System.Collections.Generic;

    public class IngredientsIndexViewModel
    {
        public string SearchText { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    }
}