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
    [Register ("NewEventController")]
    partial class NewEventController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker startDatePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel startDateSubtitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField titleField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (startDatePicker != null) {
                startDatePicker.Dispose ();
                startDatePicker = null;
            }

            if (startDateSubtitle != null) {
                startDateSubtitle.Dispose ();
                startDateSubtitle = null;
            }

            if (titleField != null) {
                titleField.Dispose ();
                titleField = null;
            }
        }
    }
}