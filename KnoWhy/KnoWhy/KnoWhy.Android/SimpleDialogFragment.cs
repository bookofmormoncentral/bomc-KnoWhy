
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
    public class SimpleDialogFragment : DialogFragment
    {
        string message = "";
        string title = "";

        public SimpleDialogFragment(string _message, string _title)
        {
            message = _message;
            title = _title;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var builder = new AlertDialog.Builder(Activity)
                 .SetMessage(message)
                 .SetNeutralButton(KnoWhy.Current.CONSTANT_DONE, (sender, args) =>
                 {
                     // Do something when this button is clicked.
                 })
                .SetTitle(title);
            return builder.Create();
        }
    }
}
