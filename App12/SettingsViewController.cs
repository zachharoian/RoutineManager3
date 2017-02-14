using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class SettingsViewController : UIViewController
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.TintColor = UIColor.Purple;
            NavigationItem.Prompt = "Routine Manger";
            if (App12.RootViewController.isEditingEnabled == true)
            {
                enableEditing.SetTitle("Disable Editing", UIControlState.Normal);
            }
            else
            {
                enableEditing.SetTitle("Enable Editing", UIControlState.Normal);
            }
            enableEditing.TouchUpInside += ToggleEditing;
            //surveyButton.TouchUpInside += TakeSurvey;

        }

        void ToggleEditing (object sender, EventArgs ea)
        {
            
            App12.RootViewController.isEditingEnabled = !App12.RootViewController.isEditingEnabled;
            if (App12.RootViewController.isEditingEnabled == true)
            {
                enableEditing.SetTitle("Disable Editing", UIControlState.Normal); 
            }
            else
            {
                enableEditing.SetTitle("Enable Editing", UIControlState.Normal);
            }
        }

        void TakeSurvey(object sender, EventArgs e)
        {

        }

        partial void SurveyButton_TouchUpInside(UIButton sender)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://goo.gl/forms/pd8e5aonYweOewLw1"));
        }
    }
}