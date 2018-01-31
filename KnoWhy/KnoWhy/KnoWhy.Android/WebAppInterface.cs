using System;
using Java.Interop;
using Android.Webkit;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using KnoWhy;

namespace KnoWhy.Droid
{
    public class WebAppInterface : Java.Lang.Object
    {
        ArticleFragment mContext;

        public WebAppInterface(ArticleFragment c) {
            mContext = c;
        }

        [Export]
        [JavascriptInterface]
        public void toggleFavorite(String value)
        {
            //Toast.MakeText(mContext, "sd", ToastLength.Short).Show();
            mContext.toggleFavorites(value);
            return;
        }
    }
}
