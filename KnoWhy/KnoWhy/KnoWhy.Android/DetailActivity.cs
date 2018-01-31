using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KnoWhy.Model;
using Android.Webkit;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics;
using Android.Util;

namespace KnoWhy.Droid
{
    [Activity(Label = "DetailActivity", Theme = "@style/Theme.DesignDemo", ParentActivity = typeof(KnoWhyActivity), HardwareAccelerated = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class DetailActivity : KnoWhyActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Detail);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return false;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }


    }
}