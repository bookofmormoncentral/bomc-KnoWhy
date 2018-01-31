package md54e7cc6660a8e97967b825d118fe5b9b2;


public class WebAppInterface
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_toggleFavorite:(Ljava/lang/String;)V:__export__\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.WebAppInterface, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", WebAppInterface.class, __md_methods);
	}


	public WebAppInterface ()
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.WebAppInterface, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public WebAppInterface (md54e7cc6660a8e97967b825d118fe5b9b2.ArticleFragment p0)
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.WebAppInterface, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "KnoWhy.Droid.ArticleFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

	@android.webkit.JavascriptInterface

	public void toggleFavorite (java.lang.String p0)
	{
		n_toggleFavorite (p0);
	}

	private native void n_toggleFavorite (java.lang.String p0);

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
