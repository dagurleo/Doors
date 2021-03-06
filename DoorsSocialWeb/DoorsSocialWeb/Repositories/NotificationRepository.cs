﻿using System;
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
            var queryNotificationByID = (from notification in db.Notifications 
                                         where notification.ID == notificationID 
                                         select notification);
            return queryNotificationByID;
        }

        public IEnumerable<Notification> getAllNewNotifications()
        {
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();
            var queryNewNotifications = (from notification in db.Notifications 
                                         where notification.userID == currentUserID && notification.notificationIsSeen == false
                                         select notification);
            return queryNewNotifications;
        }

        /*
        public IEnumerable<Notification> getAllNewGroupRequests(int groupID, string ownerID)
        {

            var requests = from gr in db.Groups
                           where gr.ID == groupID
                           join ur in db.groupRequests
                           on gr.ID equals ur.id
        }
         */
    }
}