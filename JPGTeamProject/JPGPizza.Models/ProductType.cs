namespace JPGPizza.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum ProductType
    {
        [Display(Name = "Пици")]
        Pizza,

        [Display(Name = "Напитки")]
        Drink,

        [Display(Name = "Салати")]
        Salad,

        [Display(Name = "Сандвичи")]
        Sandwich
    }
}
