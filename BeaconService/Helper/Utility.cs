using System;
using SQLite;
using System.Collections;
using static BeaconService.BeaconService;
using System.Collections.Specialized;
using System.Net;
using BeaconService.Helper;
using Android.Util;

namespace CoffeeBreak
{
	public class Utility
	{
		public Utility ()
		{
		}

		public static bool TableExists<T> (SQLiteConnection connection)
		{
			const string cmdText = "SELECT * FROM sqlite_master WHERE type = 'table' AND name = ?";
			var cmd = connection.CreateCommand (cmdText, typeof(T).Name);
			return cmd.ExecuteScalar<string> () != null;
		}



        public static User GetUser(SQLiteConnection connection)
        {
            var query = connection.Table<User>().FirstOrDefault();

            return query;
        }


        public static int SaveUser(SQLiteConnection connection, string usrName)
        {
            var usr = new User { username = usrName };
            return connection.Insert(usr);
        }



        public static void Avvia_Spotify(string domain)
        {
            // *********************************  START AVVIO SPOTIFY  *********************************
            try
            {
                //---------------
                Uri address = new Uri(domain + "CoffeeBreakService.asmx/SendSocket");
                NameValueCollection nameValueCollection = new NameValueCollection();
                var webClient = new WebClient();
                webClient.UploadValues(address, "POST", nameValueCollection);
                // --------------
            }
            catch (Exception x)
            { Log.Debug("BEACON", "Exception: " + x); }
            // *********************************  END AVVIO SPOTIFY  *********************************

        }



    }
}

