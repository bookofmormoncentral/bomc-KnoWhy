// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace KnoWhy.iOS
{
	[Register ("RootMenuViewController")]
	partial class RootMenuViewController
	{

        [Outlet]
        UIKit.UIActivityIndicatorView activityIndicator { get; set; }




        [Outlet]
        UIKit.NSLayoutConstraint constraintMenuLeft { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint constraintMenuWidth { get; set; }


        [Outlet]
        UIKit.UIScreenEdgePanGestureRecognizer gestureScreenEdgePanItem { get; set; }


        [Outlet]
        UIKit.UILabel lastUpdateLabel { get; set; }


        [Outlet]
        UIKit.UILabel link1Label { get; set; }


        [Outlet]
        UIKit.UIView link1View { get; set; }


        [Outlet]
        UIKit.UILabel link2Label { get; set; }


        [Outlet]
        UIKit.UIView link2View { get; set; }


        [Outlet]
        UIKit.UILabel link3Label { get; set; }


        [Outlet]
        UIKit.UIView link3View { get; set; }


        [Outlet]
        UIKit.UILabel loadingLabel { get; set; }


        [Outlet]
        UIKit.UIView loadingView { get; set; }


        [Outlet]
        UIKit.UIActivityIndicatorView progressIndicator { get; set; }


        [Outlet]
        UIKit.UILabel settingsLabel { get; set; }


        [Outlet]
        UIKit.UILabel updateContentLabel { get; set; }


        [Outlet]
        UIKit.UILabel updatingLabel { get; set; }


        [Outlet]
        UIKit.UIView viewBlack { get; set; }


        [Outlet]
        UIKit.UIView viewMenu { get; set; }


        [Outlet]
        UIKit.UILabel webLinksLabel { get; set; }


        [Action("gesturePan:")]
        partial void gesturePan(UIKit.UIPanGestureRecognizer sender);


        [Action("gestureScreenEdgePan:")]
        partial void gestureScreenEdgePan(UIKit.UIScreenEdgePanGestureRecognizer sender);


        [Action("gestureTap:")]
        partial void gestureTap(UIKit.UITapGestureRecognizer sender);


        [Action("tapLink1:")]
        partial void tapLink1(UIKit.UITapGestureRecognizer sender);


        [Action("tapLink2:")]
        partial void tapLink2(UIKit.UITapGestureRecognizer sender);


        [Action("tapLink3:")]
        partial void tapLink3(UIKit.UITapGestureRecognizer sender);


        [Action("tapSettings:")]
        partial void tapSettings(UIKit.UITapGestureRecognizer sender);


        [Action("tapUpdate:")]
        partial void tapUpdate(UIKit.UITapGestureRecognizer sender);

        void ReleaseDesignerOutlets()
        {
            if (activityIndicator != null)
            {
                activityIndicator.Dispose();
                activityIndicator = null;
            }


            if (constraintMenuLeft != null)
            {
                constraintMenuLeft.Dispose();
                constraintMenuLeft = null;
            }

            if (constraintMenuWidth != null)
            {
                constraintMenuWidth.Dispose();
                constraintMenuWidth = null;
            }

            if (gestureScreenEdgePanItem != null)
            {
                gestureScreenEdgePanItem.Dispose();
                gestureScreenEdgePanItem = null;
            }

            if (lastUpdateLabel != null)
            {
                lastUpdateLabel.Dispose();
                lastUpdateLabel = null;
            }

            if (link1Label != null)
            {
                link1Label.Dispose();
                link1Label = null;
            }

            if (link1View != null)
            {
                link1View.Dispose();
                link1View = null;
            }

            if (link2Label != null)
            {
                link2Label.Dispose();
                link2Label = null;
            }

            if (link2View != null)
            {
                link2View.Dispose();
                link2View = null;
            }

            if (link3Label != null)
            {
                link3Label.Dispose();
                link3Label = null;
            }

            if (link3View != null)
            {
                link3View.Dispose();
                link3View = null;
            }

            if (loadingLabel != null)
            {
                loadingLabel.Dispose();
                loadingLabel = null;
            }

            if (loadingView != null)
            {
                loadingView.Dispose();
                loadingView = null;
            }

            if (progressIndicator != null)
            {
                progressIndicator.Dispose();
                progressIndicator = null;
            }

            if (settingsLabel != null)
            {
                settingsLabel.Dispose();
                settingsLabel = null;
            }

            if (updateContentLabel != null)
            {
                updateContentLabel.Dispose();
                updateContentLabel = null;
            }

            if (updatingLabel != null)
            {
                updatingLabel.Dispose();
                updatingLabel = null;
            }

            if (viewBlack != null)
            {
                viewBlack.Dispose();
                viewBlack = null;
            }

            if (viewMenu != null)
            {
                viewMenu.Dispose();
                viewMenu = null;
            }

            if (webLinksLabel != null)
            {
                webLinksLabel.Dispose();
                webLinksLabel = null;
            }
        }
	}
}
