// This file has been autogenerated from a class added in the UI designer.

using System;

using UIKit;
using Foundation;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

using KnoWhy;
using KnoWhy.Model;
using KnoWhy.Interfaces;
using CoreGraphics;
using Reachability;
using System.Linq;

namespace KnoWhy.iOS
{
	public partial class RootMenuViewController : UIViewController
	{
        public static RootMenuViewController current = null;

        float maxBlackViewAlpha = (float)0.5;

        public RootMenuViewController(IntPtr handle) : base (handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            current = this;

            constraintMenuLeft.Constant = -constraintMenuWidth.Constant;

            //this.NavigationController.NavigationBarHidden = true;

            viewBlack.Alpha = 0;
            viewBlack.Hidden = true;

            //openMenu();
            setLabels();

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);


            //KnoWhy.Current.reorder();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //this.NavigationController.NavigationBarHidden = true;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //this.NavigationController.NavigationBarHidden = false;
            this.View.LayoutIfNeeded();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void showSettings()
        {
            //this.PerformSegue("showSettings", null);
            MainViewController.current.showSettings();
        }

        public UIColor FromHex(int hexValue)
        {
            return UIColor.FromRGB(
                (((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
                (((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
                (((float)(hexValue & 0xFF)) / 255.0f)
            );
        }

        public void openMenu()
        {
            constraintMenuLeft.Constant = 0;
            hideIsUpdating();
            // view for dimming effect should also be shown
            viewBlack.Hidden = false;

            UIView.Animate(0.3,
                () =>
                {
                    this.View.LayoutIfNeeded();
                    viewBlack.Alpha = maxBlackViewAlpha;
                    //UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
                },
                () =>
                {
                    gestureScreenEdgePanItem.Enabled = false;

                }
                          );
        }

        public void hideMenu()
        {
            constraintMenuLeft.Constant = -constraintMenuWidth.Constant;

            UIView.Animate(0.3,
                () =>
                {
                    this.View.LayoutIfNeeded();
                    viewBlack.Alpha = 0;
                    UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.Slide);
                },
                () =>
                {
                    gestureScreenEdgePanItem.Enabled = true;
                    viewBlack.Hidden = true;

                }
                          );
        }

        public void setLabels()
        {
            updateContentLabel.Text = KnoWhy.Current.CONSTANT_UPDATE_CONTENT;
            lastUpdateLabel.Text = KnoWhy.Current.getLastUpdate();
            updatingLabel.Text = KnoWhy.Current.CONSTANT_UPDATING;
            settingsLabel.Text = KnoWhy.Current.CONSTANT_SETTINGS;
            webLinksLabel.Text = KnoWhy.Current.CONSTANT_WEB_LINKS;
            link1Label.Text = KnoWhy.Current.CONSTANT_LINK1_DESC;
            if (KnoWhy.Current.CONSTANT_LINK1 != "")
            {
                CGRect frame = link1Label.Frame;
                frame.Height = 21;
                link1Label.Frame = frame;
                link1View.Hidden = false;
            }
            else
            {
                CGRect frame = link1Label.Frame;
                frame.Height = 0;
                link1Label.Frame = frame;
                link1View.Hidden = true;
            }
            link2Label.Text = KnoWhy.Current.CONSTANT_LINK2_DESC;
            if (KnoWhy.Current.CONSTANT_LINK2 != "")
            {
                CGRect frame = link2Label.Frame;
                frame.Height = 21;
                link2Label.Frame = frame;
                link2View.Hidden = false;
            }
            else
            {
                CGRect frame = link2Label.Frame;
                frame.Height = 0;
                link2Label.Frame = frame;
                link2View.Hidden = true;
            }
            link3Label.Text = KnoWhy.Current.CONSTANT_LINK3_DESC;
            if (KnoWhy.Current.CONSTANT_LINK3 != "")
            {
                CGRect frame = link3Label.Frame;
                frame.Height = 21;
                link3Label.Frame = frame;
                link3View.Hidden = false;
            }
            else
            {
                CGRect frame = link3Label.Frame;
                frame.Height = 0;
                link3Label.Frame = frame;
                link3View.Hidden = true;
            }
            this.View.LayoutIfNeeded();
            hideIsUpdating();
        }

        private void showIsUpdating()
        {
            lastUpdateLabel.Hidden = true;
            updatingLabel.Hidden = false;
            progressIndicator.StartAnimating();
        }

        private void hideIsUpdating()
        {
            lastUpdateLabel.Text = KnoWhy.Current.getLastUpdate();
            lastUpdateLabel.Hidden = false;
            updatingLabel.Hidden = true;
            progressIndicator.StopAnimating();
        }

        partial void gesturePan(UIPanGestureRecognizer sender)
        {

        }

        partial void gestureScreenEdgePan(UIScreenEdgePanGestureRecognizer sender)
        {
            if (sender.State == UIGestureRecognizerState.Began)
            {
                viewBlack.Hidden = false;
                viewBlack.Alpha = 0;
            }
            else if (sender.State == UIGestureRecognizerState.Changed)
            {
                nfloat translationX = sender.TranslationInView(sender.View).X;
                if ((-constraintMenuWidth.Constant + translationX) > 0)
                {
                    constraintMenuLeft.Constant = 0;
                    viewBlack.Alpha = maxBlackViewAlpha;
                }
                else if (translationX < 0)
                {
                    constraintMenuLeft.Constant = -constraintMenuWidth.Constant;
                    viewBlack.Alpha = 0;
                }
                else
                {
                    constraintMenuLeft.Constant = -constraintMenuWidth.Constant + translationX;

                    var ratio = translationX / constraintMenuWidth.Constant;
                    var alphaValue = ratio * maxBlackViewAlpha;
                    viewBlack.Alpha = alphaValue;
                }
            }
            else
            {
                if (constraintMenuLeft.Constant < (-constraintMenuWidth.Constant / 2))
                {
                    hideMenu();
                }
                else
                {
                    openMenu();
                }
            }
        }

        partial void gestureTap(UITapGestureRecognizer sender)
        {
            hideMenu();
        }

        async partial void tapUpdate(UITapGestureRecognizer sender)
        {
            showIsUpdating();
            await KnoWhy.Current.loadData(true);
            hideIsUpdating();
        }

        partial void tapSettings(UITapGestureRecognizer sender)
        {
            //this.PerformSegue("showAppSettings", null);
            MainViewController.current.showAppSettings();
        }

        partial void tapLink1(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK1 != "")
            {
                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        partial void tapLink2(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK2 != "")
            {
                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK2));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        partial void tapLink3(UITapGestureRecognizer sender)
        {
            if (KnoWhy.Current.CONSTANT_LINK3 != "")
            {
                try
                {
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(KnoWhy.Current.CONSTANT_LINK3));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public bool isConnected()
        {
            NetworkStatus remoteHostStatus = Reachability.Reachability.RemoteHostStatus();

            if (KnoWhy.Current.onlyWiFi == true)
            {
                if (remoteHostStatus == NetworkStatus.NotReachable || remoteHostStatus != NetworkStatus.ReachableViaWiFiNetwork)
                {
                    return false;
                }
            }
            else
            {
                if (remoteHostStatus == NetworkStatus.NotReachable)
                {
                    return false;
                }
            }
            return true;
        }
	}
}
