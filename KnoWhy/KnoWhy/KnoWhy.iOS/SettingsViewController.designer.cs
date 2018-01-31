// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace KnoWhy.iOS.Base.lproj
{
    [Register ("SettingsViewController")]
    partial class SettingsViewController
    {
        [Outlet]
        UIKit.UILabel buildLabel { get; set; }


        [Outlet]
        UIKit.UILabel buildValueLabel { get; set; }


        [Outlet]
        UIKit.UILabel onlyWiFiLabel { get; set; }


        [Outlet]
        UIKit.UISwitch onlyWiFiSwitch { get; set; }


        [Outlet]
        UIKit.UILabel reset1DescLabel { get; set; }


        [Outlet]
        UIKit.UILabel reset1Label { get; set; }


        [Outlet]
        UIKit.UILabel reset2DescLabel { get; set; }


        [Outlet]
        UIKit.UILabel reset2Label { get; set; }


        [Outlet]
        UIKit.UILabel versionLabel { get; set; }


        [Outlet]
        UIKit.UILabel versionValueLabel { get; set; }


        [Action ("onlyWiFiChanged:")]
        partial void onlyWiFiChanged (UIKit.UISwitch sender);


        [Action ("tapDone:")]
        partial void tapDone (UIKit.UIButton sender);


        [Action ("tapReset1:")]
        partial void tapReset1 (UIKit.UITapGestureRecognizer sender);


        [Action ("tapReset2:")]
        partial void tapReset2 (UIKit.UITapGestureRecognizer sender);

        void ReleaseDesignerOutlets ()
        {
            if (buildLabel != null) {
                buildLabel.Dispose ();
                buildLabel = null;
            }

            if (buildValueLabel != null) {
                buildValueLabel.Dispose ();
                buildValueLabel = null;
            }

            if (onlyWiFiLabel != null) {
                onlyWiFiLabel.Dispose ();
                onlyWiFiLabel = null;
            }

            if (onlyWiFiSwitch != null) {
                onlyWiFiSwitch.Dispose ();
                onlyWiFiSwitch = null;
            }

            if (reset1DescLabel != null) {
                reset1DescLabel.Dispose ();
                reset1DescLabel = null;
            }

            if (reset1Label != null) {
                reset1Label.Dispose ();
                reset1Label = null;
            }

            if (reset2DescLabel != null) {
                reset2DescLabel.Dispose ();
                reset2DescLabel = null;
            }

            if (reset2Label != null) {
                reset2Label.Dispose ();
                reset2Label = null;
            }

            if (versionLabel != null) {
                versionLabel.Dispose ();
                versionLabel = null;
            }

            if (versionValueLabel != null) {
                versionValueLabel.Dispose ();
                versionValueLabel = null;
            }
        }
    }
}