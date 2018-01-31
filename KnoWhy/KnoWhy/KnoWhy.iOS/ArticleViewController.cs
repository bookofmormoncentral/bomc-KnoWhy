using System;

using UIKit;
using Foundation;
using KnoWhy.Model;
using KnoWhy;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Drawing;
using WebKit;
using CoreGraphics;
using System.Threading.Tasks;

namespace KnoWhy.iOS
{
    public partial class ArticleViewController : UIViewController, IWKNavigationDelegate, IWKScriptMessageHandler
    {
        public static ArticleViewController current = null;

        public Meta meta = null;

        public Article article = null;

        WKWebView webView = null;

        public bool isRoot;

        public ArticleViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            //
            errorView.Hidden = true;
            isRoot = false;
            if (current == null)
            {
                current = this;
                isRoot = true;
            }
            showProgress();

            /*try
            {
                await loadData();
            } catch (Exception ex) {
                string errorMessage = ex.Message;
                Console.WriteLine("Error loading: " + errorMessage);
                //hideProgress();
            }*/

            if (this.SplitViewController != null && isRoot)
            {
                this.NavigationItem.LeftBarButtonItem = this.SplitViewController.DisplayModeButtonItem;
            }
        }

        public async Task loadData() {
            if (meta != null)
            {
                bool hasContent = false;

                this.Title = "#" + meta.knowhyNumber.ToString() + " " + meta.title;
                //this.NavigationItem.Title = meta.title;

                article = await KnoWhy.Current.loadNode(meta, (int)View.Bounds.Width, (int)View.Bounds.Height);

                if (webView == null)
                {
                    WKUserContentController controller = new WKUserContentController();
                    controller.AddScriptMessageHandler(this, "toggleFavorite");
                    controller.AddScriptMessageHandler(this, "showNode");

                    WKPreferences preferences = new WKPreferences();
                    preferences.JavaScriptEnabled = true;

                    WKWebViewConfiguration configuration = new WKWebViewConfiguration();
                    configuration.UserContentController = controller;
                    configuration.Preferences = preferences;

                    UIView viewContainer = (UIView)this.View.ViewWithTag(1);
                    viewContainer.Hidden = true;
                    webView = new WKWebView(new CGRect(0, 0, (int)View.Bounds.Width, (int)View.Bounds.Height), configuration);
                    webView.NavigationDelegate = this;
                    //View.BackgroundColor = UIColor.Red;
                    View.AddSubview(webView);

                    webView.TranslatesAutoresizingMaskIntoConstraints = false;

                    View.AddConstraint(
                        NSLayoutConstraint.Create(webView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, View, NSLayoutAttribute.Height, 1.0f, 0.0f)
                    );

                    View.AddConstraint(
                        NSLayoutConstraint.Create(webView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, View, NSLayoutAttribute.Width, 1.0f, 0.0f)
                    );

                    View.AddConstraint(
                        NSLayoutConstraint.Create(webView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, View, NSLayoutAttribute.Leading, 1.0f, 0.0f)
                    );

                    View.AddConstraint(
                        NSLayoutConstraint.Create(webView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, View, NSLayoutAttribute.Trailing, 1.0f, 0.0f)
                    );
                } else {
                    //webView.Frame = new CGRect(0, 0, (int)View.Bounds.Width, (int)View.Bounds.Height);
                }

                string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");

                
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

                if (hasContent == true)
                {
                    
                    webView.Hidden = false;
                    webView.LoadHtmlString(article.parsedHTML, new NSUrl(contentDirectoryPath, true));

                } else
                {
                    webView.LoadHtmlString("", new NSUrl(contentDirectoryPath, true));
                    webView.Hidden = true;
                    errorLabel.Text = KnoWhy.Current.CONSTANT_CONNECTION_FAILED;
                    errorView.Hidden = false;
                }

                if (UIKit.UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
                {
                    adjustBarForIpad();
                }
            }
            Console.WriteLine("test");
        }

        public async Task setMeta(Meta _meta) {
            meta = _meta;
            await loadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            await loadData();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if(UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
                //this.NavigationController.NavigationBarHidden = true;
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            if (message.Name == "toggleFavorite") {
                Console.WriteLine(message.Body);
                if (message.Body.ToString() == "0") {
                    KnoWhy.Current.removeMetaAsFavorite(meta);
                }
                else if (message.Body.ToString() == "1")
                {
                    KnoWhy.Current.markMetaAsFavorite(meta);
                }
            } else if (message.Name == "showNode") {
                
            }
            //
        }

        [Export("webView:decidePolicyForNavigationAction:decisionHandler:")]
        public void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
        {
            if (navigationAction.TargetFrame == null)
            {
                decisionHandler(WKNavigationActionPolicy.Cancel);
                if (navigationAction.Request.Url.ToString().StartsWith("http", StringComparison.OrdinalIgnoreCase) == true)
                {
                    openLink(navigationAction.Request.Url.ToString());
                } else
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(navigationAction.Request.Url.ToString()));
                }
            }
            else
            {
                if (navigationAction.NavigationType == WKNavigationType.LinkActivated)
                {
                    if (navigationAction.Request.Url.ToString().StartsWith("http", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        decisionHandler(WKNavigationActionPolicy.Cancel);
                        openLink(navigationAction.Request.Url.ToString());
                    } else {
                        string urlString = navigationAction.Request.Url.ToString();
                        string[] array = urlString.Split('#');
                        if (array.Length == 2)
                        {

                            string anchor = array[1];
                            string code = "el = document.getElementById(\"" + anchor + "\"); if (el) el.scrollIntoView();";
                            webView.EvaluateJavaScript(code, null);
                            decisionHandler(WKNavigationActionPolicy.Cancel);
                        }
                        else
                        {
                            decisionHandler(WKNavigationActionPolicy.Allow);
                        }
                    }
                }
                else
                {
                    decisionHandler(WKNavigationActionPolicy.Allow);
                }
            }
        }

        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            hideProgress();
        }

        public void openLink(string url)
        {
            string[] ldsStrings = KnoWhy.Current.getLDSUrls(url);
            if (ldsStrings.Length == 2)
            {
                try
                {
                    if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl(ldsStrings[0])) == true)
                    {
                        UIApplication.SharedApplication.OpenUrl(new NSUrl(ldsStrings[0]));
                    }
                    else
                    {
                        UIApplication.SharedApplication.OpenUrl(new NSUrl(ldsStrings[1]));
                    }
                    //UIApplication.SharedApplication.OpenUrl(new NSUrl(ldsStrings[0]));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fail open LDS app: " + e.Message);
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(ldsStrings[1]));
                }
            }
            else
            {
                try
                {
                    int node = KnoWhy.Current.getKnowhyNode(url);
                    if (node >= 0)
                    {
                        Meta metaN = KnoWhy.Current.getMeta(node);
                        if (metaN != null)
                        {
                            UIStoryboard storyboard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
                            ArticleViewController articleViewController = (ArticleViewController)storyboard.InstantiateViewController("ArticleViewController");
                            articleViewController.NavigationItem.Title = "#" + metaN.knowhyNumber.ToString() + " " + metaN.title;
                            articleViewController.meta = metaN;
                            UINavigationController navigationController = this.NavigationController;
                            navigationController.PushViewController(articleViewController, true);
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
            }
 
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            /*if (UIKit.UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                webView.Frame = new CGRect(0, 0, toSize.Width - 320, toSize.Height);
            }
            else
            {
                webView.Frame = new CGRect(0, 0, toSize.Width, toSize.Height);
            }*/
            if (webView != null)
            {
                webView.Frame = new CGRect(0, 0, toSize.Width, toSize.Height);
            }
        }

        #endregion

        partial void tapWebView(UITapGestureRecognizer sender)
        {
            MainViewController mainViewController = (MainViewController)KnoWhy.Current.listInterface;
            mainViewController.hideMenu();
        }

        public void showProgress()
        {
            loadingLabel.Text = KnoWhy.Current.CONSTANT_LOADING;
            loadingView.Hidden = false;
            activityIndicator.Hidden = false;
        }

        public void hideProgress()
        {
            loadingView.Hidden = true;
            activityIndicator.Hidden = true;

            if (webView.Hidden == true)
            {
                var alertController = UIAlertController.Create(KnoWhy.Current.CONSTANT_ERROR_TITLE, KnoWhy.Current.CONSTANT_CONNECTION_FAILED, UIAlertControllerStyle.Alert);
                alertController.AddAction(UIAlertAction.Create(KnoWhy.Current.CONSTANT_DONE, UIAlertActionStyle.Default, null));


                // Present Alert
                PresentViewController(alertController, true, null);
            }
        }

        public void adjustBarForIpad() {
            if (MainViewController.current.isLandscape()) {
                
            } else {
                
            }
        }

        public void toggleOverlay() {
            if (this.SplitViewController != null && isRoot)
            {
                this.NavigationItem.LeftBarButtonItem.Target.PerformSelector(this.NavigationItem.LeftBarButtonItem.Action);
            }
        }
    }
}