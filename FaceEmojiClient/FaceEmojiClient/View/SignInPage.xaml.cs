using FaceEmojiClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FaceEmojiClient.View.Xaml
{
    public partial class SignInPage : ContentPage
    {
        private TapGestureRecognizer cancelButtonTapped;
        public SignInPage()
        {
            BindingContext = new SignInViewModel();
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };

            cancelButtonTapped = new TapGestureRecognizer();
            cancelButtonTapped.Tapped += (object sender, EventArgs e) =>
            {
                Navigation.PopModalAsync();
            };
            cancelButton.GestureRecognizers.Add(cancelButtonTapped);
        }


    }
}
