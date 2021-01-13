using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Wearable.Activity;
using Java.Interop;
using Android.Views.Animations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;

namespace FakeACall
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate {
                const string accountSid = "<Twilio Account SID>";
                const string authToken = "<Twilio Auth Token>";
                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("<Your Phone Number>");
                var from = new PhoneNumber("<Twilio Phone Number>");
                var call = CallResource.Create(to, from,
                    twiml: new Twiml("<Response><Say>Hey there. Hope you're doing well. Well done getting this call running. Use this opportunity to get away from here.</Say></Response>"));

                //Console.WriteLine(call.Sid);
            };

            SetAmbientEnabled();
        }
    }
}


