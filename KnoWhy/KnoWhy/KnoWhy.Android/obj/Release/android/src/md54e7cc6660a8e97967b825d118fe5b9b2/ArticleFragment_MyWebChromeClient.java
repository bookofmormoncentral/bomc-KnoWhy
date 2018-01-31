package md54e7cc6660a8e97967b825d118fe5b9b2;


public class ArticleFragment_MyWebChromeClient
	extends android.webkit.WebChromeClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onShowCustomView:(Landroid/view/View;Landroid/webkit/WebChromeClient$CustomViewCallback;)V:GetOnShowCustomView_Landroid_view_View_Landroid_webkit_WebChromeClient_CustomViewCallback_Handler\n" +
			"n_onHideCustomView:()V:GetOnHideCustomViewHandler\n" +
			"n_getVideoLoadingProgressView:()Landroid/view/View;:GetGetVideoLoadingProgressViewHandler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.ArticleFragment+MyWebChromeClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ArticleFragment_MyWebChromeClient.class, __md_methods);
	}


	public ArticleFragment_MyWebChromeClient ()
	{
		super ();
		if (getClass () == ArticleFragment_MyWebChromeClient.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleFragment+MyWebChromeClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ArticleFragment_MyWebChromeClient (md54e7cc6660a8e97967b825d118fe5b9b2.ArticleFragment p0)
	{
		super ();
		if (getClass () == ArticleFragment_MyWebChromeClient.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleFragment+MyWebChromeClient, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "KnoWhy.Droid.ArticleFragment, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onShowCustomView (android.view.View p0, android.webkit.WebChromeClient.CustomViewCallback p1)
	{
		n_onShowCustomView (p0, p1);
	}

	private native void n_onShowCustomView (android.view.View p0, android.webkit.WebChromeClient.CustomViewCallback p1);


	public void onHideCustomView ()
	{
		n_onHideCustomView ();
	}

	private native void n_onHideCustomView ();


	public android.view.View getVideoLoadingProgressView ()
	{
		return n_getVideoLoadingProgressView ();
	}

	private native android.view.View n_getVideoLoadingProgressView ();

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
