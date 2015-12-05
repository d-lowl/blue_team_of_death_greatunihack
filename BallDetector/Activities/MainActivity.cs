using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BallDetector.Activities
{
    [Activity(Label = "BallDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string deviceName;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //TODO: LOGIC TO DETECT IF A DEVICE WAS NAMED

            if(string.IsNullOrEmpty(deviceName)) //device wasn't named
            {
                // Set our view from the RegisterName layout resource
                //SetContentView(Resource.Layout.RegPrompt);
                //Button confirmNameBtn = FindViewById<Button>(Resource.Id.confirmNameBtn);
                //confirmNameBtn.Click += ConfirmNameBtn_Click;

                
            }
        }

        private void ConfirmNameBtn_Click(object sender, EventArgs e)
        {
            //if name is unique navigate to other page
        }
    }
}

