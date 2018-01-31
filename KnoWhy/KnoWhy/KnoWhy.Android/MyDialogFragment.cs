
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace KnoWhy.Droid
{
    public class MyDialogFragment : DialogFragment
    {
        public static int RESET_1 = 1;
        public static int RESET_2 = 2;

        SettingsActivity activity = null;
        int mode = 0;

        public MyDialogFragment(SettingsActivity _activity, int _mode) {
            activity = _activity;
            mode = _mode;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            string message = "";
            string title = "";
            string button1 = "";
            string button2 = "";
            if (mode == MyDialogFragment.RESET_1) {
                title = KnoWhy.Current.CONSTANT_ALERT_RESET1_1;
                message = KnoWhy.Current.CONSTANT_ALERT_RESET1_2;
                button1 = KnoWhy.Current.CONSTANT_ALERT_RESET1_3;
                button2 = KnoWhy.Current.CONSTANT_ALERT_RESET1_4;
            } else if (mode == MyDialogFragment.RESET_2)
            {
                title = KnoWhy.Current.CONSTANT_ALERT_RESET2_1;
                message = KnoWhy.Current.CONSTANT_ALERT_RESET2_2;
                button1 = KnoWhy.Current.CONSTANT_ALERT_RESET2_3;
                button2 = KnoWhy.Current.CONSTANT_ALERT_RESET2_4;
            }
            var builder = new AlertDialog.Builder(Activity)
                 .SetMessage(message)
                 .SetPositiveButton(button2, async (sender, args) =>
                {
                    // Do something when this button is clicked.
                if (mode == RESET_1) {
                    await activity.reset1();
                } else if (mode == RESET_2) {
                    await activity.reset2();
                }
                })
                 .SetNegativeButton(button1, (sender, args) =>
                 {
                     // Do something when this button is clicked.
                 })
                .SetTitle(title);
            return builder.Create();
        }
    }
}
