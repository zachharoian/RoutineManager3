﻿using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using UIKit;
using UserNotifications;

namespace App12
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
		#region Overrides
		public override UIWindow Window
        {
            get;
            set;
        }
		static async public Task AuthorizeCameraUse()
		{
			var authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
			if (authStatus != AVAuthorizationStatus.Authorized)
			{
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}
		}

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {



            //  Check if the notifications are still allowed.
            UNUserNotificationCenter.Current.GetNotificationSettings((settings) => {
                var alertsAllowed = (settings.AlertSetting == UNNotificationSetting.Enabled);
            });
            UNUserNotificationCenter.Current.GetNotificationSettings((settings) => {
                var soundsAllowed = (settings.SoundSetting == UNNotificationSetting.Enabled);
            });



            //  Set the notification center delegate to the custom delegate
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            var actionID = "reply";
            var title = "Reply";
            var action = UNNotificationAction.FromIdentifier(actionID, title, UNNotificationActionOptions.None);

            var categoryID = "default";
            var actions = new UNNotificationAction[] { action };
            var intentIDs = new string[] { };
            var category = UNNotificationCategory.FromIdentifier(categoryID, actions, intentIDs, UNNotificationCategoryOptions.None);
            var categories = new UNNotificationCategory[] { category };
            UNUserNotificationCenter.Current.SetNotificationCategories(new NSSet<UNNotificationCategory>(categories));
            return true;
        }

		public override void WillTerminate(UIApplication application)
		{
			
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
		#endregion	

		#region Unused overrides
		public override void OnResignActivation(UIApplication application)
        {
			DataAccess.SaveEdit();
			//System.Console.WriteLine("Saved database");
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }
        #endregion
    }
}


