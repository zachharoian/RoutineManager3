using Foundation;
using System;
using UIKit;

namespace App12
{
	public partial class ConsentForm : UIViewController
    {
        public ConsentForm (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			consentFormAccept.Clicked += clicked;
		}

		void clicked (object sender, EventArgs ea)
		{
			RootViewController.consentComfirmed = true;
			DataAccess.SaveKey();
			PerformSegue("UnwindFromConsent", null);
		}

    }
}