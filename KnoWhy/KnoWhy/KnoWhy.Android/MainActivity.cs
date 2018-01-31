using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using System;
using Java.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Views;
using Android.Net;
using Android.Support.Constraints;

namespace KnoWhy.Droid
{
    [Activity(Label = "KnoWhy", Theme = "@style/Theme.DesignDemo", MainLauncher = true, Icon = "@drawable/Icon", HardwareAccelerated = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : KnoWhyActivity
    {

        DrawerLayout drawerLayout;

        FrameLayout listFrameLayout;

        FrameLayout articleFrameLayout;

        ConstraintLayout rootLayout;

        ListFragment listFragment;

        public bool isTablet;

        public bool isLandscape;

        public bool showListOverlay;

        public int portraitWidth = 0;

        public int landscapeWidth = 0;

        public int currentPosition = -1;

        public bool isExpanded { get; set; }

        public bool showList { get; set; }

        static MainActivity _current = null;
        public static MainActivity Current
        {
            get
            {
                return _current;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _current = this;

            //AppCompatActivity a = null;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            rootLayout = FindViewById<ConstraintLayout>(Resource.Id.root);

            listFrameLayout = FindViewById<FrameLayout>(Resource.Id.list);

            articleFrameLayout = FindViewById<FrameLayout>(Resource.Id.article);

            listFragment = (ListFragment)SupportFragmentManager.FindFragmentById(Resource.Id.listFragment);

            isTablet = Resources.GetBoolean(Resource.Boolean.isTablet);

            showListOverlay = false;

            ViewGroup.LayoutParams rootParams = (ViewGroup.LayoutParams)rootLayout.LayoutParameters;

            if (rootParams.Width < rootParams.Height) {
                portraitWidth = rootParams.Width;
            } else {
                portraitWidth = rootParams.Height;
            }

            landscapeWidth = Resources.GetDimensionPixelSize(Resource.Dimension.list_max_width);

            //Console.WriteLine("isTablet " + isTablet);

            updateLayout(Resources.Configuration);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (drawerLayout.IsDrawerVisible(Android.Support.V4.View.GravityCompat.Start) == true)
            {
                drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
            } else
            {
                drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
            }
            
            return base.OnOptionsItemSelected(item);
        }



        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            KnoWhy.Current.reorder();

            if (requestCode == KnoWhy.SHOW_SETTINGS && resultCode == Result.Ok) {
                ListFragment.Current.toggleFilterPanel();
            }
        }


        public void updating(bool running)
        {
            LinearLayout isUpdatingLinearLayout = FindViewById<LinearLayout>(Resource.Id.isUpdatingLinearLayout);
            TextView textViewLastUpdate = FindViewById<TextView>(Resource.Id.updateLabel);
            textViewLastUpdate.Text = KnoWhy.Current.getLastUpdate();
            if (running == true)
            {
                isUpdatingLinearLayout.Visibility = ViewStates.Visible;
                textViewLastUpdate.Visibility = ViewStates.Gone;
            } else
            {
                isUpdatingLinearLayout.Visibility = ViewStates.Gone;
                textViewLastUpdate.Visibility = ViewStates.Visible;
            }

        }

        public void initMenu()
        {
            setMenuTexts();

            updating(false);

            setMenuActions();
        }

        private void setMenuTexts()
        {
            LinearLayout isUpdatingLinearLayout = FindViewById<LinearLayout>(Resource.Id.isUpdatingLinearLayout);
            TextView textViewLastUpdateLabel = FindViewById<TextView>(Resource.Id.lastUpdateLabel);
            textViewLastUpdateLabel.Text = KnoWhy.Current.CONSTANT_UPDATE_CONTENT;

            TextView textViewUpdating = FindViewById<TextView>(Resource.Id.updatingLabel);
            textViewUpdating.Text = KnoWhy.Current.CONSTANT_UPDATING;
            TextView textViewSettings = FindViewById<TextView>(Resource.Id.settingsLabel);
            textViewSettings.Text = KnoWhy.Current.CONSTANT_SETTINGS;
            TextView textViewWebLinks = FindViewById<TextView>(Resource.Id.webLinksLabel);
            textViewWebLinks.Text = KnoWhy.Current.CONSTANT_WEB_LINKS;
            TextView textViewLink1 = FindViewById<TextView>(Resource.Id.link1Label);
            textViewLink1.Text = KnoWhy.Current.CONSTANT_LINK1_DESC;
            TextView textViewLink2 = FindViewById<TextView>(Resource.Id.link2Label);
            textViewLink2.Text = KnoWhy.Current.CONSTANT_LINK2_DESC;
            TextView textViewLink3 = FindViewById<TextView>(Resource.Id.link3Label);
            textViewLink3.Text = KnoWhy.Current.CONSTANT_LINK3_DESC;
        }

        private void setMenuActions()
        {
            //Set Menu Items
            LinearLayout lastUpdateLinearLayout = FindViewById<LinearLayout>(Resource.Id.lastUpdateLinearLayout);
            lastUpdateLinearLayout.Click += async (sender, e) => {
                updating(true);
                await KnoWhy.Current.loadData(true);
                updating(false);

            };

            LinearLayout settingsLinearLayout = FindViewById<LinearLayout>(Resource.Id.settingsLinearLayout);
            settingsLinearLayout.Click += (sender, e) => {
                StartActivityForResult(typeof(SettingsActivity), KnoWhy.SHOW_SETTINGS);
            };

            LinearLayout link1LinearLayout = FindViewById<LinearLayout>(Resource.Id.link1LinearLayout);
            if (KnoWhy.Current.CONSTANT_LINK1 != "")
            {
                link1LinearLayout.Visibility = ViewStates.Visible;
            }
            else
            {
                link1LinearLayout.Visibility = ViewStates.Gone;
            }
            link1LinearLayout.Click += (sender, e) => {
                openURL(KnoWhy.Current.CONSTANT_LINK1);
            };

            LinearLayout link2LinearLayout = FindViewById<LinearLayout>(Resource.Id.link2LinearLayout);
            if (KnoWhy.Current.CONSTANT_LINK2 != "")
            {
                link2LinearLayout.Visibility = ViewStates.Visible;
            }
            else
            {
                link2LinearLayout.Visibility = ViewStates.Gone;
            }
            link2LinearLayout.Click += (sender, e) => {
                openURL(KnoWhy.Current.CONSTANT_LINK2);
            };

            LinearLayout link3LinearLayout = FindViewById<LinearLayout>(Resource.Id.link3LinearLayout);
            if (KnoWhy.Current.CONSTANT_LINK3 != "")
            {
                link3LinearLayout.Visibility = ViewStates.Visible;
            }
            else
            {
                link3LinearLayout.Visibility = ViewStates.Gone;
            }
            link3LinearLayout.Click += (sender, e) => {
                openURL(KnoWhy.Current.CONSTANT_LINK3);
            };
        }

        private void openURL(string url)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void showSettings()
        {
            StartActivityForResult(typeof(SortAndFilterActivity), KnoWhy.SHOW_SORT_FILTER);
        }

        public void showArticle(int position, bool isRoot)
        {

            /*var activity2 = new Intent(this, typeof(DetailActivity));
            activity2.PutExtra("position", position);
            StartActivity(activity2);
            OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.left_out);*/

            currentPosition = position;

            //articleFragment.loadMeta(position);
            String tag = "article_" + position.ToString();
            if (isRoot) {
                for (int i = 0; i < SupportFragmentManager.BackStackEntryCount; ++i)
                {
                    SupportFragmentManager.PopBackStack();
                }
            }
            Android.Support.V4.App.FragmentTransaction fragmentTransaction = SupportFragmentManager.BeginTransaction();
            ArticleFragment articleFragment = new ArticleFragment(position, isRoot);
            fragmentTransaction.Add(articleFragment, tag);
            fragmentTransaction.Replace(Resource.Id.article, articleFragment);
            fragmentTransaction.AddToBackStack(tag);
            fragmentTransaction.Commit();

            showListOverlay = false;

            if (isLandscape == false || isTablet == false)
            {
                closeListTablet();
            }
            if (isExpanded == true && isRoot)
            {
                hideOverFlowList();
            }
            /*if (isRoot) {
                isExpanded = false;
            }*/
            updateLayout(Resources.Configuration);
        }

        public void showConnectionErrorDialog()
        {
            var transaction = FragmentManager.BeginTransaction();
            var dialogFragment = new SimpleDialogFragment(KnoWhy.Current.CONSTANT_CONNECTION_FAILED, KnoWhy.Current.CONSTANT_ERROR_TITLE);
            dialogFragment.Show(transaction, "dialog_fragment4");
        }

        public bool isConnected()
        {
            try
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

                    NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;


                    bool isWifi = networkInfo.Type == ConnectivityType.Wifi;
                    if (isWifi == false)
                    {
                        if (KnoWhy.Current.onlyWiFi == true)
                        {
                            return false;
                        }
                        else
                        {
                            bool isOnline = networkInfo.IsConnected;
                            if (isOnline == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
            return true;
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            updateLayout(newConfig);
        }

        public void updateLayout(Android.Content.Res.Configuration newConfig) {
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                isLandscape = true;
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                isLandscape = false;
            }

            //Console.WriteLine("isLandscape " + isLandscape);

            if (isLandscape == true && isTablet == true) {
                showListTablet();
                if (isExpanded) {
                    expandDetail();
                    if (showListOverlay)
                    {
                        listFrameLayout.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        listFrameLayout.Visibility = ViewStates.Gone;
                    }
                } else {
                    collapseDetail();
                    listFrameLayout.Visibility = ViewStates.Visible;
                }
                if (currentPosition == -1) {
                    if (KnoWhy.Current.allMetaList.Count > 0)
                    {
                        showArticle(KnoWhy.Current.getFirstMetaIndex(), true);
                    }
                }
            } else {
                
                if (currentPosition == -1)
                {
                    showListFullscreen();
                } else {
                    closeListTablet();
                }
                listFrameLayout.Visibility = ViewStates.Visible;
                articleFrameLayout.Visibility = ViewStates.Visible;
                collapseDetail();
            }

            foreach (Android.Support.V4.App.Fragment fragment in SupportFragmentManager.Fragments) {
                if (fragment.GetType() == typeof(ArticleFragment))
                {
                    ArticleFragment article = (ArticleFragment)fragment;
                    //if (article.isRoot)
                    //{
                        article.updateLayout();
                        //break;
                    //}
                }
            }

            //collapseDetail();
        }

        private void showListTablet() {
            ViewGroup.LayoutParams layoutParams = (ViewGroup.LayoutParams)listFrameLayout.LayoutParameters;
            layoutParams.Width = landscapeWidth;
            listFrameLayout.LayoutParameters = layoutParams;

            listFrameLayout.Visibility = ViewStates.Visible;

            showList = true;
        }

        private void closeListTablet() {
            ViewGroup.LayoutParams layoutParams = (ViewGroup.LayoutParams)listFrameLayout.LayoutParameters;
            layoutParams.Width = 0;
            listFrameLayout.LayoutParameters = layoutParams;

            showList = false;

        }

        private void showListFullscreen() {
            ViewGroup.LayoutParams layoutParams = (ViewGroup.LayoutParams)listFrameLayout.LayoutParameters;
            layoutParams.Width = portraitWidth;
            listFrameLayout.LayoutParameters = layoutParams;

            listFrameLayout.Visibility = ViewStates.Visible;

            isExpanded = false;

            showList = true;
        }

        public void expandDetail() {
            ConstraintLayout root = FindViewById<ConstraintLayout>(Resource.Id.root);

            ConstraintSet constraintSet = new ConstraintSet();
            constraintSet.Clone(root);
            constraintSet.Connect(Resource.Id.article, ConstraintSet.Start, 0, ConstraintSet.Start);
            constraintSet.ApplyTo(root);

            listFrameLayout.Visibility = ViewStates.Gone;

            isExpanded = true;

            //updateLayout(Resources.Configuration);
        }

        public void collapseDetail() {
            ConstraintLayout root = FindViewById<ConstraintLayout>(Resource.Id.root);

            ConstraintSet constraintSet = new ConstraintSet();
            constraintSet.Clone(root);
            constraintSet.Connect(Resource.Id.article, ConstraintSet.Start, Resource.Id.list, ConstraintSet.End);
            constraintSet.ApplyTo(root);

            listFrameLayout.Visibility = ViewStates.Visible;

            isExpanded = false;

            //updateLayout(Resources.Configuration);
        }

        public Android.Widget.FrameLayout getVideoContainer() {
            Android.Widget.FrameLayout view = FindViewById<Android.Widget.FrameLayout>(Resource.Id.customViewContainer);
            return view;
        }

        public void refresh() {
            updateLayout(Resources.Configuration);
        }

        public void showOverFlowList() {
            showListOverlay = true;
            showList = true;
            updateLayout(Resources.Configuration);
        }

        public void hideOverFlowList() {
            showListOverlay = false;
            showList = false;
            updateLayout(Resources.Configuration);
        }

        public int getCurrentPosition() {
            return currentPosition;
        }

        public override void OnBackPressed()
        {
            if (isExpanded == true && isTablet == true && isLandscape == true)
            {
                showOverFlowList();
            }
            else
            {
                if (SupportFragmentManager.BackStackEntryCount > 1)
                {
                    base.OnBackPressed();
                }
                else if (SupportFragmentManager.BackStackEntryCount == 1 && showList == false)
                {
                    showListFullscreen();
                }
                else
                {
                    Finish();
                }
            }
        }
    }
}

