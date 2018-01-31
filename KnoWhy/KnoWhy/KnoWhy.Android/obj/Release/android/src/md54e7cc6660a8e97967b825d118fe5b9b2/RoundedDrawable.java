package md54e7cc6660a8e97967b825d118fe5b9b2;


public class RoundedDrawable
	extends android.graphics.drawable.Drawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getOpacity:()I:GetGetOpacityHandler\n" +
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"n_setAlpha:(I)V:GetSetAlpha_IHandler\n" +
			"n_setColorFilter:(Landroid/graphics/ColorFilter;)V:GetSetColorFilter_Landroid_graphics_ColorFilter_Handler\n" +
			"";
		mono.android.Runtime.register ("KnoWhy.Droid.RoundedDrawable, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RoundedDrawable.class, __md_methods);
	}


	public RoundedDrawable ()
	{
		super ();
		if (getClass () == RoundedDrawable.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.RoundedDrawable, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public RoundedDrawable (android.graphics.Bitmap p0, float p1, int p2)
	{
		super ();
		if (getClass () == RoundedDrawable.class)
			mono.android.TypeManager.Activate ("KnoWhy.Droid.RoundedDrawable, KnoWhy.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Bitmap, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public int getOpacity ()
	{
		return n_getOpacity ();
	}

	private native int n_getOpacity ();


	public void draw (android.graphics.Canvas p0)
	{
		n_draw (p0);
	}

	private native void n_draw (android.graphics.Canvas p0);


	public void setAlpha (int p0)
	{
		n_setAlpha (p0);
	}

	private native void n_setAlpha (int p0);


	public void setColorFilter (android.graphics.ColorFilter p0)
	{
		n_setColorFilter (p0);
	}

	private native void n_setColorFilter (android.graphics.ColorFilter p0);

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
