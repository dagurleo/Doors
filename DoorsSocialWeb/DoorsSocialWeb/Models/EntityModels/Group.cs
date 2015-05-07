using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class Group
    {
        public int ID { get; set; }
        public string groupOwnerID { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        
    }
}