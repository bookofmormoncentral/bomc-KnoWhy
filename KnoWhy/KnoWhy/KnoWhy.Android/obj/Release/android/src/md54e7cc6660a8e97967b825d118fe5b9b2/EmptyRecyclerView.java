package md54e7cc6660a8e97967b825d118fe5b9b2;


public class EmptyRecyclerView
	extends android.support.v7.widget.RecyclerView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setAdapter:(Landroid/support/v7/widget/RecyclerView$Adapter;)V:GetSetAdapter_Landroid_support_v7_widget_RecyclerView_Adapter_Handler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.EmptyRecyclerView, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EmptyRecyclerView.class, __md_methods);
	}


	public EmptyRecyclerView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == EmptyRecyclerView.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.EmptyRecyclerView, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public EmptyRecyclerView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == EmptyRecyclerView.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.EmptyRecyclerView, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public EmptyRecyclerView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == EmptyRecyclerView.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.EmptyRecyclerView, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void setAdapter (android.support.v7.widget.RecyclerView.Adapter p0)
	{
		n_setAdapter (p0);
	}

	private native void n_setAdapter (android.support.v7.widget.RecyclerView.Adapter p0);

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
