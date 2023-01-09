using System;
using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Image : BaseEntity<int>, IAuditable
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

