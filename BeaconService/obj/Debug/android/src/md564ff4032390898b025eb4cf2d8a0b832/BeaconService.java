package md564ff4032390898b025eb4cf2d8a0b832;


public class BeaconService
	extends android.app.Service
	implements
		mono.android.IGCUserPeer,
		com.radiusnetworks.ibeacon.IBeaconConsumer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"n_onStartCommand:(Landroid/content/Intent;II)I:GetOnStartCommand_Landroid_content_Intent_IIHandler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"n_getApplicationContext:()Landroid/content/Context;:GetGetApplicationContextHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_bindService:(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z:GetBindService_Landroid_content_Intent_Landroid_content_ServiceConnection_IHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_onIBeaconServiceConnect:()V:GetOnIBeaconServiceConnectHandler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"n_unbindService:(Landroid/content/ServiceConnection;)V:GetUnbindService_Landroid_content_ServiceConnection_Handler:RadiusNetworks.IBeaconAndroid.IBeaconConsumerInvoker, Android-iBeacon-Service\n" +
			"";
		mono.android.Runtime.register ("BeaconService.BeaconService, BeaconService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BeaconService.class, __md_methods);
	}


	public BeaconService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BeaconService.class)
			mono.android.TypeManager.Activate ("BeaconService.BeaconService, BeaconService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate ()
	{
		n_onCreate ();
	}

	private native void n_onCreate ();


	public int onStartCommand (android.content.Intent p0, int p1, int p2)
	{
		return n_onStartCommand (p0, p1, p2);
	}

	private native int n_onStartCommand (android.content.Intent p0, int p1, int p2);


	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();


	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);


	public android.content.Context getApplicationContext ()
	{
		return n_getApplicationContext ();
	}

	private native android.content.Context n_getApplicationContext ();


	public boolean bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2)
	{
		return n_bindService (p0, p1, p2);
	}

	private native boolean n_bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2);


	public void onIBeaconServiceConnect ()
	{
		n_onIBeaconServiceConnect ();
	}

	private native void n_onIBeaconServiceConnect ();


	public void unbindService (android.content.ServiceConnection p0)
	{
		n_unbindService (p0);
	}

	private native void n_unbindService (android.content.ServiceConnection p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
