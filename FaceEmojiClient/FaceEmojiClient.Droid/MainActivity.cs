using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Xamarin.Forms;
using FaceEmojiClient.Message;
using FaceEmojiClient.View.Xaml;
using FaceEmojiClient.ViewModel;
using FaceEmojiClient.Control;

namespace FaceEmojiClient.Droid
{


    [Activity(Label = "FaceEmojiClient", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        public static int widthInDp = 0;
        public static int columnInDp = 0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            UserDialogs.Init(this);
            MessagingCenter.Subscribe<WelcomePage, NativeNavigationArgs>(
                            App.currentPage,
                            NativeNavigationArgs.NativeNavigationMessage,
                            HandleNativeNavigationMessage
            );
            MessagingCenter.Subscribe<SignInViewModel, NativeNavigationArgs>(
                            App.currentPage,
                            NativeNavigationArgs.NativeNavigationMessage,
                            HandleNativeNavigationMessageSignInViewModel
            );
            MessagingCenter.Subscribe<DrawEmojiPage, NativeNavigationArgs>(
                            App.currentPage,
                            NativeNavigationArgs.NativeNavigationMessage,
                            HandleNativeNavigationMessageDrawEmoji            
                            );
            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }

        private void HandleNativeNavigationMessage(WelcomePage sender, NativeNavigationArgs args)
        {
            StartActivity(typeof(MainPageActivity));
        }

        private void HandleNativeNavigationMessageSignInViewModel(SignInViewModel sender, NativeNavigationArgs args)
        {
            StartActivity(typeof(MainPageActivity));
        }
        private void HandleNativeNavigationMessageDrawEmoji(DrawEmojiPage sender, NativeNavigationArgs args)
        {
            StartActivity(typeof(MainPageActivity));
        }
    }
}

