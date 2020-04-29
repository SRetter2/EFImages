using System;

namespace EFImages.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public DateTime DatePosted { get; set; }
        public int NumberOfLikes { get; set; }
    }
}
