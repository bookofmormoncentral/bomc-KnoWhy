package md54e7cc6660a8e97967b825d118fe5b9b2;


public class ChapterViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.ChapterViewHolder, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ChapterViewHolder.class, __md_methods);
	}


	public ChapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ChapterViewHolder.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ChapterViewHolder, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

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
