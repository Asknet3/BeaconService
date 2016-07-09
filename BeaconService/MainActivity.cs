﻿
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using RadiusNetworks.IBeaconAndroid;
using CoffeeBreak.Droid;

namespace BeaconService
{
    [Activity(Label = "BeaconService", MainLauncher = true, Icon = "@drawable/icon",UiOptions = Android.Content.PM.UiOptions.None, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class MainActivity : Activity
    {
        //private const string UUID = "ACFD065E-C3C0-11E3-9BBE-1A514932AC01";
        //private const string monkeyId = "Monkey";
        public MainActivity()
        {
            
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            // Controllo se il servizio è già presente e avviato
            var manager = (ActivityManager)GetSystemService(ActivityService);
            System.Collections.Generic.IList<ActivityManager.RunningServiceInfo> list = manager.GetRunningServices(int.MaxValue);
            bool serviceExist = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Service.PackageName == "BeaconService.BeaconService")
                {
                    serviceExist = true;
                }
            }

            Button btn_start = FindViewById<Button>(Resource.Id.btn_start);
            Button btn_stop = FindViewById<Button>(Resource.Id.btn_stop);

            Intent service = new Intent(this, typeof(BeaconService));
            //Intent locationService = new Intent(this, typeof(LocationService));

            if (serviceExist)
            {
                btn_start.Enabled = false;
                btn_stop.Enabled = true;
            }
            else
            {
                btn_start.Enabled = true;
                btn_stop.Enabled = false;
            }

            btn_start.Click += delegate
            {
                StartService(service);
                //StartService(locationService); // Faccio partire il servizio per la localizzazione
                btn_start.Enabled = !btn_start.Enabled;
                btn_stop.Enabled = !btn_stop.Enabled;
            };

            btn_stop.Click += delegate
            {
                StopService(service);
                //StopService(locationService); // Fermo il servizio per la localizzazione
                btn_start.Enabled = !btn_start.Enabled;
                btn_stop.Enabled = !btn_stop.Enabled;
            };
        }
    }
}


