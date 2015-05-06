using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Notification
    {
        public int notificationID { get; set; }
        public int userID { get; set; }
        public int postCommentID { get; set; }
        public int postLikedID { get; set; }
        public int friendRequestUserID { get; set; }
        public int groupRequestUserID { get; set; }
        public bool notificationIsSeen { get; set; }
        public DateTime dateCreated { get; set; }
    }
}