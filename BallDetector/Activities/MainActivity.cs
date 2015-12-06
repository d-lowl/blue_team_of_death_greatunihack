using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Nfc;
using Poz1.NFCForms.Droid;
using Poz1.NFCForms.Abstract;

namespace BallDetector.Activities
{
    [Activity(Label = "BallDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int LOGIN_REQUEST = 1;
        public NfcAdapter NFCdevice;
        public NfcForms x;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
            NFCdevice = NfcManager.DefaultAdapter;

            Xamarin.Forms.DependencyService.Register<INfcForms, NfcForms>();
            x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;

            SetContentView(Resource.Layout.Main);

            Button addPlayerButton = FindViewById<Button>(Resource.Id.addPlayerBtn);
            addPlayerButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(LoginActivity));

                StartActivityForResult(intent, LOGIN_REQUEST);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (NFCdevice != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                NFCdevice.EnableForegroundDispatch
                (
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
                    new String[][] {new string[] {
                            NFCTechs.Ndef,
                        },
                        new string[] {
                            NFCTechs.MifareClassic,
                        },
                    }
                );
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            NFCdevice.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            x.OnNewIntent(this, intent);
        }
    }
}