using System;
namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int id, string mainTitle, string content, string buttonName, string buttonURL, int order, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            MainTitle = mainTitle;
            Content = content;
            ButtonName = buttonName;
            ButtonURL = buttonURL;
            Order = order;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}