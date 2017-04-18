namespace JPGPizza.MVC.ViewModels.Ingredients
{
    public class DeleteIngredientViewModel
    {
        public int Id { get; set; }
        public string SearchText { get; set; }
        public int? CurrentPage { get; set; }
    }
}