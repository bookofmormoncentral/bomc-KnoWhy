package md54e7cc6660a8e97967b825d118fe5b9b2;


public class EmptyRecyclerView_ListObserver
	extends android.support.v7.widget.RecyclerView.AdapterDataObserver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:()V:GetOnChangedHandler\n" +
			"n_onItemRangeInserted:(II)V:GetOnItemRangeInserted_IIHandler\n" +
			"n_onItemRangeRemoved:(II)V:GetOnItemRangeRemoved_IIHandler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.EmptyRecyclerView+ListObserver, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EmptyRecyclerView_ListObserver.class, __md_methods);
	}


	public EmptyRecyclerView_ListObserver ()
	{
		super ();
		if (getClass () == EmptyRecyclerView_ListObserver.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.EmptyRecyclerView+ListObserver, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public EmptyRecyclerView_ListObserver (md54e7cc6660a8e97967b825d118fe5b9b2.EmptyRecyclerView p0)
	{
		super ();
		if (getClass () == EmptyRecyclerView_ListObserver.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.EmptyRecyclerView+ListObserver, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "KnoWhy.Droid.EmptyRecyclerView, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onChanged ()
	{
		n_onChanged ();
	}

	private native void n_onChanged ();


	public void onItemRangeInserted (int p0, int p1)
	{
		n_onItemRangeInserted (p0, p1);
	}

	private native void n_onItemRangeInserted (int p0, int p1);


	public void onItemRangeRemoved (int p0, int p1)
	{
		n_onItemRangeRemoved (p0, p1);
	}

	private native void n_onItemRangeRemoved (int p0, int p1);

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
