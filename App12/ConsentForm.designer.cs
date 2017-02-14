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
    [Register ("ConsentForm")]
    partial class ConsentForm
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem consentFormAccept { get; set; }

        [Action ("UnwindFromConsent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UnwindFromConsent (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (consentFormAccept != null) {
                consentFormAccept.Dispose ();
                consentFormAccept = null;
            }
        }
    }
}