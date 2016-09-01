using FaceEmojiClient.View.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace FaceEmojiClient
{
    public class CameraPageApp : Application
    {
        public CameraPageApp()
        {
            MainPage = new CameraPage();
        }
    }
}
