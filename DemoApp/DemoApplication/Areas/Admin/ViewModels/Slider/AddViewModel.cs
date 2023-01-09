using System;
namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        public int? Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}