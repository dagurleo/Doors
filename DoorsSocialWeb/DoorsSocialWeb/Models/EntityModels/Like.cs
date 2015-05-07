using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Like
    {
        public int likeID { get; set; }
        public int authorID { get; set; }
        public int postID { get; set; }
        public int commentID { get; set; }
    }
}