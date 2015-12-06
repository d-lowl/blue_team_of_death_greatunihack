using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Nfc;

namespace BallDetector.Activities
{
    [Activity(Label = "BallDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int LOGIN_REQUEST = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button addPlayerButton = FindViewById<Button>(Resource.Id.addPlayerBtn);
            addPlayerButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(LoginActivity));

                StartActivityForResult(intent, LOGIN_REQUEST);
            };
        }
    }
}