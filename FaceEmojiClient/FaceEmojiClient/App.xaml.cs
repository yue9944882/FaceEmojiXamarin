using FaceEmojiClient.View.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FaceEmojiClient
{
    public partial class App : Application
    {

        public static string username;

        public static ContentPage currentPage;
        public App()
        {
            InitializeComponent();

            WelcomePage welcomePage = new WelcomePage();

            MainPage = welcomePage;

            currentPage = welcomePage;

        }
        /*
        public static NavigationPage FetchMainUI()
        {
            var cameraPage = new CameraPage();
        }
        */

        public static void startCameraPage()
        {
            CameraPage page = new CameraPage();
            App.currentPage.Navigation.PushModalAsync(page);
            currentPage = page;
        }

    }
}
