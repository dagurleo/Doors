using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class IndexViewModel : LoggedInSharedLayoutViewModel
    {       
        public IEnumerable<Post> posts { get; set; }
        public Post aPost { get; set; }
    }
}