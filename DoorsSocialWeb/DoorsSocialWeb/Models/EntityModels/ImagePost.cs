using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class ImagePost : Post
    {
        public int imageID { get; set; }
        public int postID { get; set; }
        public string imageLink { get; set; }
    }
}