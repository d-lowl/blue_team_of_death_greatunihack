using System;
using System.Text;
using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Android.Runtime;

namespace BallDetector.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity //, NfcAdapter.ICreateNdefMessageCallback, NfcAdapter.IOnNdefPushCompleteCallback
    {
        private NfcAdapter nfcAdapter;
        private TextView textView;
        private bool inReadMode;
        private Button readTagButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginPage);

            textView = FindViewById<TextView>(Resource.Id.textView);
            // Check for available NFC Adapter
            nfcAdapter = NfcAdapter.GetDefaultAdapter(this);

            readTagButton = FindViewById<Button>(Resource.Id.readTagBtn);
            readTagButton.Click += ReadTagButtonOnClick;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (inReadMode)
            {

                if (intent == null) return;

                textView.Text = intent.ToString();


                // These next few lines will create a payload (consisting of a string)
                // and a mimetype. NFC record are arrays of bytes. 
                //var payload = Encoding.ASCII.GetBytes(GetRandomHominid());
                //var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
                //var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
                //var ndefMessage = new NdefMessage(new[] { apeRecord });

                //if (!TryAndWriteToTag(tag, ndefMessage))
                //{
                //    // Maybe the write couldn't happen because the tag wasn't formatted?
                //    TryAndFormatTagWithMessage(tag, ndefMessage);
                //}             
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            // App is paused, so no need to keep an eye out for NFC tags.
            if (nfcAdapter != null)
                nfcAdapter.DisableForegroundDispatch(this);
        }

        private void DisplayMessage(string message)
        {
            textView.Text = message;
        }

        private void ReadTagButtonOnClick(object sender, EventArgs eventArgs)
        {
            var view = (View)sender;
            if (view.Id == Resource.Id.readTagBtn)
            {
                DisplayMessage("Touch and hold the tag against the phone to write.");
                EnableReadMode();
            }
        }

        private void EnableReadMode()
        {
            inReadMode = true;
            // Create an intent filter for when an NFC tag is discovered.  When
            // the NFC tag is discovered, Android will u
            var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };

            // When an NFC tag is detected, Android will use the PendingIntent to come back to this activity.
            // The OnNewIntent method will invoked by Android.
            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            if (nfcAdapter == null)
            {
                var alert = new AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.SetButton("OK", delegate
                {
                    readTagButton.Enabled = false;
                    textView.Text = "NFC is not supported on this device.";
                });
                alert.Show();
            }
            else
                nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        }
    }
}