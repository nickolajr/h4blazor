using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Hashing
{
    public class HashingHandler
    {
      
        public string HmcHashing(string TextToHash)
        {
            //my key is key = to Encoding.ASCII.GetBytes("key");
            byte[] mykey = Encoding.ASCII.GetBytes("key");
            byte[] inputbyte = Encoding.ASCII.GetBytes(TextToHash);
            HMACSHA256 hMACSHA256 = new HMACSHA256();
            hMACSHA256.Key = mykey;
            byte[] hashedvalue = hMACSHA256.ComputeHash(inputbyte);
            return Convert.ToBase64String(hashedvalue);

        }
    }
}
