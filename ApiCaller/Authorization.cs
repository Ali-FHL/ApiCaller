using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;

namespace ApiCaller
{
    public class Authorization
    {
        public static async Task<string> GetToken()
        {
            string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            string Token = string.Empty;

            await Task.Run(() =>
            {
                byte[] byte1 = Encoding.ASCII.GetBytes("grant_type=client_credentials");

                HttpWebRequest bearerReq = WebRequest.Create("https://sandbox.parsian-bank.ir/oauth2/token") as HttpWebRequest;
                bearerReq.Accept = "application/json";
                bearerReq.Method = "POST";
                bearerReq.ContentType = "application/x-www-form-urlencoded";
                bearerReq.ContentLength = byte1.Length;
                bearerReq.KeepAlive = false;
                bearerReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(consumerKey + ":" + consumerSecret)));
                Stream newStream = bearerReq.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);

                WebResponse bearerResp = bearerReq.GetResponse();

                using (var reader = new StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8))
                {
                    var response = reader.ReadToEnd();
                    Bearer bearer = JsonConvert.DeserializeObject<Bearer>(response);
                    Token = bearer.access_token;
                }
            });

            return Token;
        }


        public static T Post<T>(string uri, object body)
        {
            var handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            var requestUri = new Uri(uri);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken().Result);

            if (body != null)
            {
                var jsonSerialize = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");
            }

            var response = httpClient.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
                return default;

            var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

            return result;
        }
    }
}
