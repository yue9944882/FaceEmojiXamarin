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
using Android.Support.V7.Widget;
using FaceEmojiClient.Droid.Pages;
using FaceEmojiClient.Service;
using Android.Graphics;

namespace FaceEmojiClient.Droid
{

    public class AppGlobalData
    {
        public static Context context = null;
        public static Bitmap[] bitmaps = null; 
    }


    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MainPage);
            AppGlobalData.context = this.ApplicationContext;
            DialogService.ShowLoading("Downloading Emojis");

            GridView gridView = FindViewById<GridView>(Resource.Id.grid);
            Button button = FindViewById<Button>(Resource.Id.emojiButton);
            GridAdapter adapter = new GridAdapter(this);
            gridView.SetAdapter(adapter);
            gridView.StretchMode = StretchMode.StretchSpacingUniform;
            gridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                int position = args.Position;
                if(GridAdapter.locks[position] == true)
                {
                    DialogService.ShowError("Loading");
                }
                else
                {
                    //                    DialogService.ShowSuccess("Good");
                    var alert = new AlertDialog.Builder(this);
                    Android.Views.View view = LayoutInflater.Inflate(Resource.Layout.MainPageGrid, null);
                    ImageView iv = view.FindViewById<ImageView>(Resource.Id.imagegrid);
                    iv.SetImageBitmap(GridAdapter.images[position]);
                    alert.SetView(view);
                    alert.Create().Show();

                }
            };



            DialogService.HideLoading();

            Button mineButton = this.FindViewById<Button>(Resource.Id.mineButton);
            Button emojiButton = this.FindViewById<Button>(Resource.Id.emojiButton);
            Button safariButton = this.FindViewById<Button>(Resource.Id.safariButton);

            emojiButton.Click += delegate
            {
                StartActivity(typeof(CameraPageActivity));
            };

            mineButton.Click += delegate
            {
                updateGridView();
            };

            safariButton.Click += delegate
            {
                DialogService.ShowError("To be continued");
            };

        }

        private void updateGridView()
        {
            GridAdapter adapter = new GridAdapter(this);
            GridView gridView = FindViewById<GridView>(Resource.Id.grid);
            gridView.SetAdapter(adapter);
        }



    }
}