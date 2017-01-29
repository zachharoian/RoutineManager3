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
        TableSource dataSource;

		public string tempTitleFieldText;
        public static UIColor BarTint = UIColor.FromRGB(33, 150, 243);

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Agenda", "Agenda");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //  Create datastream
            TableView.Source = dataSource = new TableSource(this, DataAccess.GetEvents(DateTime.Now));
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            this.NavigationController.NavigationBar.BarTintColor = BarTint;

        }

		//	Unwind from cancel to Main Agenda
		[Action("UnwindToMasterViewController:")]
		public void UnwindToMasterViewController(UIStoryboardSegue segue)
		{
		}
        public string tempDesc, tempImage;
        public DateTime tempStart, tempEnd;
        public int tempID;
		[Action("UnwindToNewEvent:")]
		public void UnwindToNewEvent(UIStoryboardSegue segue)
		{
			var segueData = (UITableViewController)segue.SourceViewController;
			if (tempIndexPath != null)
			{
				EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd, tempID, tempImage);
                Console.WriteLine("Image Path (From Master): " + tempImage);
                dataSource.EditItem(tempIndexPath.Row, newEvent);
                //TableView.BeginUpdates();
                //TableView.EndUpdates();
                TableView.ReloadData();
				//TableView.ReloadRows(new NSIndexPath[] { tempIndexPath}, UITableViewRowAnimation.Automatic);
				tempIndexPath = null;
			}
			else
			{
                EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd, tempImage);
                Console.WriteLine("Image Path: " + tempImage);
                dataSource.AddItem(newEvent);
                TableView.ReloadData();
			}
		}

        

		public NSIndexPath tempIndexPath;


		public void SegueToEdit()
		{
			PerformSegue("editEventSegue", null);
		}
        
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
			
			if(segue.Identifier == "editEventSegue")
			{
                base.PrepareForSegue(segue, sender);
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.tableItems[indexPath.Row];
                var transferdata = segue.DestinationViewController as EditEventController;

				transferdata.currentTableCell = indexPath;

                transferdata.ID = item.ID;
                transferdata.titleFieldText = item.Title;
                transferdata.descFieldText = item.Desc;
                transferdata.startTime = item.Start;
                transferdata.endTime = item.End;
                transferdata.imagePath = item.Image;
            }
        }
    }
}

