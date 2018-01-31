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
	[Register ("ArticleViewController")]
	partial class ArticleViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView activityIndicator { get; set; }

		[Outlet]
		UIKit.UILabel errorLabel { get; set; }

		[Outlet]
		UIKit.UIView errorView { get; set; }

		[Outlet]
		UIKit.UILabel loadingLabel { get; set; }

		[Outlet]
		UIKit.UIView loadingView { get; set; }

		[Action ("tapWebView:")]
		partial void tapWebView (UIKit.UITapGestureRecognizer sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (activityIndicator != null) {
				activityIndicator.Dispose ();
				activityIndicator = null;
			}

			if (errorLabel != null) {
				errorLabel.Dispose ();
				errorLabel = null;
			}

			if (errorView != null) {
				errorView.Dispose ();
				errorView = null;
			}

			if (loadingLabel != null) {
				loadingLabel.Dispose ();
				loadingLabel = null;
			}

			if (loadingView != null) {
				loadingView.Dispose ();
				loadingView = null;
			}
		}
	}
}
