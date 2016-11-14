using System;
using Android.App;
using Android.Content;
using Android.OS;
using RadiusNetworks.IBeaconAndroid;
using CoffeeBreak.Droid;
using CoffeeBreak;
using SQLite;
using System.Linq;
using System.IO;
using Android.Widget;
using System.Net;
using System.Collections.Specialized;
using Android.Util;
using Android.Bluetooth;

namespace BeaconService
{
    [Service]
    public class BeaconService : Service, IBeaconConsumer
    {
        //System.Timers.Timer timer1;
        //private User username;
        string domain = "http://asknet.ddns.net/";

        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        bool statoInizialeBT;
        private const string UUID = "ACFD065E-C3C0-11E3-9BBE-1A514932AC01";
        private const string monkeyId = "Monkey";

        private static MonitorNotifier _monitorNotifier;
        private static RangeNotifier _rangeNotifier;

        private static Region _monitoringRegion; // = new Region(monkeyId, UUID, (Java.Lang.Integer)101, null);
        private static Region _rangingRegion; //= new Region(monkeyId, UUID, (Java.Lang.Integer)101, null);


        private IBeaconManager _iBeaconManager = IBeaconManager.GetInstanceForApplication(Application.Context);

        int _previousProximity;



        public BeaconService()
        {
            _monitorNotifier = new MonitorNotifier();
            _rangeNotifier = new RangeNotifier();

            _monitoringRegion = new Region(monkeyId, UUID, null, null);

            //_iBeaconManager.SetForegroundBetweenScanPeriod(3000); // Effettua scan ogni 3000 millisecondi

            _iBeaconManager.SetMonitorNotifier(_monitorNotifier);
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Log.Debug("BEACON", "Sono sull'OnCreate");

            //_monitorNotifier = new MonitorNotifier();
            //_rangeNotifier = new RangeNotifier();

            _rangingRegion = new Region(monkeyId, UUID, null, null);


            _iBeaconManager.Bind((IBeaconConsumer)this);

            _monitorNotifier.EnterRegionComplete += EnteredRegion;
            _monitorNotifier.ExitRegionComplete += ExitedRegion;

            _rangeNotifier.DidRangeBeaconsInRegionComplete += this.RangingBeaconsInRegion;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug("BEACON", "Sono nell'OnStartCommand");


            // Controlla che il bluetooth sia attivo
            statoInizialeBT = bluetoothAdapter.IsEnabled;
            if (!statoInizialeBT)
            {
                //Se quando parte il servizio il BT è spento, ACCENDILO
                CreaNotifica("Attenzione", "Attiva prima il BT sul tuo dispositivo");
                SendMessage("attiva prima il Bluetooth sul tuo telefono");
                StopService(intent);
                System.Environment.Exit(0);
            }

            return StartCommandResult.NotSticky;
        }



        //public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        //{
        //    Log.Debug("BEACON", "Sono nell'OnStartCommand");

        //    #region ===== Invia notifica =====
        //    timer1 = new System.Timers.Timer();
        //    timer1.Elapsed += new System.Timers.ElapsedEventHandler(NotificaTemporizzata);
        //    timer1.Interval = 8000; // in miliseconds
        //    timer1.Enabled = true;
        //    #endregion =======================


        //    _iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

        //    _monitorNotifier.EnterRegionComplete += EnteredRegion;
        //    _monitorNotifier.ExitRegionComplete += ExitedRegion;
        //    _rangeNotifier.DidRangeBeaconsInRegionComplete += this.RangingBeaconsInRegion;

        //    _iBeaconManager.SetMonitorNotifier(_monitorNotifier);
        //    _iBeaconManager.SetRangeNotifier(_rangeNotifier);

        //    _monitoringRegion = new Region(monkeyId, UUID, null, null);
        //    _rangingRegion = new Region(monkeyId, UUID, null, null);

        //    //-----  Si deve fare il Bind  e quindi avviare "StartMonitoringBeaconsInRegion"
        //    _iBeaconManager.Bind(this);
        //    _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
        //    _iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
        //    //------------------------------------------------------------------------------




        //    String username = "Pippo";
        //    Uri address = new Uri("http://asknet.ddns.net/CoffeeBreakService.asmx/UpdateMessage");
        //    NameValueCollection nameValueCollection = new NameValueCollection();
        //    nameValueCollection["u"] = username;
        //    var webClient = new WebClient();
        //    try
        //    {
        //        webClient.UploadValues(address, "POST", nameValueCollection);
        //    }
        //    catch
        //    {
        //        // Mostrare messaggio d'errore. Non è stato possibile raggiungere il Client
        //    }

        //    return StartCommandResult.NotSticky;
        //}




        public void OnIBeaconServiceConnect()
        {
            Log.Debug("BEACON", "Sono nell'OnIBeaconServiceConnect");

            _iBeaconManager.SetMonitorNotifier(_monitorNotifier);
            _iBeaconManager.SetRangeNotifier(_rangeNotifier);

            _iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
            _iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
        }     



        public override void OnDestroy()
        {
            Log.Debug("BEACON", "Sono sull'OnDestroy");
            //base.OnDestroy();
            //timer1.Dispose();
            //timer1 = null;
            ////this.ApplicationContext.StopService(new Intent(this, typeof(BeaconService)));
        }



        public override IBinder OnBind(Intent intent)
        {
            // This example isn't of a bound service, so we just return NULL.
            return null;
        }



        //private void NotificaTemporizzata(object sender, EventArgs e)
        //{
        //    CreaNotifica();
        //}



        //public void CreaNotifica()
        //{
        //    // Instantiate the builder and set notification elements:
        //    Notification.Builder builder = new Notification.Builder(this)
        //        .SetContentTitle("Sample Notification")
        //        .SetContentText("Time: " + DateTime.Now)
        //        .SetDefaults(NotificationDefaults.Sound)
        //        .SetSmallIcon(Resource.Drawable.Icon);

        //    builder.SetPriority((int)NotificationPriority.High);

        //    // Build the notification:
        //    Notification notification = builder.Build();

        //    // Get the notification manager:
        //    NotificationManager notificationManager =
        //        GetSystemService(Context.NotificationService) as NotificationManager;

        //    // Publish the notification:
        //    const int notificationId = 0;
        //    notificationManager.Notify(notificationId, notification);
        //}


    //    public class User
    //{
    //    public User()
    //    {

    //    }

    //    [PrimaryKey, AutoIncrement]
    //    public int ID { get; set; }
    //    public string username { get; set; }
    //}



        void RangingBeaconsInRegion(object sender, RangeEventArgs e)
        {
            Log.Debug("BEACON", "Sono dentro RangingBeaconsInRegion");

            ////---------------
            //String username = "IANO";
            //Uri address = new Uri("http://asknet.ddns.net/CoffeeBreakService.asmx/UpdateMessage");
            //NameValueCollection nameValueCollection = new NameValueCollection();
            //nameValueCollection["u"] = username;
            //var webClient = new WebClient();
            //webClient.UploadValues(address, "POST", nameValueCollection);
            //// --------------

            if (e.Beacons.Count > 0)
            {
                var beacon = e.Beacons.FirstOrDefault();
                var message = string.Empty;
                //username = Utility.GetUser(GetConnection());
                String username = "Giuseppe";


                switch ((ProximityType)beacon.Proximity)
                {
                    case ProximityType.Immediate:
                        //UpdateDisplay("Ciao " + username.username + ", ho trovato un beacon! \n E' MOLTO vicino\n\nRssi: " + (ProximityType)beacon.Rssi, Xamarin.Forms.Color.Green);
                        //bool send = DependencyService.Get<ISendMail> ().Send();
                        SendMessage("bene, sei molto vicino a me!");

                        
                        //					if(isFirstTime || (DateTime.Now - startTime).TotalSeconds > 10) 
                        //					{
                        //						isFirstTime = false;
                        //						ShowNotification();
                        //						startTime = DateTime.Now;
                        //					break;
                        //					}
                        break;

                    case ProximityType.Near:
                        //UpdateDisplay("Ho trovato un beacon! \n E' vicino\n\nRssi: " + (ProximityType)beacon.Rssi, Xamarin.Forms.Color.Yellow);
                        SendMessage("avvicinati di più, non essere timido!");
                        break;

                    case ProximityType.Far:
                        //UpdateDisplay("Ho trovato un beacon! \n E' lontano\n\nRssi: " + (ProximityType)beacon.Rssi, Xamarin.Forms.Color.Blue);
                        break;
                    case ProximityType.Unknown:
                        //UpdateDisplay("Non sono sicuro che ci sia un Beacon nelle vicinanze.\n\nRssi: " + (ProximityType)beacon.Rssi, Xamarin.Forms.Color.Red);
                        break;
                }

                _previousProximity = beacon.Proximity;
            }
        }


        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "db_coffeeBreak";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);


            // creo la connection
            var conn = new SQLiteConnection(path);

            // Restituisco la connessione al DB
            return conn;
        }



        void EnteredRegion(object sender, MonitorEventArgs e)
        {
            Log.Debug("BEACON", "Sono entrato nell'EnteredRegion");
        }

        void ExitedRegion(object sender, MonitorEventArgs e)
        {
            //RunOnUiThread(() => Toast.MakeText(this, "No beacons visible", ToastLength.Short).Show());
            SendMessage("sei fuori dalla mia portata");
        }


        public void SendMessage(string msg)
        {
            String username = "Giuseppe";
            //---------------
            Uri address = new Uri(domain + "CoffeeBreakService.asmx/UpdateMessage");
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection["usrcfgsend"] = username + ", " + msg;
            var webClient = new WebClient();
            webClient.UploadValues(address, "POST", nameValueCollection);
            // --------------
        }



        public void CreaNotifica(string title, string msg)
        {
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle(title)
                .SetContentText(msg)
                .SetDefaults(NotificationDefaults.Sound)
                .SetSmallIcon(Resource.Drawable.Icon);

            builder.SetPriority((int)NotificationPriority.High);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }








}