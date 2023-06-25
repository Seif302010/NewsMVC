namespace NewsMVC.Models.DomainModels.AuthorizationModels
{
    public class Login
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }
    }
}
