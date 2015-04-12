using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Models
{
    class Tweet
    {
        /// <summary>
        /// The ID of the tweet from Twitter
        /// </summary>
        public UInt64 id { get; set; }
        /// <summary>
        /// The text of the tweet
        /// </summary>
        public String text { get; set; }
        /// <summary>
        /// A user object of the poster of the tweet
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// The time when the tweet was posted
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// A basic contrusctor the tweet class
        /// </summary>
        /// <param name="id">The ID of the tweet from Twitter</param>
        /// <param name="text">text of the tweet</param>
        /// <param name="poster">The user object of the poster</param>
        /// <param name="hashtags">The a list of the hastags, as hashtag objects, in the tweet</param>
        /// <param name="urls">A List of the urls in the tweet, as url objects</param>
        /// <param name="createdAt">he time when the tweet was posted</param>
        public Tweet(UInt64 id, String text, User poster, List<Hashtag> hashtags, List<Url> urls, string createdAt)
        {
            this.id = id;
            this.text = text;
            this.user = poster;
            this.entities.hashtags = hashtags;
            this.entities.urls = urls;
            this.created_at = createdAt;
        }
        /// <summary>
        /// A blank contrustor
        /// </summary>
        public Tweet() { }

        public entitiesClass entities = new entitiesClass();

        public class entitiesClass
        {
            /// <summary>
            /// A list array of all hashtags, as a hashtag object, used in the tweet
            /// </summary>
            public List<Hashtag> hashtags { get; set; }
            /// <summary>
            /// A List of the urls in the tweet, as url objects
            /// </summary>
            public List<Url> urls { get; set; }
        }
    }
}
