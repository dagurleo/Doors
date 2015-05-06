using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Model_Classes;

namespace DoorsSocialWeb.Repositories
{
    public class NotificationRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<Notification> getNotificationsByID(int notificationID)
        {
            //TODO: return a List<Notification> of notifications.
            List<Notification> listOfNotification = new List<Notification>();
            return listOfNotification;
        }
    }
}