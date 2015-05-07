using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class Post
    {
        public int ID { get; set; }

        
        public string authorID { get; set; }

        public string subject { get; set; }
        public int imageID { get; set; }
        public DateTime dateCreated { get; set; }
        public bool postIsInGroup { get; set; }
        public int groupTopicID { get; set; }
        
    }
}