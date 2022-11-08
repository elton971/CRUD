using System;
using System.IO;

namespace Doiman
{
    public class Post
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public  DateTimeOffset Creationdate { get; set; }
        public User User { get; set; }
    }
}