using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class ConsentForm : UITableViewController
    {
        public ConsentForm (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UITextField consent = new UITextField(new CoreGraphics.CGRect(0, 0, this.View.Bounds.Width, this.View.Bounds.Height));
            consent.TextAlignment = UITextAlignment.Left;
            consent.TextColor = UIColor.Black;
            consent.Text = "Thank you for your interest in my project! \nYou must be 18 or older to continue. This application is designed to help autistic adults gain independence in their individual living situations. To participate, you first must sign this consent form to gain access to the app. Then, play around with the app and try it out. After you have tried some of the features, take the survey located in the top bar of the app. You should spend at least 5 minutes exploring the app before taking the survey. There are little to no risks for you, the user.There may be a slight increase in stress levels from being introduced to a new app or entering information.But, it could be beneficial to you, as you may be able to use the app to help organize your daily routines. Confidentiality will be maintained throughout the process, as there is no personal information recorded from the app or the survey, except for the name and signature you provide on this consent form. \nIf you have any questions or concerns, feel free to send contact Jeff Skaanland, my mentor, at JeffSkaanland@gmail.com.His information will also be available in the app if you require it later. \nI have read and understand the above terms, and confirm I am over the age of 18.";
            this.View = consent;

        }
    }
}