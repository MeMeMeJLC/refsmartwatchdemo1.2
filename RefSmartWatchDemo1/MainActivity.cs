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
using Java.Interop;
using Android.Views.Animations;
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace RefSmartWatchDemo1
{
    [Activity(Label = "RefSmartWatchDemo1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.RoundMain);

 /*           var v = FindViewById<WatchViewStub>(Resource.Id.watch_view_stub);
            v.LayoutInflated += delegate
            {*/

                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.gameButton);

                button.Click += async (sender, e) =>
                {
                    Console.Out.WriteLine("button clicked");
                    string url = "http://refwebapiodata20161016090651.azurewebsites.net/odata/Game(1)";

                    JsonValue json = await FetchGameAsync(url);
                };
            //};            
        }

        private async Task<JsonValue> FetchGameAsync(string url)
        {
            Console.Out.WriteLine("fetch game run");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            request.ContentType = "application/json";
            request.Method = "GET";

            //send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // get a stream representation of the HTTP web response 
                using (Stream stream = response.GetResponseStream())
                {
                    //use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    //return the json object
                    return jsonDoc;
                }
            }
        }
    }
}


