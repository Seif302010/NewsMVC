//using Humanizer.Localisation;
using System.IO;

namespace NewsMVC.Models.DomainModels
{
    public class NewsDto : News
    {
        public IFormFile? image { get; set; }

    }
}
