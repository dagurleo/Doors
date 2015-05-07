using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoorsSocialWeb.Repositories
{
    public class NotificationRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //TODO: implement more functions we might need and connect to db;
        public IEnumerable<Notification> getNotificationsByID(int notificationID)
        {
            var queryNotificationByID = (from notification in db.Notifications where notification.ID == notificationID select notification);
            return queryNotificationByID;
        }

        public IEnumerable<Notification> getAllNewNotifications()
        {
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();
            var queryNewNotifications = (from notification in db.Notifications where notification.ID == currentUserID && notification.notificationIsSeen == false);
            return queryNewNotifications;
        }
    }
}