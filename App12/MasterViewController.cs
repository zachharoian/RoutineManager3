using System;
using Foundation;
using UIKit;


namespace App12
{
    public partial class MasterViewController : UITableViewController
    {
        TableSource dataSource;
        public static UIColor BarTint = UIColor.FromRGB(33, 150, 243);
		public EventData Event;
		public DateTime Day;
		public UIImage incomingUIImage;
		public bool[] daysActive = new bool[7];

		public MasterViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //  Create datastream
            TableView.Source = dataSource = new TableSource(this, Day);
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }

		//	Unwind from cancel to Main Agenda
		[Action("UnwindToMasterViewController:")]
		public void UnwindToMasterViewController(UIStoryboardSegue segue)
		{
		}

		[Action("UnwindToNewEvent:")]
		public void UnwindToNewEvent(UIStoryboardSegue segue)
		{
			var segueData = (UITableViewController)segue.SourceViewController;
			if (Event.ID != 0)  
			{
				//	Edit existing event
                dataSource.EditItem(Event);
			}
			else 
			{
				// Create New Event
                dataSource.AddItem(Event, incomingUIImage);

			}
			dataSource.ReloadSourceData();
			TableView.ReloadData();
            Event.enableNotification();
		}

		[Action("DeleteEvent:")]
		public void DeleteEvent(UIStoryboardSegue segue) 
		{
			Event.disableNotification();
			dataSource.DeleteItem(Event.ID);
			TableView.ReloadData();
		}
        

		public void SegueToEdit()
		{
			PerformSegue("editEventSegue", null);
		}
		bool comingFromNotif;

        public void SegueToEditFromNotification(EventData newEvent)
        {
			comingFromNotif = true;
            Event = newEvent;
            PerformSegue("editEventSegue", null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
			if(segue.Identifier == "editEventSegue")
			{
                base.PrepareForSegue(segue, sender);
				var transferdata = segue.DestinationViewController as RoutineManager.EventViewController;
                var tempIndexPath = TableView.IndexPathForSelectedRow;
				if (comingFromNotif == false)
					transferdata.Event = dataSource.tableItems[tempIndexPath.Row];
				else
					transferdata.Event = Event;
				comingFromNotif = false;
            }
        }
    }
}

