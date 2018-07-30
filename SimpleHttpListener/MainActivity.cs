using Android.App;
using Android.OS;
using System.Net;
using System;
using System.Text;

namespace SimpleHttpListener
{
    [Activity(Label = "SimpleHttpListener", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private HttpListener _listener;
        private const string TurnOnCameraController = "TurnOnCamera";
        private const string TurnOffCameraController = "TurnOffCamera";
        private const string TestCameraController = "TestCamera";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            SetupListener();
        }

        private void SetupListener()
        {
            if(!HttpListener.IsSupported)
            {
                return;
            }

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://+:1616/{TurnOnCameraController}/");
            _listener.Prefixes.Add($"http://+:1616/{TurnOffCameraController}/");
            _listener.Prefixes.Add($"http://+:1616/{TestCameraController}/");
            _listener.Start();
            _listener.BeginGetContext(HandleRequest, _listener);
        }

        private void HandleRequest(IAsyncResult result)
        {
            HttpListenerContext context = _listener.EndGetContext(result);
            string response = null;

            string url = context.Request.Url.ToString();

            if(url.Contains(TurnOnCameraController))
            {
                response = "<html><h1>The camera has been turned on.</h1></html>";
            }
            else if (url.Contains(TurnOffCameraController))
            {
                response = "<html><h1>The camera has been turned off.</h1></html>";
            }
            else if (url.Contains(TestCameraController))
            {
                response = "<html><h1>The camera is working as expected.</h1></html>";
            }
            
            byte[] buffer = Encoding.UTF8.GetBytes(response);

            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();

            _listener.BeginGetContext(HandleRequest, _listener);
        }
    }
}

