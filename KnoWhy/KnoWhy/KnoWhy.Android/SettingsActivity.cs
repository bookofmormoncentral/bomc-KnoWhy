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
using Android.Graphics;
using KnoWhy.Interfaces;
using Android.Support.V7.App;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using System.Threading.Tasks;

namespace KnoWhy.Droid
{
    [Activity(Label = "Settings", Theme = "@style/Theme.Dialog")]
    public class SettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetResult(Result.Canceled);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Settings);

            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(false);
            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);

            this.Title = KnoWhy.Current.CONSTANT_SETTINGS_TITLE;

            TextView buttonClose = (TextView)FindViewById(Resource.Id.buttonClose);
            buttonClose.Click += (sender, e) => {
                CheckBox checkOnlyWiFi = (CheckBox)FindViewById(Resource.Id.chekOnlyWiFi);
                KnoWhy.Current.setOnlyWiFi(checkOnlyWiFi.Checked);
                KnoWhy.Current.updateSettings();
                Finish();
            };

            reloadData();
            
        }

        protected override void OnDestroy()
        {
            KnoWhy.Current.updateSettings();
            base.OnDestroy();
        }
        

        public void reloadData()
        {
            TextView textViewVersion = (TextView)FindViewById(Resource.Id.labelVersion);
            textViewVersion.Text = KnoWhy.VERSION;


            TextView textViewBuild = (TextView)FindViewById(Resource.Id.labelBuild);
            textViewBuild.Text = KnoWhy.BUILD_ANDROID;

            CheckBox checkOnlyWiFi = (CheckBox)FindViewById(Resource.Id.chekOnlyWiFi);
            checkOnlyWiFi.Checked = KnoWhy.Current.onlyWiFi;

            LinearLayout reset1LinearLayout = (LinearLayout)FindViewById(Resource.Id.reset1LinearLayout);
            reset1LinearLayout.Click += (sender, e) => {
                var transaction = FragmentManager.BeginTransaction();
                var dialogFragment = new MyDialogFragment(this, MyDialogFragment.RESET_1);
                dialogFragment.Show(transaction, "dialog_fragment1");
            };

            LinearLayout reset2LinearLayout = (LinearLayout)FindViewById(Resource.Id.reset2LinearLayout);
            reset2LinearLayout.Click += (sender, e) => {
                var transaction = FragmentManager.BeginTransaction();
                var dialogFragment = new MyDialogFragment(this, MyDialogFragment.RESET_2);
                dialogFragment.Show(transaction, "dialog_fragment2");
            };

        }

        public async Task reset1() {
            await KnoWhy.Current.reset(false);
            SetResult(Result.Ok);
            Finish();
        }

        public async Task reset2() {
            await KnoWhy.Current.reset(true);
            SetResult(Result.Ok);
            Finish();
        }
    }
}