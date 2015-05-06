using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Message
    {
        public int messageID { get; set; }
        public int senderID { get; set; }
        public int recieverID { get; set; }
        public string subject { get; set; }
        public DateTime dateCreated { get; set; }
        public bool messageIsRead { get; set; }
    }
}