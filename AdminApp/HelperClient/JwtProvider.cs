using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JWTFrontEnd.Providers
{
    public class JwtProvider
    {
        private static string _tokenUri;
        public JwtProvider()
        {
        }
        public class TokenResult
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }
        public static JwtProvider Create(string tokenUri)
        {
            _tokenUri = tokenUri;
            return new JwtProvider();
        }
        public async Task<TokenResult> GetTokenAsync(string username, string password, string clientId)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_tokenUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("device_id", Environment.MachineName),
                        new KeyValuePair<string, string>("client_id", clientId),
                    });
                var response = await client.PostAsync(string.Empty, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TokenResult>(result);
                }
                else
                {
                    //return null if unauthenticated
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private static JObject DecodePayload(string token)
        {
            var parts = token.Split('.');
            var payload = parts[1];

            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));
            return JObject.Parse(payloadJson);
        }

        public static ClaimsIdentity CreateIdentity(bool isAuthenticated, string userName, string accessToken, string refreshToken,string sessionId)
        {
            //decode the payload from token
            //in order to create a claim       
            dynamic payload = DecodePayload(accessToken);
            string userId = string.IsNullOrEmpty(payload?.nameid?.ToString()) ? "" : payload.nameid;
            string mobi = string.IsNullOrEmpty(payload?.given_name?.ToString()) ? "" : payload.given_name;
            string email = string.IsNullOrEmpty(payload?.email?.ToString()) ? "" : payload.email;
            var jwtIdentity = new ClaimsIdentity(new JwtIdentity(isAuthenticated, userName, CookieAuthenticationDefaults.AuthenticationScheme));
            //add roles
            if (payload.role != null)
            {
                if (payload.role.Type == JTokenType.Array)
                {
                    string[] roles = payload.role.ToObject(typeof(string[]));

                    foreach (var role in roles)
                    {
                        if (role.StartsWith("Iris.B2B.Paybill") || role.StartsWith("Iris.B2B.Paybill.Sales"))
                            jwtIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
                else if (payload.role.Type == JTokenType.String)
                {
                    var role = payload.role.ToObject(typeof(string));
                    if (role.StartsWith("Iris.B2B.Paybill") || role.StartsWith("Iris.B2B.Paybill.Sales"))
                        jwtIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            //add user id
            jwtIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            jwtIdentity.AddClaim(new Claim(ClaimTypes.GivenName, mobi));
            jwtIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
            jwtIdentity.AddClaim(new Claim("sessionId", sessionId));
            //jwtIdentity.AddClaim(new Claim("refresh_token", refreshToken));

            return jwtIdentity;
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }
    }


    public class JwtIdentity : IIdentity
    {
        private readonly bool _isAuthenticated;
        private readonly string _name;
        private readonly string _authenticationType;

        public JwtIdentity() { }
        public JwtIdentity(bool isAuthenticated, string name, string authenticationType)
        {
            _isAuthenticated = isAuthenticated;
            _name = name;
            _authenticationType = authenticationType;
        }
        public string AuthenticationType
        {
            get
            {
                return _authenticationType;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
