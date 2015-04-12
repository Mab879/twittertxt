using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using TwitterTXT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TwitterTXT.Events
{
    class TwitterListener
    {
        private Stream twitterStream;
        public delegate void NewTweetHandler(Tweet tweet);
        public delegate void DeletedTweetHandler(string id);
        public static event NewTweetHandler NewTweet;
        public static event EventHandler DeletedTweet;

        public void listenForTweetEvent(WebClient client, String oauthToken, String oauthTokenSecret)
        {
            String uri_string = "https://userstream.twitter.com/1.1/user.json";
            String nonce = TwitterApiHelpers.generateNonce();
            String timestamp = TwitterApiHelpers.GetCurrentUnixTimestampSeconds();
            SortedDictionary<string, string> OAuthTokens = new SortedDictionary<string, string>();
            OAuthTokens.Add("oauth_consumer_key", Properties.Resources.OAuthConsumerKey);
            OAuthTokens.Add("oauth_nonce", nonce);
            OAuthTokens.Add("oauth_signature_method", "HMAC-SHA1");
            OAuthTokens.Add("oauth_timestamp", timestamp);
            OAuthTokens.Add("oauth_version", "1.0");
            OAuthTokens.Add("oauth_token", oauthToken);
            OAuthTokens.Add("oauth_signature", Uri.UnescapeDataString(TwitterApiHelpers.generateOAuthSignature(uri_string, "GET", oauthTokenSecret, OAuthTokens)));
            String HeaderContent = TwitterApiHelpers.convertTokensToHeaderString(OAuthTokens); //"OAuth oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timestamp + "\", oauth_version=\"1.0\"";
            client.Headers.Add("Authorization", HeaderContent);
            twitterStream = client.OpenRead(uri_string);
            Thread datThread = new Thread(processStream);
            datThread.Start();
        }

        private void processStream()
        {
            string buffer = "";
            char readChar = (char)twitterStream.ReadByte();
            while (true)// stupid stuff is fun!
            {

                buffer += readChar;

                try
                {
                    readChar = (char)twitterStream.ReadByte();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
                if (readChar == '\n')
                {
                    // TODO raise events
                    if (String.IsNullOrWhiteSpace(buffer))
                    {
                        buffer = "";
                    }
                    else
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Converters.Add(new JavaScriptDateTimeConverter());
                        var deserializedObj = JsonConvert.DeserializeObject(buffer);
                        Tweet deserializedTweet = JsonConvert.DeserializeObject<Tweet>(buffer);
                        if (deserializedTweet.created_at != null) {
                            //Console.WriteLine(buffer);
                            TwitterListener.NewTweet(deserializedTweet);
                            buffer = "";
                        }
                        else
                        {
                            buffer = "";
                        }
                  
                    }

                }
            }
        }

    }
}
