using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class Topic
    {
        public int topicID { get; set; }
        public int groupID { get; set; }
        public string topicName { get; set; }
        public List<Post> posts { get; set; }
    }
}