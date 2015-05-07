using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class ImagePost : Post
    {
        public int ID { get; set; }
        public int postID { get; set; }
        public string imageLink { get; set; }
    }
}