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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.);

            //textView = FindViewById<TextView>(Resource.Id.);

            nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
        }

		protected override void OnResume()
		{
			EnableReadMode ();
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
					ndef.Connect();
					NdefMessage ndefM = ndef.NdefMessage;
					NdefRecord[] rec = ndefM.GetRecords();
					byte[] payload = rec[0].GetPayload();
					textView.Text = System.Text.Encoding.Default.GetString(payload);
				}
				//        NdefMessage[] messages = NfcUtils.getNdefMessages(getIntent());
				//        byte[] payload = messages[0].getRecords()[0].getPayload();
				//        String placeId = new String(payload);              

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

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    if (nfcAdapter != null)
        //    {
        //        var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
        //        nfcAdapter.EnableForegroundDispatch
        //        (
        //            this,
        //            PendingIntent.GetActivity(this, 0, intent, 0),
        //            new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
        //            null
        //        );
        //    }
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    nfcAdapter.DisableForegroundDispatch(this);
        //}

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);

        //    NdefMessage[] messages = null;

        //    var rawMsgs = intent.GetParcelableExtra(NfcAdapter.ExtraNdefMessages).ToArray<Parcelable>();

        //    if (rawMsgs != null)
        //    {
        //        return;
        //    }

        //    if (rawMsgs != null)
        //    {
        //        var x = rawMsgs.ToArray<byte>();
        //        messages = rawMsgs.ToArray();
        //    }

        //    //These next few lines will create a payload (consisting of a string)
        //    // and a mimetype.NFC record are arrays of bytes. 
        //    var payload = Encoding.ASCII.GetBytes(GetRandomHominid());
        //    var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
        //    var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
        //    var ndefMessage = new NdefMessage(new[] { apeRecord });

        //    if (!TryAndWriteToTag(tag, ndefMessage))
        //    {
        //        // Maybe the write couldn't happen because the tag wasn't formatted?
        //        TryAndFormatTagWithMessage(tag, ndefMessage);
        //    }
        //}
    }
}