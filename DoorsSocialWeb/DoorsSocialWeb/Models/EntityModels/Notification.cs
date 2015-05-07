using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class Notification
    {
        public int ID { get; set; }
        public string userID { get; set; }
        public int postCommentID { get; set; }
        public int postLikedID { get; set; }
        public string friendRequestUserID { get; set; }
        public int groupRequestUserID { get; set; }
        public bool notificationIsSeen { get; set; }
        public DateTime dateCreated { get; set; }
    }
}