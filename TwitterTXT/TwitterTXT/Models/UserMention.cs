using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Models
{
    class UserMention
    {
        /// <summary>
        /// The ID user the user from Twitter
        /// </summary>
        public UInt64 id { get; set; }
        /// <summary>
        /// The user name the. Example (@username)
        /// </summary>
        public String screenName { get; set; }
        /// <summary>
        /// The full name of the user methonded
        /// </summary>
        public String fullName { get; set; }
        /// <summary>
        /// The start index of where the username of the metioned user
        /// </summary>
        public int startIndex { get; set; }
        /// <summary>
        /// The end index of where the username of the metioned user
        /// </summary>
        public int endIndex { get; set; }
        /// <summary>
        /// This is a basic contrustor for the UserMention
        /// </summary>
        /// <param name="id"></param>
        /// <param name="screenName"></param>
        /// <param name="fullName"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        public UserMention(UInt64 id, String screenName, String fullName, int startIndex, int endIndex) {
            this.id = id;
            this.screenName = screenName;
            this.fullName = fullName;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        /// <summary>
        /// Blank contrustor
        /// </summary>
        public UserMention() { }

        
    }
}
