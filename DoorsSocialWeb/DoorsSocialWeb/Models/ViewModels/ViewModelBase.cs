using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Models.ViewModels
{
    public abstract class ViewModelBase
    {
        public ApplicationUser user { get; set; }
    }

    public class HomeViewModel : ViewModelBase
    {

    }
}