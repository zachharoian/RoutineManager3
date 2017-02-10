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
    [Register ("SettingsViewController")]
    partial class SettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton enableEditing { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton surveyButton { get; set; }

        [Action ("SurveyButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SurveyButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (enableEditing != null) {
                enableEditing.Dispose ();
                enableEditing = null;
            }

            if (surveyButton != null) {
                surveyButton.Dispose ();
                surveyButton = null;
            }
        }
    }
}