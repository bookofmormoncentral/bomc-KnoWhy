
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Graphics;
using KnoWhy.Interfaces;
using Java.Util;
using Android.Net;
using KnoWhy.Model;
using Android.Webkit;

namespace KnoWhy.Droid
{
    public class ArticleFragment : Fragment
    {

        public Meta meta = null;

        public Article article = null;

        protected Android.Widget.FrameLayout customViewContainer;
        protected View mCustomView;
        protected WebView webView;
        protected MyWebChromeClient mWebChromeClient;
        protected MyWebViewClient mWebViewClient;
        protected WebChromeClient.ICustomViewCallback customViewCallback;

        protected Toolbar toolbar;

        protected Android.Widget.ImageButton buttonExpand;
        protected Android.Widget.ImageButton buttonCollapse;
        protected Android.Widget.FrameLayout overlay;

        bool hasContent = false;

        protected int position = -1;

        public bool isRoot = false;

        public ArticleFragment(int _position, bool _isRoot) {
            position = _position;
            isRoot = _isRoot;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.ArticleFragment, container, false);

            AppCompatActivity activity = (AppCompatActivity)this.Activity;

            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.app_name);
            //activity.SetSupportActionBar(toolbar);
            //activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //activity.SupportActionBar.SetDisplayShowTitleEnabled(true);
            //activity.SupportActionBar.SetHomeButtonEnabled(true);
            //activity.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer);
            toolbar.NavigationClick += handler;

            buttonExpand = view.FindViewById<Android.Widget.ImageButton>(Resource.Id.buttonExpand);
            buttonExpand.Click += delegate
            {
                expand();
            };

            buttonCollapse = view.FindViewById<Android.Widget.ImageButton>(Resource.Id.buttonCollapse);
            buttonCollapse.Click += delegate
            {
                collapse();
            };

            overlay = view.FindViewById<Android.Widget.FrameLayout>(Resource.Id.overlay);
            overlay.Click += delegate {
                if (MainActivity.Current.isExpanded)
                {
                    overlay.Visibility = ViewStates.Gone;
                    MainActivity.Current.hideOverFlowList();
                }
            };

            mWebViewClient = new MyWebViewClient((AppCompatActivity)Activity, this);
            mWebChromeClient = new MyWebChromeClient(this);
            webView = view.FindViewById<WebView>(Resource.Id.webView1);
            //customViewContainer = view.FindViewById<Android.Widget.FrameLayout>(Resource.Id.customViewContainer);
            customViewContainer = MainActivity.Current.getVideoContainer();

            updateLayout();

            if (position >= 0) {
                loadMeta(position);
            }

            return view;
        }

        public void showProgress()
        {
            Android.Widget.LinearLayout viewProgress = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.viewProgress);
            viewProgress.Visibility = ViewStates.Visible;
        }

        public void hideProgress()
        {
            Android.Widget.LinearLayout viewProgress = (Android.Widget.LinearLayout)View.FindViewById(Resource.Id.viewProgress);
            viewProgress.Visibility = ViewStates.Gone;
            Android.Widget.FrameLayout errorView = (Android.Widget.FrameLayout)View.FindViewById(Resource.Id.frameError);
            errorView.Visibility = ViewStates.Gone;
            webView.Visibility = ViewStates.Visible;
            if (hasContent == false)
            {
                errorView.Visibility = ViewStates.Visible;
                MainActivity.Current.showConnectionErrorDialog();
                webView.Visibility = ViewStates.Gone;
            }
        }

        public void expand() {
            /*buttonExpand.Visibility = ViewStates.Gone;
            buttonCollapse.Visibility = ViewStates.Visible;
            MainActivity.Current.isExpanded = true;*/

            MainActivity.Current.expandDetail();
            MainActivity.Current.refresh();

            //toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
        }

        public void collapse() {
            /*buttonExpand.Visibility = ViewStates.Visible;
            buttonCollapse.Visibility = ViewStates.Gone;
            MainActivity.Current.isExpanded = false;
            if (isRoot) {
                toolbar.NavigationIcon = null;
            }*/
            MainActivity.Current.collapseDetail();
            MainActivity.Current.refresh();
        }

        public void loadMeta(int _position) {
            position = _position;
            if (position >= 0 && position < KnoWhy.Current.metaList.Count)
            {
                meta = (Meta)KnoWhy.Current.metaList.ToArray()[position];
            }

            if (meta != null)
            {
                toolbar.Title = "#" + meta.knowhyNumber.ToString() + " " + meta.title;
                if (!isRoot || (MainActivity.Current.isLandscape == false || MainActivity.Current.isTablet == false)) {
                    toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
                    /*toolbar.NavigationClick += delegate
                    {
                        MainActivity.Current.OnBackPressed();
                    };*/
                }

                int width = 0;
                int height = 0;
                try
                {
                    DisplayMetrics displayMetrics = new DisplayMetrics();
                    IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
                    Display display = windowManager.DefaultDisplay;

                    display.GetMetrics(displayMetrics);

                    width = displayMetrics.WidthPixels;
                    height = displayMetrics.HeightPixels;

                    if (width == 0 || height == 0)
                    {
                        Point size = new Point();
                        display.GetSize(size);
                        width = size.X;
                        height = size.Y;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                // Create your application here
                Handler h = new Handler();
                Action myAction = async () =>
                {
                    showProgress();

                    // your code that you want to delay here
                    article = await KnoWhy.Current.loadNode(meta, width, height);


                    if (article != null)
                    {
                        if (article.parsedHTML != null)
                        {
                            if (article.parsedHTML != "")
                            {
                                hasContent = true;
                            }
                        }
                    }
                    /*webView.Touch += (s, e) =>
                    {
                        var handled = false;
                        if (e.Event.Action == MotionEventActions.Move)
                        {
                            // do stuff
                            handled = true;
                        }

                        e.Handled = handled;
                    };*/
                    webView.SetWebChromeClient(mWebChromeClient);
                    webView.SetWebViewClient(mWebViewClient);
                    webView.Settings.JavaScriptEnabled = true;
                    webView.AddJavascriptInterface(new WebAppInterface(this), "Android");

                    if (hasContent == true)
                    {

                        webView.LoadDataWithBaseURL("file:///android_asset/Content/", article.parsedHTML, "text/html", "UTF-8", "");
                    }
                    else
                    {
                        webView.LoadDataWithBaseURL("file:///android_asset/Content/", "", "text/html", "UTF-8", "");
                    }

                    hideProgress();
                };

                h.PostDelayed(myAction, 0);
            }
        }

        /*public override void OnBackPressed()
        {
            if (inCustomView())
            {
                hideCustomView();
                return;
            }
            if (mCustomView == null && webView.CanGoBack())
            {
                webView.GoBack();
                return;
            }
            base.OnBackPressed();
            OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.right_out);
        }*/

        public void updateLayout()
        {
            if (isRoot && MainActivity.Current.isTablet == true && MainActivity.Current.isLandscape == true && MainActivity.Current.isExpanded == false) {
                toolbar.NavigationIcon = null;
            } else {
                toolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha);
            }

            if (MainActivity.Current.isLandscape == true && MainActivity.Current.isTablet == true) {
                if (MainActivity.Current.isExpanded) {
                    buttonExpand.Visibility = ViewStates.Gone;
                    buttonCollapse.Visibility = ViewStates.Visible;
                } else {
                    buttonExpand.Visibility = ViewStates.Visible;
                    buttonCollapse.Visibility = ViewStates.Gone;
                }
                if (MainActivity.Current.showListOverlay)
                {
                    overlay.Visibility = ViewStates.Visible;
                }
                else
                {
                    overlay.Visibility = ViewStates.Gone;
                }
            } else {
                buttonExpand.Visibility = ViewStates.Gone;
                buttonCollapse.Visibility = ViewStates.Gone;

                overlay.Visibility = ViewStates.Gone;
            }


        }

        void handler(object sender, EventArgs args)
        {
            if (isRoot == false || MainActivity.Current.isLandscape == false || MainActivity.Current.isExpanded)
            {
                if (MainActivity.Current.isExpanded == true && isRoot) {
                    overlay.Visibility = ViewStates.Visible;
                }
                MainActivity.Current.OnBackPressed();
            } else {
                MainActivity.Current.OnBackPressed();
            }
        }

        public void toggleFavorites(string value)
        {
            try
            {
                Activity.RunOnUiThread(() => {
                    if (value == "0")
                    {
                        KnoWhy.Current.removeMetaAsFavorite(meta);
                    }
                    else if (value == "1")
                    {
                        KnoWhy.Current.markMetaAsFavorite(meta);
                    }
                });

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public bool inCustomView()
        {
            return (mCustomView != null);
        }

        public void hideCustomView()
        {
            mWebChromeClient.OnHideCustomView();
        }

        public class MyWebViewClient : WebViewClient
        {
            AppCompatActivity activity = null;
            ArticleFragment fragment = null;

            public MyWebViewClient(AppCompatActivity _activity, ArticleFragment _fragment)
            {
                activity = _activity;
                fragment = _fragment;
            }

            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                string[] ldsStrings = KnoWhy.Current.getLDSUrls(url);
                if (ldsStrings.Length == 2)
                {
                    try
                    {
                        var uri = Android.Net.Uri.Parse(ldsStrings[0]);
                        var intent = new Intent(Intent.ActionView, uri);
                        activity.StartActivity(intent);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fail open LDS app: " + e.Message);
                        var uri = Android.Net.Uri.Parse(ldsStrings[1]);
                        var intent = new Intent(Intent.ActionView, uri);
                        activity.StartActivity(intent);
                        return true;
                    }
                }
                else
                {
                    try
                    {
                        int node = KnoWhy.Current.getKnowhyNode(url);
                        if (node >= 0)
                        {
                            int position = KnoWhy.Current.getMetaPosition(node);
                            if (position >= 0)
                            {
                                /*var activity2 = new Intent(activity, typeof(DetailActivity));
                                activity2.PutExtra("position", position);
                                activity.StartActivity(activity2);
                                activity.OverridePendingTransition(Resource.Animation.left_in, Resource.Animation.left_out);*/

                                MainActivity.Current.showArticle(position, false);

                                return true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    var uri = Android.Net.Uri.Parse(url);
                    var intent = new Intent(Intent.ActionView, uri);
                    activity.StartActivity(intent);
                    return true;
                }
            }

        }

        public class MyWebChromeClient : WebChromeClient
        {
            ArticleFragment activity = null;
            private Bitmap mDefaultVideoPoster;
            private View mVideoProgressView;

            public MyWebChromeClient(ArticleFragment _activity)
            {
                activity = _activity;
            }

            public override void OnShowCustomView(View view, WebChromeClient.ICustomViewCallback callback)
            {
                //base.OnShowCustomView(view, callback);
                if (activity.mCustomView != null)
                {
                    callback.OnCustomViewHidden();
                    return;
                }
                activity.mCustomView = view;
                activity.webView.Visibility = ViewStates.Gone;
                activity.customViewContainer.Visibility = ViewStates.Visible;
                activity.customViewContainer.AddView(view);
                activity.customViewCallback = callback;
            }

            public override void OnHideCustomView()
            {
                base.OnHideCustomView();
                if (activity.mCustomView == null)
                {
                    return;
                }

                activity.webView.Visibility = ViewStates.Visible;
                activity.customViewContainer.Visibility = ViewStates.Gone;

                activity.mCustomView.Visibility = ViewStates.Gone;

                activity.customViewContainer.RemoveView(activity.mCustomView);
                activity.customViewCallback.OnCustomViewHidden();

                activity.mCustomView = null;
            }

            public override View VideoLoadingProgressView
            {
                get
                {
                    if (mVideoProgressView == null)
                    {
                        LayoutInflater inflater = LayoutInflater.From(activity.Activity);
                        mVideoProgressView = inflater.Inflate(Resource.Layout.video_progress, null);
                    }
                    return mVideoProgressView;
                }
            }

        }
    }
}
