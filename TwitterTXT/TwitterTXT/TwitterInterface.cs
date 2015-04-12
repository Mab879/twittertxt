using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;


namespace TwitterTXT
{
    using Events;
    class TwitterInterface
    {
        private string oauthToken;
        private string oauthTokenSecret;
        public string screenanme;
        private WebClient streamingWebclient = new WebClient();
        private TwitterListener tl;

        public String getLoginUrl()
        {
            String uri = "https://api.twitter.com/oauth/request_token";
            WebClient client = new WebClient();

            String nonce = TwitterApiHelpers.generateNonce();
            String timestamp = TwitterApiHelpers.GetCurrentUnixTimestampSeconds();
            SortedDictionary<string, string> OAuthTokens = new SortedDictionary<string, string>();
            OAuthTokens.Add("oauth_consumer_key", Properties.Resources.OAuthConsumerKey);
            OAuthTokens.Add("oauth_nonce", nonce);
            OAuthTokens.Add("oauth_signature_method", "HMAC-SHA1");
            OAuthTokens.Add("oauth_timestamp", timestamp);
            OAuthTokens.Add("oauth_version", "1.0");
            String HeaderContent = TwitterApiHelpers.convertTokensToHeaderString(OAuthTokens); //"OAuth oauth_consumer_key=\"" + Properties.Resources.OAuthConsumerKey + "\", oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timestamp + "\", oauth_version=\"1.0\"";

            client.Headers.Add("Authorization", HeaderContent + ", oauth_signature=\"" + TwitterApiHelpers.generateOAuthSignature(uri, "POST", "", OAuthTokens) + "\"");
            byte[] dumbEmptyArray = new byte[0];
            byte[] responseArray = client.UploadData(uri, "POST", dumbEmptyArray);
            Dictionary<string, string> responseDict = TwitterApiHelpers.convertResponseToDictionary(Encoding.ASCII.GetString(responseArray));
            oauthToken = responseDict["oauth_token"];
            oauthTokenSecret = responseDict["oauth_token_secret"];
            return "https://api.twitter.com/oauth/authorize?" + Encoding.ASCII.GetString(responseArray);
        }

        public bool setOAuthWithPin(string pin)
        {
            String uri = "https://api.twitter.com/oauth/access_token";
            WebClient client = new WebClient();

            String nonce = TwitterApiHelpers.generateNonce();
            String timestamp = TwitterApiHelpers.GetCurrentUnixTimestampSeconds();
            SortedDictionary<string, string> OAuthTokens = new SortedDictionary<string, string>();
            OAuthTokens.Add("oauth_consumer_key", Properties.Resources.OAuthConsumerKey);
            OAuthTokens.Add("oauth_nonce", nonce);
            OAuthTokens.Add("oauth_signature_method", "HMAC-SHA1");
            OAuthTokens.Add("oauth_timestamp", timestamp);
            OAuthTokens.Add("oauth_token", oauthToken);
            OAuthTokens.Add("oauth_version", "1.0");
            String HeaderContent = TwitterApiHelpers.convertTokensToHeaderString(OAuthTokens); //"OAuth oauth_nonce=\"" + nonce + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timestamp + "\", oauth_version=\"1.0\"";

            OAuthTokens.Add("oauth_verifier", pin);
            client.Headers.Add("Authorization", HeaderContent + ", oauth_signature=\"" + TwitterApiHelpers.generateOAuthSignature(uri, "POST", oauthTokenSecret, OAuthTokens) + "\"");
            client.Headers.Add("ContentType", "application/x-www-form-urlencoded");

            byte[] bodyArray = Encoding.ASCII.GetBytes("oauth_verifier=" + pin);
            try
            {
                Byte[] responseArray = client.UploadData(uri, "POST", bodyArray);
                Dictionary<string, string> responseDict = TwitterApiHelpers.convertResponseToDictionary(Encoding.ASCII.GetString(responseArray));
                //TODO store use user_id and screen_name
                oauthToken = responseDict["oauth_token"];
                oauthTokenSecret = responseDict["oauth_token_secret"];
                screenanme = responseDict["screen_name"];
            }
            catch (System.Net.WebException)
            {

                return false;
            }

            //return "https://api.twitter.com/oauth/authorize?" + Encoding.ASCII.GetString(responseArray);
            return true;
        }

        public void postThatCrapToTwitter(string crap)
        {
            String uri = "https://api.twitter.com/1.1/statuses/update.json";
            WebClient client = new WebClient();

            String nonce = TwitterApiHelpers.generateNonce();
            String timestamp = TwitterApiHelpers.GetCurrentUnixTimestampSeconds();
            SortedDictionary<string, string> OAuthTokens = new SortedDictionary<string, string>();
            OAuthTokens.Add("oauth_consumer_key", Properties.Resources.OAuthConsumerKey);
            OAuthTokens.Add("oauth_nonce", nonce);
            OAuthTokens.Add("oauth_signature_method", "HMAC-SHA1");
            OAuthTokens.Add("oauth_timestamp", timestamp);
            OAuthTokens.Add("oauth_token", oauthToken);
            OAuthTokens.Add("oauth_version", "1.0");
            OAuthTokens.Add("status", Uri.UnescapeDataString(crap));

            OAuthTokens.Add("oauth_signature", Uri.UnescapeDataString(TwitterApiHelpers.generateOAuthSignature(uri, "POST", oauthTokenSecret, OAuthTokens)));
            OAuthTokens.Remove("status");
            String HeaderContent = TwitterApiHelpers.convertTokensToHeaderString(OAuthTokens);

            client.Headers.Add("Authorization", HeaderContent);
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Remove("Expect");

            byte[] bodyArray = Encoding.ASCII.GetBytes("status=" + crap);

            Byte[] responseArray = client.UploadData(uri, "POST", bodyArray);
            String response = Encoding.ASCII.GetString(responseArray);
        }

        public void listenAsync()
        {
            tl = new TwitterListener();
            tl.listenForTweetEvent(streamingWebclient, oauthToken, oauthTokenSecret);
        }

        public void listenStop()
        {
            streamingWebclient.CancelAsync();
            streamingWebclient.Dispose();
        }
    }
}
