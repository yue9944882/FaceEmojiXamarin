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
using JavaScriptCore;
using Newtonsoft.Json.Linq;
using FaceEmojiClient.Control;
using Android.Graphics;
using System.Net;
using System.Threading.Tasks;
using FaceEmojiClient.Service;

namespace FaceEmojiClient.Droid.Pages
{
    public class GridAdapter : BaseAdapter
    {
        Context context;

        public GridAdapter(Context c)
        {
            Items = RESTService.getMyPhoto(App.username);
            images = new Bitmap[Items.Length];
            locks = new bool[Items.Length];
            context = c;
            updatePhotosItems();
            DownloadOrUpdateImages();
        }

        public void updatePhotosItems()
        {
            DialogService.ShowLoading("Downloading");
            Items = RESTService.getMyPhoto("jinmin");
            int len = Items.Length;
            for(int i = 0; i < len; i++)
            {
                locks[i] = true; 

            }
            DownloadOrUpdateImages();
            NotifyDataSetChanged();
            DialogService.HideLoading();
        }


        public static string[] Items { get; set; }
        public static Bitmap[] images { get; set; }
        public static bool[] locks { get; set; }

        public override int Count
        {
            get { return Items.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        // create a new ImageView for each item referenced by the Adapter
        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            
            if (convertView == null) {
                ImageView imageView = new ImageView(context);
                var imageBitmap = GetImageBitmapFromUrl(Items[position]);
                imageView.SetImageBitmap(images[position]);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.LayoutParameters = new GridView.LayoutParams(450, 500);
                convertView = imageView;
            }; 
            return convertView;
        }

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }

        public static Task<Bitmap> GetImageBitmapFromUrlTask(string url)
        {
            return Task.Run(() =>
            {
                Bitmap imageBitmap = null;
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            });
        }

        public static async void DownloadOrUpdateImagesAsync()
        {
            for(int i = 0; i < Items.Length; i++)
            {
                Bitmap b = await GetImageBitmapFromUrlTask(Items[i]);
                images[i] = b;
                locks[i] = false;
            }
        }

        public static void DownloadOrUpdateImages()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Bitmap b = GetImageBitmapFromUrl(Items[i]);
                images[i] = b;
                locks[i] = false;
            }
        }

    }
}