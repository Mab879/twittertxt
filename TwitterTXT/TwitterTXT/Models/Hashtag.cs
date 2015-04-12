using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Models
{
    class Hashtag
    {
        /// <summary>
        /// The text of the hashtag without the '#"
        /// </summary>
        public String text { get; set; }
        /// <summary>
        /// The start and index of the hashtag in the tweet
        /// </summary>
        public int[] indices { get; set; }
        ///// <summary>
        ///// The starting index of the hashtag
        ///// </summary>
        //public int startIndex { get; set; }
        ///// <summary>
        ///// The ending index of the hashtag
        ///// </summary>
        //public int endIndex { get; set; }
        /// <summary>
        /// A Basic contrusctor for the hash tag
        /// </summary>
        /// <param name="text">The text of the hash tag without the '#'</param>
        /// <param name="startIndex">The starting index of the hash tag</param>
        /// <param name="endIndex">The ending index of the hash tag</param>
        public Hashtag(String text, int startIndex, int endIndex) {
            this.text = text;
            this.indices = new int[2];
            this.indices[0] = startIndex;
            this.indices[1] = endIndex;
            //this.startIndex = startIndex;
            //this.endIndex = endIndex;
        }

        /// <summary>
        /// Blank contrustor
        /// </summary>
        public Hashtag() { }
    }
}
