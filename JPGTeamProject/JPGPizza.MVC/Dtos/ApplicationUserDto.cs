namespace JPGPizza.MVC.Dtos
{
    using JPGPizza.Models;

    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}