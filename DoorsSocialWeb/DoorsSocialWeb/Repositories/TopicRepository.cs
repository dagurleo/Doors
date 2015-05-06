using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Model_Classes;

namespace DoorsSocialWeb.Repositories
{
    public class TopicRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<Topic> getTopicsByID(int groupID) // I THINK GROUPID ?
        {
            //TODO: return List<Topic> with all the topics within that group
            List<Topic> listOfTopic = new List<Topic>();
            return listOfTopic;
        }

        public void addNewTopic(Topic newTopic)
        {
            //TODO: Add newTopic
        }
    }
}