namespace JPGPizza.MVC.Utility
{
    using JPGPizza.Models;

    public static class ProductTypeHelper
    {
        public static string GetName(ProductType type)
        {
            switch (type)
            {
                case ProductType.Pizza:
                    return "Пици";
                case ProductType.Drink:
                    return "Напитки";
                case ProductType.Salad:
                    return "Салати";
                case ProductType.Sandwich:
                    return "Сандвичи";
                default:
                    return string.Empty;
            }
        }
    }
}