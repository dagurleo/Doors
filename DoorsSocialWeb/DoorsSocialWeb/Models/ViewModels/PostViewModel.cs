using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.ViewModels;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class PostViewModel
    {
        public Post aPost { get; set; }
        public ApplicationUser postAuthor { get; set; }
    }
}