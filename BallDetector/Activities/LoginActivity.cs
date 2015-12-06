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
using Poz1.NFCForms.Abstract;
using Xamarin.Forms;

namespace BallDetector.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity //, NfcAdapter.ICreateNdefMessageCallback, NfcAdapter.IOnNdefPushCompleteCallback
    {
        private NfcAdapter nfcAdapter;
        private TextView textView;
        private Android.Widget.Button readTagButton;
        INfcForms device = DependencyService.Get<INfcForms>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            device.NewTag += Device_NewTag;

            SetContentView(Resource.Layout.LoginPage);

            textView = FindViewById<TextView>(Resource.Id.textView);
            // Check for available NFC Adapter
            //nfcAdapter = NfcAdapter.GetDefaultAdapter(this);

            //readTagButton = FindViewById<Button>(Resource.Id.readTagBtn);
            //readTagButton.Click += ReadTagButtonOnClick;
            // Register callback to set NDEF message
            //nfcAdapter.SetNdefPushMessageCallback(this, this);
            //    // Register callback to listen for message-sent success
            //nfcAdapter.SetOnNdefPushCompleteCallback(this, this);

        }

        private void Device_NewTag(object sender, NfcFormsTag e)
        {
            var msg = e.NdefMessage.ToByteArray();

            var trueMsg = NdefMessage.FromArray(msg);

            //foreach (NdefRecord record in trueMsg)
            //{
            //    Debug.WriteLine("Record type: " + Encoding.UTF8.GetString(record.Type, 0, record.Type.Length));
            //    // Go through each record, check if it's a Smart Poster
            //    if (record.CheckSpecializedType(false) == typeof(NdefSpRecord))
            //    {
            //        // Convert and extract Smart Poster info
            //        var spRecord = new NdefSpRecord(record);
            //        Debug.WriteLine("URI: " + spRecord.Uri);
            //        Debug.WriteLine("Titles: " + spRecord.TitleCount());
            //        Debug.WriteLine("1. Title: " + spRecord.Titles[0].Text);
            //        Debug.WriteLine("Action set: " + spRecord.ActionInUse());
            //    }
            //}
        }

        //public NdefMessage CreateNdefMessage(NfcEvent e)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnNdefPushComplete(NfcEvent e)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    // TODO THIS
        //    base.OnActivityResult(requestCode, resultCode, data);
        //}

        //protected override void OnNewIntent(Intent intent)
        //{
        //    if (inReadMode)
        //    {
        //        //NdefMessage[] messages = null;
        //        //var rawMsgs = intent.GetParcelableExtra(NfcAdapter.ExtraNdefMessages).ToArray<Parcelable>();

        //        //if (rawMsgs != null)
        //        //{
        //        //    return;
        //        //}

        //        //textView.Text = rawMsgs.GetId().ToString();

        //        //if (rawMsgs != null)
        //        //{
        //        //    messages = new NdefMessage[rawMsgs.length];
        //        //    for (int i = 0; i < rawMsgs.length; i++)
        //        //    {
        //        //        messages[i] = (NdefMessage)rawMsgs[i];
        //        //    }
        //        //}

        //        // These next few lines will create a payload (consisting of a string)
        //        // and a mimetype. NFC record are arrays of bytes. 
        //        //var payload = Encoding.ASCII.GetBytes(GetRandomHominid());
        //        //var mimeBytes = Encoding.ASCII.GetBytes(ViewApeMimeType);
        //        //var apeRecord = new NdefRecord(NdefRecord.TnfMimeMedia, mimeBytes, new byte[0], payload);
        //        //var ndefMessage = new NdefMessage(new[] { apeRecord });

        //        //if (!TryAndWriteToTag(tag, ndefMessage))
        //        //{
        //        //    // Maybe the write couldn't happen because the tag wasn't formatted?
        //        //    TryAndFormatTagWithMessage(tag, ndefMessage);
        //        //}
        //    }
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    // App is paused, so no need to keep an eye out for NFC tags.
        //    if (nfcAdapter != null)
        //        nfcAdapter.DisableForegroundDispatch(this);
        //}

        //private void DisplayMessage(string message)
        //{
        //    textView.Text = message;
        //}

        //private void ReadTagButtonOnClick(object sender, EventArgs eventArgs)
        //{
        //    var view = (View)sender;
        //    if (view.Id == Resource.Id.readTagBtn)
        //    {
        //        DisplayMessage("Touch and hold the tag against the phone to write.");
        //        EnableReadMode();
        //    }
        //}

        //private void EnableReadMode()
        //{
        //    inReadMode = true;
        //    // Create an intent filter for when an NFC tag is discovered.  When
        //    // the NFC tag is discovered, Android will u
        //    var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
        //    var filters = new[] { tagDetected };

        //    // When an NFC tag is detected, Android will use the PendingIntent to come back to this activity.
        //    // The OnNewIntent method will invoked by Android.
        //    var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
        //    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

        //    if (nfcAdapter == null)
        //    {
        //        var alert = new AlertDialog.Builder(this).Create();
        //        alert.SetMessage("NFC is not supported on this device.");
        //        alert.SetTitle("NFC Unavailable");
        //        alert.SetButton("OK", delegate {
        //            readTagButton.Enabled = false;
        //            textView.Text = "NFC is not supported on this device.";
        //        });
        //        alert.Show();
        //    }
        //    else
        //        nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
        //}
    }
}