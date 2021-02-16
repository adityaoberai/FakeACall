using System;
using System.Net.Http;
using System.Threading.Tasks;
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
using Android.Views.Animations;
using Java.Interop;

namespace FakeACall
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : WearableActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Text = "Attempting Call";
            if(await Call()==true)
            { 
                button.Text = "Call Successful";
            }
            
            else
            {
                button.Text = "Call Failed";
            }

            CloseApp();

            SetAmbientEnabled();
        }

        private async Task<bool> Call()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("<ADD AZURE FUNCTION URL HERE>", null);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured\n\n" + ex.Message);
                return false;
            }   
        }

        private void CloseApp()
        {
            Handler h = new Handler();
            Action close = () =>
            {
                Process.KillProcess(Process.MyPid());
            };

            h.PostDelayed(close, 3000);
        }
    }
}


