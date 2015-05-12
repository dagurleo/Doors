using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class friendRequest
    {
        public int ID { get; set; }
        public string userID { get; set; }
        public string userRequestID { get; set; }
        public bool userIsApproved { get; set; }
    }
}