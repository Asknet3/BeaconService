package md564ff4032390898b025eb4cf2d8a0b832;


public class MonitorNotifier
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.radiusnetworks.ibeacon.MonitorNotifier
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_didDetermineStateForRegion:(ILcom/radiusnetworks/ibeacon/Region;)V:GetDidDetermineStateForRegion_ILcom_radiusnetworks_ibeacon_Region_Handler:RadiusNetworks.IBeaconAndroid.IMonitorNotifierInvoker, Android-iBeacon-Service\n" +
			"n_didEnterRegion:(Lcom/radiusnetworks/ibeacon/Region;)V:GetDidEnterRegion_Lcom_radiusnetworks_ibeacon_Region_Handler:RadiusNetworks.IBeaconAndroid.IMonitorNotifierInvoker, Android-iBeacon-Service\n" +
			"n_didExitRegion:(Lcom/radiusnetworks/ibeacon/Region;)V:GetDidExitRegion_Lcom_radiusnetworks_ibeacon_Region_Handler:RadiusNetworks.IBeaconAndroid.IMonitorNotifierInvoker, Android-iBeacon-Service\n" +
			"";
		mono.android.Runtime.register ("BeaconService.MonitorNotifier, BeaconService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MonitorNotifier.class, __md_methods);
	}


	public MonitorNotifier () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MonitorNotifier.class)
			mono.android.TypeManager.Activate ("BeaconService.MonitorNotifier, BeaconService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void didDetermineStateForRegion (int p0, com.radiusnetworks.ibeacon.Region p1)
	{
		n_didDetermineStateForRegion (p0, p1);
	}

	private native void n_didDetermineStateForRegion (int p0, com.radiusnetworks.ibeacon.Region p1);


	public void didEnterRegion (com.radiusnetworks.ibeacon.Region p0)
	{
		n_didEnterRegion (p0);
	}

	private native void n_didEnterRegion (com.radiusnetworks.ibeacon.Region p0);


	public void didExitRegion (com.radiusnetworks.ibeacon.Region p0)
	{
		n_didExitRegion (p0);
	}

	private native void n_didExitRegion (com.radiusnetworks.ibeacon.Region p0);

	private java.util.ArrayList refList;
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
