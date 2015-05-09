using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class MessageService
    {
        /*
         * Selects all messages from db where sender is the user or reciever is the user. That way we can display all messages in time order
         */
        public IEnumerable<Message> getAllMessages(string senderID)
        {
            return getAllMessages(senderID);
        }

        /*
         * Returns a list of a conversation between two parties
         */
        public IEnumerable<Message> getConversation(string senderID, string recieverID)
        {
            return getConversation(senderID, recieverID);
        }

        /*
         * Returns all new messages who the user has not seen before
         */
        public IEnumerable<Message> getNewMessages()
        {
            return getNewMessages();
        }

        /*
         * Need info on what variables will be parsed here
         */
        public void addNewMessage()
        {
            //TODO: Create a new message...
        }
    }
}