using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class NotificationService
    {
        private NotificationRepository notificationRepo = new NotificationRepository();
        
        public IEnumerable<Notification> getNotificationsByID(int notificationID)
        {
            return notificationRepo.getNotificationsByID(notificationID);
        }

        public IEnumerable<Notification> getAllNewNotifications()
        {
            return notificationRepo.getAllNewNotifications();
        }

        /*
        public IEnumerable<Notification> getAllNewGroupRequests(int groupID, string ownerID)
        {
            //return notificationRepo.getAllNewGroupRequests(groupID, ownerID);
        }
         */
    }
}