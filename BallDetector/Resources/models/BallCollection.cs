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

namespace BallDetector.Resources.models
{
    public class BallCollection
    {
        public List<string> balls;
        
        public BallCollection()
        {
            balls = new List<string>();
            // request balls from server
        }

        public bool AddNewBall(string name)
        {
            if (balls.Contains(name))
                return false;

            balls.Add(name);
            return true;
        }
    }
}