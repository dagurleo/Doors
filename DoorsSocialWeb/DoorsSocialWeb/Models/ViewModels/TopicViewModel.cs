using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class TopicViewModel
    {
        public ApplicationUser currentUser { get; set; }
        public IEnumerable<Group> groups { get; set; }
        public IEnumerable<ApplicationUser> friends { get; set; }
        public Group currentGroup { get; set; }
        public IEnumerable<Topic> topics { get; set; }
        public IEnumerable<Post> posts { get; set; }

    }
}