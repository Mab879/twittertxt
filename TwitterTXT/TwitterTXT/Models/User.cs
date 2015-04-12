using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Models
{
    class User
    {
        /// <summary>
        /// ID the user from Twitter
        /// </summary>
        public UInt64 id { get; set; }
        /// <summary>
        /// The full name of the user
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// The screenname of the user
        /// </summary>
        public String screen_name { get; set; }
        /// <summary>
        /// A basic contrustor for the User
        /// </summary>
        /// <param name="id">ID the user from Twitter</param>
        /// <param name="fullName">he full name of the user</param>
        /// <param name="screenName">The screenname of the user</param>
        public User(UInt64 id, String fullName, String screenName) {
            this.id = id;
            this.name = fullName;
            this.screen_name = screenName;
        }
        /// <summary>
        /// Blank contrustor
        /// </summary>
        public User() { }
    }
}
