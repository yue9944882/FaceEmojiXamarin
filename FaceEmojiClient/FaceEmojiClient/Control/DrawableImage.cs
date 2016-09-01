using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FaceEmojiClient.Control
{
    public class DrawableImage : Image
    {
        public static readonly BindableProperty CurrentLineColorProperty =
            BindableProperty.Create((DrawableImage w) => w.CurrentLineColor, Color.Default);

        public Color CurrentLineColor
        {
            get
            {
                return (Color)GetValue(CurrentLineColorProperty);
            }
            set
            {
                SetValue(CurrentLineColorProperty, value);
            }
        }
    }
}
