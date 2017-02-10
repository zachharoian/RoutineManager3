using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace App12
{
    //
    //
    //  Source for Master Table Data
    //
    //
    public class TableSource : UITableViewSource
    {
        //  List for data to be stored
        //  This is the list that will be stored in the database
        public List<EventData> tableItems = new List<EventData>();
        //  View Controller
        readonly MasterViewController controller;
        //  Cell ID
        string cellIdentifier = "TableCell";

        private DateTime date;

        //
        //  Constructor
        //
        public TableSource(MasterViewController controller, DateTime tempDate)
        {
            //  Set the current view controller to the controller input
            this.controller = controller;
			date = tempDate;
			tableItems = DataAccess.GetEvents(date);

        }
		//  END TableSource()

		public void DeleteItem(int tempID, NSIndexPath path) 
		{
			//var obj = tableItems.ElementAt(path.Row);

			var obj = DataAccess.GetObject(tempID);
			DataAccess.DeleteObject(obj);
			ReloadSourceData();
		}


        public void EditItem(int index, EventData item)
        {
            DataAccess.SaveObject(item);
            ReloadSourceData();
		}

        public void ReloadSourceData()
        {
			tableItems = DataAccess.GetEvents(date);

        }


        //
        //  Method for adding an item
        //
        public void AddItem(EventData item)
        {
            DataAccess.SaveObject(item);
            ReloadSourceData();
            
        }
        //  END AddItem()


        //
        //  Custom method for retrieving amount of items in list
        //
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            nint itemCount = DataAccess.Count(date);
            if (itemCount == 0)
            {
                UILabel noDataLabel = new UILabel(new CoreGraphics.CGRect(0, 0, tableview.Bounds.Width, tableview.Bounds.Height));
                if (controller.Day.DayOfWeek == DateTime.Now.DayOfWeek)
                {
                    noDataLabel.Text = "No events today.";
                }else if(controller.Day.DayOfWeek == DateTime.Now.AddDays(1).DayOfWeek)
                {
                    noDataLabel.Text = "No events tomorrow.";
                }
                else
                {
                    noDataLabel.Text = "No events on " + controller.Day.DayOfWeek.ToString() + ".";
                }
                noDataLabel.TextColor = UIColor.DarkGray;
                noDataLabel.TextAlignment = UITextAlignment.Center;
                tableview.BackgroundView = noDataLabel;
            }
            //  Returns how many items are in the list.
            return itemCount;

        }
        //  END RowsInSection()


        //
        //  Allows cells to be deleted
        //
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
			return false;
        }
        //  END CanEditRow()


        //
        //  When the row is touched
        //
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
			controller.SegueToEdit();
            //  Unselect row when completed, and show an animation for it.
            tableView.DeselectRow(indexPath, true);
        }
        //  END RowSelected()

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 140;
        }// END GetHeightForRow()

        //  
        //  Custom method for cell retreival
        //
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as AgendaCell;
            tableView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
            if (cell == null)
                cell = new AgendaCell(cellIdentifier);
            cell.UpdateCell(tableItems[indexPath.Row]);
            
            return cell;
        }
        //  END GetCell()

    }
    //  END TableSource Class

}
//  END App12 Nammespace