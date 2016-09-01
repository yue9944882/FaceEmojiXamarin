using System;
using System.IO;
using Android.App;

using Android.Graphics;

[assembly: Xamarin.Forms.Dependency(typeof(FaceEmojiClient.Droid.ScreenshotServiceAndroid))]
namespace FaceEmojiClient.Droid
{
    public class ScreenshotServiceAndroid : FaceEmojiClient.Service.ScreenshotService
    {
        public static Activity Activity { get; set; }

        public byte[] CaptureScreen()
        {
            Activity Activity = Xamarin.Forms.Forms.Context as Activity;
            var view = Activity.Window.PeekDecorView();
            view.DrawingCacheEnabled = true;
            var bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }
    }
}