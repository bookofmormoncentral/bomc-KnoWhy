// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace KnoWhy.iOS
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        UIKit.UIActivityIndicatorView activityIndicator { get; set; }


        [Outlet]
        UIKit.UILabel arrowLabel { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint bookAndChapterHeight { get; set; }


        [Outlet]
        UIKit.UIView booksContainerView { get; set; }


        [Outlet]
        UIKit.UIView bookTitlePanel { get; set; }


        [Outlet]
        UIKit.UIButton buttonBack1 { get; set; }


        [Outlet]
        UIKit.UIButton buttonBack2 { get; set; }


        [Outlet]
        UIKit.UIButton buttonBookChapter { get; set; }


        [Outlet]
        UIKit.UIButton buttonBookChapter2 { get; set; }


        [Outlet]
        UIKit.UIButton buttonNext1 { get; set; }


        [Outlet]
        UIKit.UIButton buttonNext2 { get; set; }


        [Outlet]
        UIKit.UIView chaptersContainerView { get; set; }


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
        UIKit.UIView panelBooks { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint panelBooksHeight { get; set; }


        [Outlet]
        UIKit.UIView panelChapters { get; set; }


        [Outlet]
        UIKit.UIView panelFilter2 { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint panelFilterHeight { get; set; }


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


        [Action ("gesturePan:")]
        partial void gesturePan (UIKit.UIPanGestureRecognizer sender);


        [Action ("gestureScreenEdgePan:")]
        partial void gestureScreenEdgePan (UIKit.UIScreenEdgePanGestureRecognizer sender);


        [Action ("gestureTap:")]
        partial void gestureTap (UIKit.UITapGestureRecognizer sender);


        [Action ("tapBookAndChapter:")]
        partial void tapBookAndChapter (UIKit.UIButton sender);


        [Action ("tapBookAndChapter2:")]
        partial void tapBookAndChapter2 (UIKit.UIButton sender);


        [Action ("tapBookAndChapter22:")]
        partial void tapBookAndChapter22 (UIKit.UITapGestureRecognizer sender);


        [Action ("tapLink1:")]
        partial void tapLink1 (UIKit.UITapGestureRecognizer sender);


        [Action ("tapLink2:")]
        partial void tapLink2 (UIKit.UITapGestureRecognizer sender);


        [Action ("tapLink3:")]
        partial void tapLink3 (UIKit.UITapGestureRecognizer sender);


        [Action ("tapMenu:")]
        partial void tapMenu (UIKit.UIBarButtonItem sender);


        [Action ("tapNext:")]
        partial void tapNext (UIKit.UIButton sender);


        [Action ("tapPrevious:")]
        partial void tapPrevious (UIKit.UIButton sender);


        [Action ("tapSettings:")]
        partial void tapSettings (UIKit.UITapGestureRecognizer sender);


        [Action ("tapUpdate:")]
        partial void tapUpdate (UIKit.UITapGestureRecognizer sender);

        void ReleaseDesignerOutlets ()
        {
            if (activityIndicator != null) {
                activityIndicator.Dispose ();
                activityIndicator = null;
            }

            if (arrowLabel != null) {
                arrowLabel.Dispose ();
                arrowLabel = null;
            }

            if (bookAndChapterHeight != null) {
                bookAndChapterHeight.Dispose ();
                bookAndChapterHeight = null;
            }

            if (booksContainerView != null) {
                booksContainerView.Dispose ();
                booksContainerView = null;
            }

            if (bookTitlePanel != null) {
                bookTitlePanel.Dispose ();
                bookTitlePanel = null;
            }

            if (buttonBack1 != null) {
                buttonBack1.Dispose ();
                buttonBack1 = null;
            }

            if (buttonBack2 != null) {
                buttonBack2.Dispose ();
                buttonBack2 = null;
            }

            if (buttonBookChapter != null) {
                buttonBookChapter.Dispose ();
                buttonBookChapter = null;
            }

            if (buttonBookChapter2 != null) {
                buttonBookChapter2.Dispose ();
                buttonBookChapter2 = null;
            }

            if (buttonNext1 != null) {
                buttonNext1.Dispose ();
                buttonNext1 = null;
            }

            if (buttonNext2 != null) {
                buttonNext2.Dispose ();
                buttonNext2 = null;
            }

            if (chaptersContainerView != null) {
                chaptersContainerView.Dispose ();
                chaptersContainerView = null;
            }

            if (constraintMenuLeft != null) {
                constraintMenuLeft.Dispose ();
                constraintMenuLeft = null;
            }

            if (constraintMenuWidth != null) {
                constraintMenuWidth.Dispose ();
                constraintMenuWidth = null;
            }

            if (gestureScreenEdgePanItem != null) {
                gestureScreenEdgePanItem.Dispose ();
                gestureScreenEdgePanItem = null;
            }

            if (lastUpdateLabel != null) {
                lastUpdateLabel.Dispose ();
                lastUpdateLabel = null;
            }

            if (link1Label != null) {
                link1Label.Dispose ();
                link1Label = null;
            }

            if (link1View != null) {
                link1View.Dispose ();
                link1View = null;
            }

            if (link2Label != null) {
                link2Label.Dispose ();
                link2Label = null;
            }

            if (link2View != null) {
                link2View.Dispose ();
                link2View = null;
            }

            if (link3Label != null) {
                link3Label.Dispose ();
                link3Label = null;
            }

            if (link3View != null) {
                link3View.Dispose ();
                link3View = null;
            }

            if (loadingLabel != null) {
                loadingLabel.Dispose ();
                loadingLabel = null;
            }

            if (loadingView != null) {
                loadingView.Dispose ();
                loadingView = null;
            }

            if (panelBooks != null) {
                panelBooks.Dispose ();
                panelBooks = null;
            }

            if (panelBooksHeight != null) {
                panelBooksHeight.Dispose ();
                panelBooksHeight = null;
            }

            if (panelChapters != null) {
                panelChapters.Dispose ();
                panelChapters = null;
            }

            if (panelFilter2 != null) {
                panelFilter2.Dispose ();
                panelFilter2 = null;
            }

            if (panelFilterHeight != null) {
                panelFilterHeight.Dispose ();
                panelFilterHeight = null;
            }

            if (progressIndicator != null) {
                progressIndicator.Dispose ();
                progressIndicator = null;
            }

            if (settingsLabel != null) {
                settingsLabel.Dispose ();
                settingsLabel = null;
            }

            if (updateContentLabel != null) {
                updateContentLabel.Dispose ();
                updateContentLabel = null;
            }

            if (updatingLabel != null) {
                updatingLabel.Dispose ();
                updatingLabel = null;
            }

            if (viewBlack != null) {
                viewBlack.Dispose ();
                viewBlack = null;
            }

            if (viewMenu != null) {
                viewMenu.Dispose ();
                viewMenu = null;
            }

            if (webLinksLabel != null) {
                webLinksLabel.Dispose ();
                webLinksLabel = null;
            }
        }
    }
}