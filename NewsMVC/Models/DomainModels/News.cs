//using Humanizer.Localisation;
using System.IO;

namespace NewsMVC.Models.DomainModels
{
    public class News
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public byte[]? image { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;

        [Required, FromNowToAfterOneWeek]
        public DateTime publication_date { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
