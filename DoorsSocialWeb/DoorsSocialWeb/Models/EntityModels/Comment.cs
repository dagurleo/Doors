using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoorsSocialWeb.Models.EntityModels
{
    public class Comment
    {
        public int ID{ get; set; }        
        public string authorID{ get; set; }
        public int postID { get; set; }
        public string subject { get; set; }
        public DateTime dateCreated { get; set; }
        
    }
}