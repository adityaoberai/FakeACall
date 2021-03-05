using System;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Activity;

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
                HttpResponseMessage response = await client.PostAsync("https://callfake.azurewebsites.net/api/FakeCall", null);
                return true;
            }
            catch (Exception ex)
            {
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


