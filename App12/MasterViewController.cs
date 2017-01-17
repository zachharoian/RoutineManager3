using System;
using System.Collections.Generic;

using Foundation;

//  Additional Packages
using UIKit;
using SQLite;
using System.IO;
using EventKit;

namespace App12
{
    public partial class MasterViewController : UITableViewController
    {
        TableSource dataSource;

		public string tempTitleFieldText;

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Agenda", "Agenda");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //  Create datastream
            TableView.Source = dataSource = new TableSource(this, DataAccess.GetEvents());
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

        }

		//	Unwind from cancel to Main Agenda
		[Action("UnwindToMasterViewController:")]
		public void UnwindToMasterViewController(UIStoryboardSegue segue)
		{
		}
        public string tempDesc;
        public DateTime tempStart, tempEnd;
		[Action("UnwindToNewEvent:")]
		public void UnwindToNewEvent(UIStoryboardSegue segue)
		{
			var segueData = (UITableViewController)segue.SourceViewController;
			if (tempIndexPath != null)
			{
				EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd);
				dataSource.EditItem(tempIndexPath.Row, newEvent);
				TableView.ReloadRows(new NSIndexPath[] { tempIndexPath }, UITableViewRowAnimation.Automatic);
				tempIndexPath = null;
			}
			else
			{
				EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd);
				dataSource.AddItem(0, newEvent);
                DataAccess.SaveObject(newEvent);

				using (var indexPath = NSIndexPath.FromRowSection(0, 0))
					TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
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

                transferdata.titleFieldText = item.Title;
                transferdata.descFieldText = item.Desc;
                transferdata.startTime = item.Start;
                transferdata.endTime = item.End;
            }
        }
    }
}

