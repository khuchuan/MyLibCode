using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DTO
{
    public class AuthenService
    {
        private readonly string _url_authen;

        public AuthenService(string URL_Authen)
        {
            _url_authen = URL_Authen;
        }

        public string GetToken(string username, string password, string clientID)
        {
            try
            {
                var stringPost = $"username={username}&password={password}&grant_type=password&client_id={clientID}";
                var request = WebRequest.Create(_url_authen);
                var data = Encoding.UTF8.GetBytes(stringPost);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                var response = request.GetResponse();
                var stringResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic dynamicData = JObject.Parse(stringResponse);
                return dynamicData.access_token;
            }
            catch (Exception ex)
            {
                //FileLogger.Error("Error in AthenService: GetToken()", ex);
                return string.Empty;
            }
        }
    }
}
