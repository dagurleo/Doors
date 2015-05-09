using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class GroupViewModel : LoggedInSharedLayoutViewModel
    {
        public Group currentGroup { get; set; }
        public IEnumerable<Post> groupPosts { get; set; }
        public IEnumerable<Topic> currentGroupTopics { get; set; }
    }
}