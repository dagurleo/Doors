using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Models.EntityModels
{
    public class groupRequest
    {
        public int id { get; set; }
        public string groupOwnerId { get; set; }
        public string userRequestId { get; set; }
        public bool userIsApproved { get; set; }
    }
}