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

namespace BallDetector.Resources.Models
{
    public class Ball
    {
        public string Name // should be unique
        { get; private set; }

        public Ball(string name)
        {
            Name = name;
        }
    }
}