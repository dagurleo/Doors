using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Repositories
{
    public class TopicRepository
    {
        //TODO: implement more functions we might need and connect to db;

        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * Return all topics within that group, for the searchbar expansion
         */
        public List<Topic> getTopicsByID(int groupID)
        {
            //TODO: return List<Topic> with all the topics within that group
            List<Topic> listOfTopic = new List<Topic>();
            return listOfTopic;
        }

        public Topic getTopicByID(int topicID)
        {
            Topic top = (from t in db.Topics
                        where t.ID == topicID
                        select t).SingleOrDefault();
            return top;
        }

        public IEnumerable<Topic> getTopicsForGroup(int groupId)
        {
            var topics = from t in db.Topics
                         where t.groupID == groupId
                         orderby t.topicName ascending
                         select t;

            return topics;
        }

        public bool doesTopicExistWithinGroup(int groupID, string topicName)
        {
            var topics = getTopicsForGroup(groupID);
            
            foreach(var t in topics)
            {
                if(t.topicName == topicName)
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * Add new topic
         * Need info on variables to parse here.
         */
        public void addNewTopic(int groupID, string topicName)
        {
            Topic topic = new Topic { groupID = groupID, topicName = topicName };
            db.Topics.Add(topic);
            db.SaveChanges();
        }
    }
}