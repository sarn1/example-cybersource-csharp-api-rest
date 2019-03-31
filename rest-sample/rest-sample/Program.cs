using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace rest_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string SecretKey = "YOUR_SECRET_KEY";
            string KeyId = "YOUR_KEY_ID";
            string MerchantId = "YOUR_MERCHANT_ID";

            // pulling from file to control the body payload
            string JsonObj = File.ReadAllText(@"json.txt");        
           
            var Digest = GenerateDigest(JsonObj);
            //Console.WriteLine(Digest);

            var SignatureParm = "host: apitest.cybersource.com\n(request-target): post /pts/v2/payments/\ndigest: " + Digest + "\nv-c-merchant-id: " + MerchantId;
            var SignatureHash = GenerateSignatureFromParams(SignatureParm, SecretKey);
            //Console.WriteLine(SignatureHash);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://apitest.cybersource.com/pts/v2/payments/");
            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add("v-c-merchant-id", MerchantId);
            httpWebRequest.Headers.Add("v-c-date", DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'"));
            httpWebRequest.Headers.Add("Digest", Digest);
            httpWebRequest.Headers.Add("Signature", "keyid=\""+ KeyId + "\", algorithm=\"HmacSHA256\", headers=\"host (request-target) digest v-c-merchant-id\", signature=\"" + SignatureHash + "\"");
            httpWebRequest.ContentType = "application/json";

            var httpResponse = new HttpWebResponse();
            string res = "";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonObj);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    res = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(httpResponse.ToString());
            }

            Console.Write(res);

            Console.ReadKey();
        }

        public static string GenerateDigest(string bodyText)
        {
            var digest = "";
            using (var sha256hash = SHA256.Create())
            {
                byte[] payloadBytes = sha256hash
                    .ComputeHash(Encoding.UTF8.GetBytes(bodyText));
                digest = Convert.ToBase64String(payloadBytes);
                digest = "SHA-256=" + digest;
            }
            return digest;
        }

        public static string GenerateSignatureFromParams(string signatureParams, string secretKey)
        {
            var sigBytes = Encoding.UTF8.GetBytes(signatureParams);
            var decodedSecret = Convert.FromBase64String(secretKey);
            var hmacSha256 = new HMACSHA256(decodedSecret);
            var messageHash = hmacSha256.ComputeHash(sigBytes);
            return Convert.ToBase64String(messageHash);
        }
    }
}
