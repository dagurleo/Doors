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
        
        /*
         * Return a list of posts within that topic
         * Does this belong here or in Posts ?
         */
        public IEnumerable<Post> getPostsByTopicID(int topicID)
        {
            var queryAllPostsWithinTopic = (from postWithinTopic in db.Posts where postWithinTopic.ID == topicID && postWithinTopic.postIsInGroup == true select postWithinTopic);
            return queryAllPostsWithinTopic;
        }

        /*
         * Add new topic
         * Need info on variables to parse here.
         */
        public void addNewTopic()
        {
            //TODO: Add newTopic
        }
    }
}