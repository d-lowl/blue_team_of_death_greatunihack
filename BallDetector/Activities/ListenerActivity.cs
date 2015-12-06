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

            SetContentView(Resource.Layout.);

            textView = FindViewById<TextView>(Resource.Id.);

            nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (nfcAdapter != null)
            {
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                nfcAdapter.EnableForegroundDispatch
                (
                    this,
                    PendingIntent.GetActivity(this, 0, intent, 0),
                    new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
                    null
                );
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            nfcAdapter.DisableForegroundDispatch(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            NdefMessage[] messages = null;

            var rawMsgs = intent. .GetParcelableExtra(NfcAdapter.ExtraNdefMessages).ToArray<Parcelable>();

            if (rawMsgs != null)
            {
                return;
            }

            if (rawMsgs != null)
            {
                var x = rawMsgs.ToArray<byte>();
                messages = rawMsgs.ToArray();
            }

            //These next few lines will create a payload (consisting of a string)
            // and a mimetype.NFC record are arrays of bytes. 
            var payload = Encoding.ASCII.GetBytes(GetRandomHominid());
            var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
            var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
            var ndefMessage = new NdefMessage(new[] { apeRecord });

            if (!TryAndWriteToTag(tag, ndefMessage))
            {
                // Maybe the write couldn't happen because the tag wasn't formatted?
                TryAndFormatTagWithMessage(tag, ndefMessage);
            }
        }
    }
}