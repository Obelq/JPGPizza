namespace JPGPizza.MVC.ViewModels.ApplicationUser
{
    using JPGPizza.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditApplicationUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [MinLength(3), MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(50)]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }
    }
}