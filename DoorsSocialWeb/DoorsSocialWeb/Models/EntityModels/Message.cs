using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class Message
    {
        public int messageID { get; set; }
        public string senderID { get; set; }
        public string recieverID { get; set; }
        public string subject { get; set; }
        public DateTime dateCreated { get; set; }
        public bool messageIsRead { get; set; }
    }
}