using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Services
{
    public class MessageService
    {
        private MessageRepository messageRepo = new MessageRepository();       
        /*
         * Selects all messages from db where sender is the user or reciever is the user. That way we can display all messages in time order
         */
        public IEnumerable<Message> getAllMessages(string senderID)
        {
            return messageRepo.getAllMessages();
        }

        /*
         * Returns a list of a conversation between two parties
         */
        public IEnumerable<Message> getConversation(string senderID, string recieverID)
        {
            return messageRepo.getConversation(senderID, recieverID);
        }

        /*
         * Returns all new messages who the user has not seen before
         */
        public IEnumerable<Message> getNewMessages()
        {
            return messageRepo.getNewMessages();
        }

        public IEnumerable<Message> getFirstMessages(string userID)
        {
            return messageRepo.getFirstMessages(userID);
        }

        /*
         * Need info on what variables will be parsed here
         */
        public void addNewMessage(Message message)
        {
            messageRepo.addNewMessage(message);
        }

        public IEnumerable<ApplicationUser> getUsersYouHaveChattedTo()
        {
            return messageRepo.getUsersYouHaveChattedTo();
        }

        public Message getNewestMessageForConversation(string id1, string id2)
        {
            return messageRepo.getNewestMessageForConversation(id1, id2);
        }
    }
}