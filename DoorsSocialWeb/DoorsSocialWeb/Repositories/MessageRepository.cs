using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Repositories
{
    public class MessageRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<Message> getMessagesByID(int messageID)
        {
            //TODO: return a List<Message> of messages with that ID
            List<Message> listOfMessages = new List<Message>();
            return listOfMessages;
        }

        public void addNewMessage(Message m)
        {
            //TODO: Create a new message...
        }
    }
}