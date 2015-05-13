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
    public class MessageRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * Selects all messages from db where sender is the user or reciever is the user. That way we can display all messages in time order
         */
        public IEnumerable<Message> getAllMessages()
        {
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();
            var queryAllMessages = (from message in db.Messages
                                    where message.senderID == currentUserID || message.recieverID == currentUserID
                                    orderby message.dateCreated ascending
                                    group message by message.senderID into messageGroup
                                    select messageGroup.First());
            return queryAllMessages;
        }

        /*
         * Returns a list of a conversation between two parties
         */
        public IEnumerable<Message> getConversation(string senderID, string recieverID)
        {
            var queryConversation = (from message in db.Messages
                                     where message.senderID == senderID && message.recieverID == recieverID
                                     orderby message.dateCreated
                                     select message);
            return queryConversation;
        }

        /*
         * Returns all new messages who the user has not seen before
         */
        public IEnumerable<Message> getNewMessages()
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var queryNewMessages = (from message in db.Messages where message.recieverID == currentUserId && message.messageIsRead == false select message);
            return queryNewMessages;
        }

        public IEnumerable<Message> getFirstMessages(string userID)
        {
            var queryAllMessages = (from m in db.Messages
                                    where m.recieverID == userID ||
                                    m.senderID == userID
                                    group m by m.dateCreated into mGroup
                                    let latest = mGroup.FirstOrDefault()
                                    select latest);

            //var queryAllMessages = queryAllRecievingFirstMessages.Concat(queryAllSendingFirstMessages);

            return queryAllMessages;
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