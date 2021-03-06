using System;
using UIKit;
using UserNotifications;

namespace App12
{
    //
    //
    //  ConsentForm: ViewController that presents the Consent Form upon first time launch.
    //
    //
	public partial class ConsentForm : UIViewController
    {
        public ConsentForm(IntPtr handle) : base (handle)
        {
        }
        #region Setup
        //
        //  ViewDidLoad(): Launches when the view is setup.
        //
        public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            //  Set the tint to purple
			NavigationController.NavigationBar.TintColor = UIColor.Purple;

            //  Lambda expression for when accept is clicked.
			consentFormAccept.Clicked += clicked;
		}
        #endregion

        #region Accept button clicked
        //
        //  clicked(): Tells the application that consent has been confirmed, and saves it.
        //             It then returns to the main screen.
        //
        void clicked (object sender, EventArgs ea)
		{
            //  Set consent to true
			RootViewController.consentComfirmed = true;
            //  Save the value
			DataAccess.SaveKey();


            //  Go back to main agenda
			PerformSegue("UnwindFromConsent", null);
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
			{
				// Handle approval
			});
			UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Sound, (approved, err) =>
			{
				// Handle approval
			});
			//  Ask for Notification access
			//TODO: Add onboarding screen. Replace boring consent form with images and a UIPageView.


        }
        #endregion
    }
}