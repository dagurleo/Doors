using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Post
    {
        public int postID { get; set; }
        public int authorID { get; set; }
        public string subject { get; set; }
        public int imageID { get; set; }
        public DateTime dateCreated { get; set; }
        public bool postIsInGroup { get; set; }
        public int groupTopicID { get; set; }
        public List<Like> likesOnPost { get; set; }
        public List<Comment> commentsOnPost { get; set; }
    }
}