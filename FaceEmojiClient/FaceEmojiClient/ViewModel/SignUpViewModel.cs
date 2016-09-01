using FaceEmojiClient;
using FaceEmojiClient.Service;
using FaceEmojiClient.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace FaceEmojiClient.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        string firstName;
        string lastName;
        string username;
        string password;
        string email;

        Command signUpUserCommand;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        public Command SignUpUserCommand
        {
            get { return signUpUserCommand ?? (signUpUserCommand = new Command( async () =>
            {
                bool ok = await SignUpByREST(
                    this.Username,
                    this.Password,
                    this.Email,
                    this.FirstName,
                    this.LastName
                    );
                if (ok)
                {
                    DialogService.ShowSuccess("Successfully Signup");
                    
                }
                else
                {
                    DialogService.ShowError("Failed");
                }

            })); }
        }

        private async Task ExecuteSignUpUserCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var user = new User
            {
                Name = string.Format("{0} {1}", FirstName, LastName),
                //ProfileImage = GravatarService.CalculateUrl(Email)
            };

            var account = new Account
            {
                Username = Username,
                Password = Password,
                Email = Email,
                UserId = user.Id
            };


        }

        private async Task CreateAccount(Account account, User user)
        {
        }

        private async Task SignIn(Account account)
        {
        }

        private void NavigateToMainUI()
        {
        }

        public async Task<bool> SignUpByREST(string username, string password, string email, string firstname, string lastname)
        {
            string json = "{"
                + "\"username\":" + "\"" + username + "\","
                + "\"password\":" + "\"" + password + "\","
                + "\"firstname\":" + "\"" + firstname + "\","
                + "\"lastname\":" + "\"" + lastname + "\","
                + "\"email\":" + "\"" + email + "\""
                + "}";
            string respJson = await MakePostRequestByClient("http://joi-testp.chinacloudapp.cn:8090/auth/register", json);
            JObject resp = JObject.Parse(respJson);
            if (resp.Equals("success"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> MakePostRequest(string url, string serializedDataString, bool isJson = true)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (isJson)
                request.ContentType = "application/json";
            else
                request.ContentType = "application/x-www-form-urlencoded";

            request.Method = "POST";
            var stream = await request.GetRequestStreamAsync();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(serializedDataString);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();

            using (StreamReader sr = new StreamReader(respStream))
            {
                return sr.ReadToEnd();
            }
        }

        public async Task<string> MakePostRequestByClient(string url, string json)
        {
            var client = new System.Net.Http.HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://joi-testp.chinacloudapp.cn:8090/auth/register", content);
            return await response.Content.ReadAsStringAsync();
        }


    }
}
