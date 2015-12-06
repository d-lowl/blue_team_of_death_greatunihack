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
using System.Net;

namespace BallDetector.Server
{
    //http://2guys2balls.azurewebsites.net/api/addPlayer?uname=<>&ballid=<>
    //http://2guys2balls.azurewebsites.net/api/playerScore?ballid=<>

    public static class ServerComs
    {
        public static void SendNameAndBall(string name, string ball)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(
                string.Format("http://2guys2balls.azurewebsites.net/api/addPlayer?uname={0}&ballid={1}", name, ball)));
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            request.GetResponse();
        }

        public static void SendBall(string ball)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(
                string.Format("http://2guys2balls.azurewebsites.net/api/playerScore?ballid={0}", ball)));
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            request.GetResponse();
        }
    }
}