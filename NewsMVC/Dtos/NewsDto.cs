//using Humanizer.Localisation;
using System.IO;

namespace NewsMVC.Dtos
{
    public class NewsDto : News
    {
        public IFormFile? image { get; set; }

    }
}
