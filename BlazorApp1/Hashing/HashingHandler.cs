using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Hashing
{
    public class HashingHandler
    {
        //can use SHA156Hashing
        //can use hmchashing
        public string HmcHashing(string TextToHash)
        {
            byte[] mykey = Encoding.ASCII.GetBytes("key");
            byte[] inputbyte = Encoding.ASCII.GetBytes(TextToHash);
            HMACSHA256 hMACSHA256 = new HMACSHA256();
            hMACSHA256.Key = mykey;
            byte[] hashedvalue = hMACSHA256.ComputeHash(inputbyte);
            return Convert.ToBase64String(hashedvalue);

        }
    }
}
