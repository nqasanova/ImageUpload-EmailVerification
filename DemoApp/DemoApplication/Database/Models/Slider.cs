using System;
using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}