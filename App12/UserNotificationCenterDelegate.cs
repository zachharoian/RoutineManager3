using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjCRuntime;
using UserNotifications;

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
            Console.WriteLine(newEvent.Title);
            // Tell system to display the notification anyway or use
            // `None` to say we have handled the display locally.
            //  Not being called? Notifcation not showing.
            completionHandler(UNNotificationPresentationOptions.Alert);
            Console.WriteLine("Made it past Handler");
            //newEvent.enableNotification();


        }
                
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            switch (response.ActionIdentifier)
            {
                case "reply":
                    //Console.WriteLine("Reply clicked.");
                    break;

                default:
                    break;
            }

            completionHandler();
        }
        #endregion
    }
}