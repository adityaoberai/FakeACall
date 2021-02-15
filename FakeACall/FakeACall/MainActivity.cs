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
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

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

            button.Click += async delegate
            {
                button.Text = "Attempting Call";
                await Call();
            };
            button.Text = "Call Successful";
            Thread.Sleep(3000);
            button.Text = "Call Now";

            SetAmbientEnabled();
        }

        private async Task Call()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("https://fakeacall.azurewebsites.net/api/CallNumber", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured\n\n" + ex.Message);
            }
            
        }

    }
}


