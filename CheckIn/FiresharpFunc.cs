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
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using CheckIn.Global;

namespace CheckIn
{
    public class FiresharpFunc
    {
        public IFirebaseClient conectarse()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "I1Q3f9gu7YOxoluHoPWzx1CBdv1dredgXaofvDWw",
                BasePath = "https://checkinit.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(config);
            return client;
        }
    }
}