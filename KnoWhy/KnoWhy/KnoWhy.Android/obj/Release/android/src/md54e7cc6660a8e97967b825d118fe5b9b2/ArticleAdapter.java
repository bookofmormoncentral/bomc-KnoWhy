package md54e7cc6660a8e97967b825d118fe5b9b2;


public class ArticleAdapter
	extends android.support.v7.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.ArticleAdapter, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ArticleAdapter.class, __md_methods);
	}


	public ArticleAdapter ()
	{
		super ();
		if (getClass () == ArticleAdapter.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleAdapter, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ArticleAdapter (md54e7cc6660a8e97967b825d118fe5b9b2.MainActivity p0)
	{
		super ();
		if (getClass () == ArticleAdapter.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.ArticleAdapter, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "KnoWhy.Droid.MainActivity, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);


	public void onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();

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
