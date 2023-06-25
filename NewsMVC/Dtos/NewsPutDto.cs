//using Humanizer.Localisation;
using System.IO;

namespace NewsMVC.Dtos
{
    public class NewsPutDto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }
        public string? Content { get; set; }

        [FromNowToAfterOneWeek]
        public DateTime? publication_date { get; set; }
        public IFormFile? image { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
