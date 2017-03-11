using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjCRuntime;
using UserNotifications;
using UIKit;

namespace App12
{
    class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        #region Constructors
        public UserNotificationCenterDelegate()
        {
        }
        #endregion

        #region Override Methods
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do something with the notification
            Console.WriteLine("Active Notification: {0}", notification);
            //notification.Request.Content.Title 
            EventData newEvent = DataAccess.GetNotification(Convert.ToInt32(notification.Request.Identifier));
            // Tell system to display the notification anyway or use
            // `None` to say we have handled the display locally.
            completionHandler(UNNotificationPresentationOptions.Alert);
            newEvent.enableNotification();


        }
                
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            //EventData newEvent = DataAccess.GetNotification(Convert.ToInt32(response.Notification.Request.Identifier));
            switch (response.ActionIdentifier)
            {
                case "reply":
                    //  Reply action
                    break;

                default:
                    if (response.IsDefaultAction)
                    {
                        //  Default action

                    }
                    else if (response.IsDefaultAction)
                    {
                        //  Dismiss action
                    }
                    break;
            }

            completionHandler();
        }
        #endregion
    }
}