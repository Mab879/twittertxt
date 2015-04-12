using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Models
{
    class Url
    {
        /// <summary>
        /// The URL from twitter (might be short)
        /// </summary>
        public String url { get; set; }
        /// <summary>
        /// The full, real URL
        /// </summary>
        public String expanded_url { get; set; }
        /// <summary>
        /// The dispaly url from twitter 
        /// </summary>
        public String display_url { get; set; }
        /// <summary>
        /// The start and index of the display URL in the tweet
        /// </summary>
        public int[] indices { get; set; }
        ///// <summary>
        ///// The start index of the display URL in the tweet
        ///// </summary>
        //public int startIndex { get; set; }
        ///// <summary>
        ///// The end of the display URL in the tweet
        ///// </summary>
        //public int endIndex { get; set; }
        /// <summary>
        /// A basic contrustor for a Url
        /// </summary>
        /// <param name="theUrl">The URL from twitter (might be short)</param>
        /// <param name="expandedUrl">The full, real URL</param>
        /// <param name="displayURL">The start index of the display URL in the tweet</param>
        /// <param name="startIndex">The start index of the display URL in the tweet</param>
        /// <param name="endIndex">The end of the display URL in the tweet</param>
        public Url(String theUrl, String expandedUrl, String displayURL, int startIndex, int endIndex)
        {
            this.url = url;
            this.expanded_url = expandedUrl;
            this.display_url = displayURL;
            this.indices = new int[2];
            this.indices[0] = startIndex;
            this.indices[1] = endIndex;
            //this.startIndex = startIndex;
            //this.endIndex = endIndex;
        }

        /// <summary>
        /// Blank contrustor
        /// </summary>
        public Url() { }
    }
}
