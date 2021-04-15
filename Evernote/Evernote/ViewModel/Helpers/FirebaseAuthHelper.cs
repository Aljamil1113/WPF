using Evernote.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Evernote.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private const string api_key = "AIzaSyBj6kSenF94DFzlFoaMT-baA41OQ1kc-y0";
        public async static Task<bool> Register(User user)
        {
            using(HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={api_key}", data);

                if(response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = result.localId;

                    MessageBox.Show("You successfully register to Evernote!");

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }

        public async static Task<bool> Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={api_key}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = result.localId;

                    MessageBox.Show("You successfully LogIn to Evernote!");

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }

        public class FirebaseResult
        {
            public string kind { get; set; }
            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string expiresIn { get; set; }
            public string localId { get; set; }
        }

        public class ErrorDetails
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        public class Error
        {
            public ErrorDetails error { get; set; }
        }
    }
}
