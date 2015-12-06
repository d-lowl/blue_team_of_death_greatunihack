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
using Android.Nfc;
using Poz1.NFCForms.Droid;
using Android.Nfc.Tech;

namespace BallDetector.Activities
{
    [Activity(Label = "ListenerActivity")]
    public class ListenerActivity : Activity
    {
        private NfcAdapter nfcAdapter;
        private TextView textView;
        int totalScores = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            time1 = new DateTimeOffset();
            time2 = new DateTimeOffset();

            SetContentView(Resource.Layout.ListeningPage);

            textView = FindViewById<TextView>(Resource.Id.scoreTextView);

            nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
        }

		protected override void OnResume()
		{
            base.OnResume();
			EnableReadMode();
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
		}

		protected override void OnNewIntent(Intent intent)
		{
			if (true)
			{
				if (intent == null) return;

				var tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);

				if(tag == null)
					textView.Text = "intent was null";
				else
				{
					Ndef ndef = Ndef.Get(tag);

                    try
                    {
                        ndef.Connect();
                        NdefMessage ndefM = ndef.NdefMessage;
                        NdefRecord[] rec = ndefM.GetRecords();
                        byte[] payload = rec[0].GetPayload();
                        string sendToServer = System.Text.Encoding.Default.GetString(payload);
                        totalScores += 1;
                        textView.Text = totalScores.ToString();
                        Server.ServerComs.SendBall(sendToServer);
                    }
                    catch (NullReferenceException e)
                    {
                        return;
                    }
				}          
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

		private void EnableReadMode()
		{
			//            inReadMode = true;
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
//						readTagButton.Enabled = false;
						textView.Text = "NFC is not supported on this device.";
					});
				alert.Show();
			}
			else
				nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
		}
    }
}