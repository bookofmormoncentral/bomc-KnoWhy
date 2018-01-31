package md54e7cc6660a8e97967b825d118fe5b9b2;


public class MyDialogFragment
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.MyDialogFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyDialogFragment.class, __md_methods);
	}


	public MyDialogFragment ()
	{
		super ();
		if (getClass () == MyDialogFragment.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.MyDialogFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MyDialogFragment (md54e7cc6660a8e97967b825d118fe5b9b2.SettingsActivity p0, int p1)
	{
		super ();
		if (getClass () == MyDialogFragment.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.MyDialogFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "KnoWhy.Droid.SettingsActivity, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

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
