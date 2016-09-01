using FaceEmojiClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FaceEmojiClient.View.Xaml
{
    public partial class SignUpPage : ContentPage
    {

        private TapGestureRecognizer cancelButtonTapped;

        public SignUpPage()
        {
            BindingContext = new SignUpViewModel();
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            firstNameEntry.Completed += (object sender, EventArgs e) =>
            {
                lastNameEntry.Focus();
            };
            lastNameEntry.Completed += (object sender, EventArgs e) =>
            {
                lastNameEntry.Focus();
            };
            usernameEntry.Completed += (object sender, EventArgs e) =>
            {
                usernameEntry.Focus();
            };
            passwordEntry.Completed += (object sender, EventArgs e) =>
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
