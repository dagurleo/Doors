using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class AllMessagesViewModel : LoggedInSharedLayoutViewModel
    {
        public IEnumerable<ApplicationUser> messageUsers { get; set; }
        public IEnumerable<Message> firstMessages { get; set; } 
        
    }
}