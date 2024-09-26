using System.Security.Cryptography;
using static BlazorApp1.Components.Pages.Home;

namespace BlazorApp1.Codes
{
    public class AsymetriskEncryptionHandler
    {
        private string _privateKey;
        private string _publicKey;
        private readonly HttpClient _httpClient;

        public AsymetriskEncryptionHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;

            if (!File.Exists("privateKey.pem"))
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    _privateKey = rsa.ToXmlString(true);
                    _publicKey = rsa.ToXmlString(false);

                    File.WriteAllText("privateKey.pem", _privateKey);
                    File.WriteAllText("publicKey.pem", _publicKey);
                }
            }
            else
            {
                _privateKey = File.ReadAllText("privateKey.pem");
                _publicKey = File.ReadAllText("publicKey.pem");
            }
        }

        public async Task<string> SendEncryptionRequest(string todoItem)
        {
            
            var encryptRequest = new EncryptRequest
            {
                TodoItem = todoItem,
                PublicKey = _publicKey
            };

            var encryptedResponse = await _httpClient.PostAsJsonAsync("https://localhost:7242/api/todo/encrypt", encryptRequest);

            // Check if the response was successful
            if (encryptedResponse.IsSuccessStatusCode)
            {
                // Read the response content
                return await encryptedResponse.Content.ReadAsStringAsync();
            }

            // Handle the case where the response was not successful
            return null;
        }

        public string DecryptAsymetrisk(string textToDecrypt)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);

                byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
                byte[] decryptedDataAsByteArray = rsa.Decrypt(byteArrayTextToDecrypt, false);
                string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptedDataAsByteArray);

                return decryptedDataAsString;
            }
        }
    }
}
