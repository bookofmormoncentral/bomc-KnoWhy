package md54e7cc6660a8e97967b825d118fe5b9b2;


public class ArticleFragment_MyWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.ArticleFragment+MyWebViewClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ArticleFragment_MyWebViewClient.class, __md_methods);
	}


	public ArticleFragment_MyWebViewClient ()
	{
		super ();
		if (getClass () == ArticleFragment_MyWebViewClient.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleFragment+MyWebViewClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ArticleFragment_MyWebViewClient (android.support.v7.app.AppCompatActivity p0, md54e7cc6660a8e97967b825d118fe5b9b2.ArticleFragment p1)
	{
		super ();
		if (getClass () == ArticleFragment_MyWebViewClient.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleFragment+MyWebViewClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Support.V7.App.AppCompatActivity, Xamarin.Android.Support.v7.AppCompat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null:KnoWhy.Droid.ArticleFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1)
	{
		return n_shouldOverrideUrlLoading (p0, p1);
	}

	private native boolean n_shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1);

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
