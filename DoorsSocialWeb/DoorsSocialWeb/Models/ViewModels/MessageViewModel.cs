using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class MessageViewModel : LoggedInSharedLayoutViewModel
    {
        public IEnumerable<Message> messagesWithUser { get; set; }
        public ApplicationUser messageReciever { get; set; }
    }
}