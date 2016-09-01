using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FaceEmojiClient.View.Xaml
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();

            SetUpUserInterface();
            SetupEventHandler();
        }

        private void SetUpUserInterface()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SetupEventHandler()
        {
            signUpButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushModalAsync(new SignUpPage());
            };

            signInButton.Clicked += (object sender, EventArgs e) => {
                Navigation.PushModalAsync(new SignInPage());
            };
        }


    }
}
