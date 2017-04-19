namespace JPGPizza.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum Gender
    {
        [Display(Name = "Мъж")]
        Male,

        [Display(Name = "Жена")]
        Female
    }
}
