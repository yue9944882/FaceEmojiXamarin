using FaceEmojiClient.Message;
using FaceEmojiClient.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FaceEmojiClient.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        string username;
        string password;

        Command logInUserCommand;

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

        public Command SignInUserCommand
        {
            get {
                return logInUserCommand ?? (logInUserCommand = new Command( async () =>
                {
                    bool ok = await signInByREST(this.Username, this.Password);
                    if (ok)
                    {
                        App.username = this.Username;
                        MessagingCenter.Send(this, NativeNavigationArgs.NativeNavigationMessage, new NativeNavigationArgs("main"));
                    }
                    else
                    {
                        DialogService.ShowError("Verification Failure");
                    }
                }));
            }
        }

        private async Task ExecuteSignInUserCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            //DialogService.ShowLoading(Strings.SigningIn);
            //MessagingCenter.Send(App.currentPage, NativeNavigationArgs.NativeNavigationMessage, new NativeNavigationArgs("main"));
        }
        
        /*
        private async Task<bool> SignIn()
        {
            var account = new Account
            {
                Username = Username,
                Password = Password
            };

            return await AccountService.Instance.Login(account);
        }
        */

        private void NavigateToMainUI()
        {
            //App.Current.MainPage = App.FetchMainUI();

        }

        public async Task<bool> signInByREST(string username, string password)
        {
            String json = "{" 
                + "\"username\":" + "\"" + username + "\","
                + "\"password\":" + "\"" + password + "\""
                + "}";
            string respJson = await MakePostRequestByClient("http://joi-testp.chinacloudapp.cn:8090/auth/login", json);
            JObject resp = JObject.Parse(respJson);
            string respV = (string) resp.GetValue("response");
            if (respV.Equals("success"))
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
            HttpResponseMessage response = await client.PostAsync("http://joi-testp.chinacloudapp.cn:8090/auth/login", content);
            return await response.Content.ReadAsStringAsync();
        }


    }




}
