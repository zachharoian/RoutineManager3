// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace App12
{
    [Register ("RepeatViewController")]
    partial class RepeatViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel dateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker datePicker { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (dateLabel != null) {
                dateLabel.Dispose ();
                dateLabel = null;
            }

            if (datePicker != null) {
                datePicker.Dispose ();
                datePicker = null;
            }
        }
    }
}