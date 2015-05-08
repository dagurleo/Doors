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
        
        public List<Notification> getNotificationsByID(int notificationID)
        {
            return notificationRepo.getNotificationsByID(notificationID).ToList();
        }

        public List<Notification> getAllNewNotifications()
        {
            return notificationRepo.getAllNewNotifications().ToList();
        }
    }
}