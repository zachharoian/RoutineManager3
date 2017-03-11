using System;
using System.Collections.Generic;

using Foundation;

//  Additional Packages
using UIKit;
using SQLite;
using System.IO;
using EventKit;
using UserNotifications;

namespace App12
{
    public partial class MasterViewController : UITableViewController
    {
        private TableSource dataSource;

        public static UIColor BarTint = UIColor.FromRGB(33, 150, 243);

		public EventData Event;

        public MasterViewController(IntPtr handle) : base(handle)
        {
            //Title = NSBundle.MainBundle.LocalizedString("Agenda", "Agenda");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //  Create datastream
            TableView.Source = dataSource = new TableSource(this, Day);
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Purple;
            


        }
        public DateTime Day;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			//TableView.ReloadData();
		}
       

		//	Unwind from cancel to Main Agenda
		[Action("UnwindToMasterViewController:")]
		public void UnwindToMasterViewController(UIStoryboardSegue segue)
		{
		}

        public bool[] daysActive = new bool [7];
		[Action("UnwindToNewEvent:")]
		public void UnwindToNewEvent(UIStoryboardSegue segue)
		{
			var segueData = (UITableViewController)segue.SourceViewController;
			if (tempIndexPath != null)  
			{
				//	Edit existing event
                dataSource.EditItem(tempIndexPath.Row, Event);
				tempIndexPath = null;
			}
			else 
			{
				// Create New Event
                dataSource.AddItem(Event);
			}
			dataSource.ReloadSourceData();
			TableView.ReloadData();
            Event.enableNotification();

			//TableView.ReloadData();
		}

		[Action("DeleteEvent:")]
		public void DeleteEvent(UIStoryboardSegue segue) 
		{
			Event.disableNotification();
			dataSource.DeleteItem(Event.ID, tempIndexPath);
			tempIndexPath = null;
			TableView.ReloadData();

		}
        

		public NSIndexPath tempIndexPath;
        private bool notification;
		public void SegueToEdit()
		{
			PerformSegue("editEventSegue", null);

		}

        public void SegueToEditFromNotification(EventData newEvent)
        {
            notification = true;
            Event = newEvent;
            PerformSegue("editEventSegue", null);

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
			
			if(segue.Identifier == "editEventSegue")
			{
                base.PrepareForSegue(segue, sender);
                var transferdata = segue.DestinationViewController as EditEventController;
                if (notification == false)
                {
                    var indexPath = TableView.IndexPathForSelectedRow;
                    transferdata.currentTableCell = indexPath;
                    transferdata.Event = dataSource.tableItems[indexPath.Row];
                }
                else
                {
                    transferdata.Event = Event;
                    notification = false;
                }
            }
        }
    }
}

