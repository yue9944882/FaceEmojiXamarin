using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FaceEmojiClient.Service;
using System.IO;
using System.Net;
using Android.Provider;
using Android.Content.PM;
using Android.Graphics;
using Android.Views.Animations;

namespace FaceEmojiClient.Droid
{

    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }
    }


    [Activity(Label = "CameraPageActivity")]
    public class CameraPageActivity : Activity
    {
        /*
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ScreenshotServiceAndroid.Activity = this;
            // Create your application here
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new CameraPageApp());
        }
        */


        private ImageView _imageView;

        private string imgFileName = "tmp_save.png";

        public static class App
        {
            public static Java.IO.File _file;
            public static Java.IO.File _dir;
            public static Bitmap bitmap;
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TakePhoto);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button pButton = FindViewById<Button>(Resource.Id.photoButton);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView);
                pButton.Click += TakeAPicture;
                Button cButton = FindViewById<Button>(Resource.Id.continueButton);
                cButton.Click += (object sender, EventArgs args) => 
                {
                    UploadImg(sender, args);
                    Finish(); 
                };
                TakeAPicture(this, null);
            }
        }

        private void CreateDirectoryForPictures()
        {
            App._dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();

            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }

        private void UploadImg(object sender, EventArgs eventArgs)
        {
            ContextWrapper cw = new ContextWrapper(this.ApplicationContext);
            Java.IO.File directory = cw.GetDir("imgDir", FileCreationMode.WorldReadable);
            Java.IO.File myPath = new Java.IO.File(directory, this.imgFileName);
            System.IO.FileStream imgFile = new FileStream(myPath.AbsolutePath, FileMode.Open);
            /*
            BinaryReader br = new BinaryReader(imgFile);
            long numBytes = new FileInfo(this.imgFileName).Length;
            byte[] buff = null;
            buff = br.ReadBytes((int) numBytes);
            */
            DialogService.ShowLoading("Uploading");
            try
            {
                RESTService.UploadFilesToRemoteUrl("http://joi-testp.chinacloudapp.cn:8090/image/upload/jinmin", imgFile);
            }
            catch (WebException e)
            {
            }
            DialogService.HideLoading();
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);

            // Save the bitmap to a file 
            ContextWrapper cw = new ContextWrapper(this.ApplicationContext);
            Java.IO.File directory = cw.GetDir("imgDir", FileCreationMode.Private);
            Java.IO.File myPath = new Java.IO.File(directory, this.imgFileName);
            try
            {
                using (var os = new System.IO.FileStream(myPath.AbsolutePath, System.IO.FileMode.Create))
                {
                    Matrix matrix = new Matrix();
                    matrix.SetRotate(90);
                    Bitmap scaledBitmap = Bitmap.CreateScaledBitmap(App.bitmap, App.bitmap.Height, App.bitmap.Width/2, true);
                    Bitmap rotatedBitmap = Bitmap.CreateBitmap(scaledBitmap, 0, 0, scaledBitmap.Width, scaledBitmap.Height, matrix, true);
                    App.bitmap = rotatedBitmap;
                    App.bitmap.Compress(Bitmap.CompressFormat.Png, 100, os);
                    os.Close();
                }
            }
            catch (Exception ex) {
                System.Console.Write(ex.Message);
            }

            // Dispose of the Java side bitmap.
            GC.Collect();
            try
            {
                System.IO.FileStream imgFile = new FileStream(myPath.AbsolutePath, FileMode.Open);
                int b = imgFile.ReadByte();
                imgFile.Close();
            }
            catch (Java.IO.IOException e)
            {
            }
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
        }
    }
}