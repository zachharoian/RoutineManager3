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
    [Register ("EditEventController")]
    partial class EditEventController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem buttonSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableViewCell colorCell { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel colorLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton deleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView descField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker endDatePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel endDateSubtitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView headerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton imageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel repeatText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker startDatePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel startDateSubtitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField titleField { get; set; }

        [Action ("ColorTextOnEndEdit:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ColorTextOnEndEdit (UIKit.UIDatePicker sender);

        [Action ("TextColor:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextColor (UIKit.UIDatePicker sender);

        [Action ("UpdateSaveButton:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UpdateSaveButton (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (buttonSave != null) {
                buttonSave.Dispose ();
                buttonSave = null;
            }

            if (colorCell != null) {
                colorCell.Dispose ();
                colorCell = null;
            }

            if (colorLabel != null) {
                colorLabel.Dispose ();
                colorLabel = null;
            }

            if (deleteButton != null) {
                deleteButton.Dispose ();
                deleteButton = null;
            }

            if (descField != null) {
                descField.Dispose ();
                descField = null;
            }

            if (endDatePicker != null) {
                endDatePicker.Dispose ();
                endDatePicker = null;
            }

            if (endDateSubtitle != null) {
                endDateSubtitle.Dispose ();
                endDateSubtitle = null;
            }

            if (headerView != null) {
                headerView.Dispose ();
                headerView = null;
            }

            if (imageView != null) {
                imageView.Dispose ();
                imageView = null;
            }

            if (repeatText != null) {
                repeatText.Dispose ();
                repeatText = null;
            }

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