using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FireSharp.Interfaces;
using CheckIn.Global;
using FireSharp.Response;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CheckIn.Resources.screen
{
    [Activity(Label = "CheckIn", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        FiresharpFunc ffunc = new FiresharpFunc();
        IFirebaseClient cliente;
        string name;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            name = string.Empty;
            cliente = ffunc.conectarse();
            Button btnCheckIn = FindViewById<Button>(Resource.Id.btnCheckIn);
            Button btnCheckInAlmuerzo = FindViewById<Button>(Resource.Id.btnCheckInAlmuerzo);
            Button btnCheckOutAlmuerzo = FindViewById<Button>(Resource.Id.btnCheckOutAlmuerzo);
            Button btnCheckOut = FindViewById<Button>(Resource.Id.btnCheckOut);
            btnCheckIn.Click += BtnCheckIn_Click;
            btnCheckOut.Click += BtnCheckOut_Click;
            btnCheckInAlmuerzo.Click += BtnCheckInAlmuerzo_Click;
            btnCheckOutAlmuerzo.Click += BtnCheckOutAlmuerzo_Click;
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        private async void BtnCheckOutAlmuerzo_Click(object sender, EventArgs e)
        {
            //UPDATE
            Usuario usuarioTemp = new Usuario();
            usuarioTemp.apellido = "Suarez";
            FirebaseResponse response = await cliente.UpdateAsync("Registro/-KHqiOHH5J8YlbUHe-3W", usuarioTemp);
            string body= response.Body;
        }

        private async void BtnCheckInAlmuerzo_Click(object sender, EventArgs e)
        {
            //GET
            //FirebaseResponse response = await cliente.GetAsync("Registro/"+name);
            FirebaseResponse response = await cliente.GetAsync("Registro");
            List<Usuario> lu = new List<Usuario>();
            Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response.Body);
            foreach (var item in jo)
            {
                Usuario usuarioTemp = new Usuario();
                usuarioTemp.nombre = item.Value["nombre"].ToString();
                usuarioTemp.apellido = item.Value["apellido"].ToString();
                usuarioTemp.fecNac = DateTime.Parse(item.Value["fecNac"].ToString());
                usuarioTemp.id = item.Key;
                usuarioTemp.mail = item.Value["mail"].ToString();
                lu.Add(usuarioTemp);
            }
            Console.WriteLine("La lista contiene: " + lu.Count);
            for (int i = 0; i < lu.Count; i++)
            {
                Console.WriteLine(lu[i].nombre +" : " +lu[i].fecNac);
            }

            //foreach (var item in jo)
            //{
            //    //Console.WriteLine(item.Key + "+++"+item.Value);
            //    Newtonsoft.Json.Linq.JObject ju = Newtonsoft.Json.Linq.JObject.Parse(item.Value.ToString());
            //    foreach (var item2 in ju)
            //    {
            //        Console.WriteLine(item2.Key + "--" + item2.Value);
            //    }
            //}
            //Console.WriteLine(jo.Count);
        }

        private async void BtnCheckOut_Click(object sender, EventArgs e)
        {
            //PUT
            DateTime dt = DateTime.Now;
            Usuario user = new Usuario();
            user.nombre = "Kevin";
            user.apellido = "Farías";
            user.fecNac = dt;
            user.mail = "kevin.dfs@gmail.com";
            Guid g = new Guid();
            user.id = g.ToString();
            PushResponse response = await cliente.PushAsync("Registro", user);
            Console.WriteLine(response.Result.name);
            name = response.Result.name;

        }

        private async void BtnCheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                //PUSH
                DateTime dt = DateTime.Now;
                Usuario user = new Usuario();
                user.nombre = "Kevin";
                user.apellido = "Farías";
                user.fecNac = dt;
                user.mail = "kevin.dfs@gmail.com";
                Guid g = new Guid();
                user.id = g.ToString();
                SetResponse response = await cliente.SetAsync("usuario/set", user);
                Usuario result = response.ResultAs<Usuario>();
                Console.WriteLine(result.ToString());
            }
            catch (Exception ex)
            {

                Console.WriteLine("Excepción: " + ex.ToString());
            }

        }
        
    }
}

