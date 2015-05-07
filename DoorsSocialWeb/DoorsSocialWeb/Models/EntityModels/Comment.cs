using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Comment
    {
        public int commentID{ get; set; }
        public int authorID{ get; set; }
        public int postID { get; set; }
        public string subject { get; set; }
        public DateTime dateCreated { get; set; }
        public List<Like> likesOnComment { get; set; }
    }
}