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
                                     where (message.senderID == senderID && message.recieverID == recieverID)
                                     || (message.senderID == recieverID && message.recieverID == senderID)  
                                     orderby message.dateCreated
                                     select message);

            foreach(var i in queryConversation)
            {
                if(i.recieverID == HttpContext.Current.User.Identity.GetUserId())
                {
                    i.messageIsRead = true;
                }
            }
            db.SaveChanges();
                   
            return queryConversation;
        }


        public IEnumerable<ApplicationUser> getUsersYouHaveChattedTo()
        {
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();
            var users = (from m in db.Messages
                         where m.recieverID == currentUserID && m.senderID != currentUserID
                         join u in db.Users
                         on m.senderID equals u.Id
                         orderby m.dateCreated descending
                         select u).Distinct();
                                          
            return users;
        }
        public Message getNewestMessageForConversation(string id1, string id2)
        {
            var newestMessage = (from m in db.Messages
                                 where m.senderID == id1 && m.recieverID == id2
                                 || m.senderID == id2 && m.recieverID == id1
                                 orderby m.dateCreated descending
                                 select m).FirstOrDefault();
            return newestMessage;   

        }

        /*
         * Returns all new messages who the user has not seen before
         */
        public IEnumerable<Message> getNewMessages()
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var queryNewMessages = (from message in db.Messages 
                                    where message.recieverID == currentUserId && message.messageIsRead == false 
                                    select message);
            return queryNewMessages;
        }
        
        public IEnumerable<Message> getFirstMessages(string userID)
        {                                                       
            //var queryAllMessages = queryAllRecievingFirstMessages.Concat(queryAllSendingFirstMessages);
            var queryAllMessages = from m in db.Messages
                                   select m;

            return queryAllMessages;
        }

        /*
         * Need info on what variables will be parsed here
         */
        public void addNewMessage(Message message)
        {
            db.Messages.Add(message);
            db.SaveChanges();
        }        

        public int getUnreadMessageCount()
        {
            var unreadMessages = (from m in db.Messages
                                  where m.messageIsRead == false
                                  select m).Count();

            return unreadMessages;
        }
    }
}