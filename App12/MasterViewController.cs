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
        public int tempID;
		[Action("UnwindToNewEvent:")]
		public void UnwindToNewEvent(UIStoryboardSegue segue)
		{
			var segueData = (UITableViewController)segue.SourceViewController;
			if (tempIndexPath != null)
			{
				EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd, tempID);
                if (newEvent.Image == null)
                    //ParseforImage(newEvent.Title);
                dataSource.EditItem(tempIndexPath.Row, newEvent);
                //TableView.BeginUpdates();
                //TableView.EndUpdates();
                TableView.ReloadData();
				//TableView.ReloadRows(new NSIndexPath[] { tempIndexPath}, UITableViewRowAnimation.Automatic);
				tempIndexPath = null;
			}
			else
			{
                EventData newEvent = new EventData(tempTitleFieldText, tempDesc, tempStart, tempEnd);
                if (newEvent.Image == null)
                   // ParseforImage(newEvent.Title);
				dataSource.AddItem(newEvent);
                TableView.ReloadData();
			}
		}

        
        /*
        public UIImage ParseforImage(string title)
        {
            string[] keyterms = new string[] {  "tooth", "brush", "teeth", "dentist", // i <= 3 -> Toothbrush
                                                "lunch", "dinner", "breakfast", "eat", "food", "dessert", "taste", // 4 <= i && i <= 10 -> Fork & knife
                                                "movie", "film", "flick",   //  11 <= i && i <= 13 -> Movie ticket
                                                "walk", "run", "jog",   //  14 <= i && i <= 16 -> Man running
                                                "lift", "weights", "gym", "workout", "exercise",    //  17 <= i && i <= 21 -> Barbell
                                                "shop", "buy",  //  22 <= i && i <= 23 -> Shopping bag
                                                "bathroom", "potty", "restroom", "facilities",  //  24 <= i && i <= 27 -> Bathroom
                                                "shower", "bath",   //  28 <= i && i <= 29 -> Shower
                                                "appointment", "doctor", "checkup", "shot", "hospital", "nurse", "medicine",    //  30 <= i && i <= 36 -> Red cross
                                                "homework", "write", "draw", "work",    //  37 <= i && i <= 40 -> pencil
                                                "bus", "metro", //  41 <= i && <= 42 -> Front facing bus
                                                "school", "class", //   43 <= i && i <= 44 -> school house
                                                ""

                                                };
                
            for (int i = 0; i < keyterms.Length; i++)
            {
                if (title.Contains(keyterms[i]) == true)
                {
                    //  Use if statements to see when i is in the range for a key term
                }
            }
        }
        */

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
            }
        }
    }
}

