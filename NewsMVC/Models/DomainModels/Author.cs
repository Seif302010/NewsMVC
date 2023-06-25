namespace NewsMVC.Models.DomainModels
{
    public class Author
    {
        public int Id { get; set; }

        [Required, StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}
