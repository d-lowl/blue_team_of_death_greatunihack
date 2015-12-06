using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Nfc;
using System.Collections.Generic;

namespace BallDetector.Activities
{
    [Activity(Label = "BallDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int LOGIN_REQUEST = 1;
        ListView userList;
        ArrayAdapter<string> adapter;
        List<string> users;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            users = new List<string>();

            SetContentView(Resource.Layout.Main);

            userList = FindViewById<ListView>(Resource.Id.playerList);

            Button addPlayerButton = FindViewById<Button>(Resource.Id.addPlayerBtn);
            addPlayerButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(LoginActivity));

                StartActivityForResult(intent, LOGIN_REQUEST);
            };
            
            Button playButton = FindViewById<Button>(Resource.Id.playButton);
            playButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ListenerActivity));

                //foreach (string user in users)
                //{
                //    Server.ServerComs.
                //}

                StartActivity(intent);
            };

  
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == LOGIN_REQUEST&&resultCode==Result.Ok)
            {
                string userName = data.GetStringExtra("Name");

                users.Add(userName);

                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, users.ToArray());

                userList.Adapter = adapter;
            }        
        }
    }
}