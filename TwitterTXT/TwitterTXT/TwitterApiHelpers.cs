using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT
{
    class TwitterApiHelpers
    {
        public static String generateNonce()
        {

            String nonce = "";
            Random rand = new Random();
            for (int x = 0; x < 32; x++)
            {
                int num = rand.Next(59);
                if (num < 10)
                {
                    nonce += (char)(num + 48);
                }
                else if (num < 35)
                {
                    nonce += (char)(num + 55);
                }
                else
                {
                    nonce += (char)(num + 62);
                }
            }
            return nonce;
        }

        public static Dictionary<string, string> convertResponseToDictionary(string response)
        {
            Dictionary<string, string> keyValDict = new Dictionary<string, string>();
            string[] keyValuesPair = response.Split('&');
            foreach (string keyValue in keyValuesPair)
            {
                string[] parts = keyValue.Split('=');
                keyValDict.Add(parts[0], Uri.UnescapeDataString(parts[1]));
            }
            return keyValDict;
        }

        public static string generateOAuthSignature(string uri,  String requestType, String oauthTokenSecret, SortedDictionary<string, string> tokens)
        {
            String parameterString = convertTokensToParameterString(tokens); //"oauth_consumer_key=" + Properties.Resources.OAuthConsumerKey + "&oauth_nonce=" + nonce + "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" + timestamp + "&oauth_version=1.0";
            String signatureBase = requestType + "&" + Uri.EscapeDataString(uri) + "&" + Uri.EscapeDataString(parameterString);
            String signingKey = Properties.Resources.ConsumerSecret + "&" + oauthTokenSecret;
            System.Security.Cryptography.HMACSHA1 hmac = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(signingKey));
            byte[] oauthSignature = hmac.ComputeHash(Encoding.ASCII.GetBytes(signatureBase));
            return Uri.EscapeDataString(System.Convert.ToBase64String(oauthSignature));
        }

        public static string convertTokensToParameterString(SortedDictionary<string, string> tokens)
        {
            string header = "";
            if (tokens.Count == 0)
            {
                return header;
            }

            header += tokens.ElementAt(0).Key + "=" + Uri.EscapeDataString(tokens.ElementAt(0).Value);
            for (int x = 1; x < tokens.Count; x++)
            {
                KeyValuePair<string, string> token = tokens.ElementAt(x);
                header += "&" + token.Key + "=" + Uri.EscapeDataString(token.Value);
            }
            return header;
        }

        public static string convertTokensToHeaderString(SortedDictionary<string, string> tokens)
        {
            string header = "OAuth ";
            if (tokens.Count == 0)
            {
                return header;
            }

            header += tokens.ElementAt(0).Key + "=\"" + Uri.EscapeDataString(tokens.ElementAt(0).Value) + "\"";
            for (int x = 1; x < tokens.Count; x++)
            {
                KeyValuePair<string, string> token = tokens.ElementAt(x);
                header += ", " + token.Key + "=\"" + Uri.EscapeDataString(token.Value) + "\"";
            }
            return header;
        }

        public static String GetCurrentUnixTimestampSeconds()
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return ((long)(DateTime.UtcNow - UnixEpoch).TotalSeconds).ToString();
        }

    }
}
