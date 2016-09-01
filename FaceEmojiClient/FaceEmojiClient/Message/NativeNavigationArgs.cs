using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FaceEmojiClient.Message
{
    public class NativeNavigationArgs
    {
        public string NavigateTo { get; private set; }

        public NativeNavigationArgs(string NavigateTo)
        {
            this.NavigateTo = NavigateTo;
        }

        public const string NativeNavigationMessage = "FaceEmoji.NativeNavigationMessage";

        public const string NativeNavigationToMainMessage = "FaceEmoji.NativeNavigationToMainMessage";

    }

}
