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
        public TableSource(MasterViewController controller, List<EventData> currentEvents)
        {
            //  Set the current view controller to the controller input
            this.controller = controller;
            
            tableItems = currentEvents;
            
        }
        //  END TableSource()


        public void EditItem(int index, EventData item)
        {
            tableItems[index].Title = item.Title;
            tableItems[index].Desc = item.Desc;
            tableItems[index].Start = item.Start;
            tableItems[index].End = item.End;
            tableItems[index].ID = item.ID;
            tableItems[index].Image = item.Image;
            Console.WriteLine("TableSource thinks Image path from " + item.Title + " is " + item.Image);
            DataAccess.SaveObject(item);
            ReloadSourceData();
		}

        public void ReloadDate(DateTime tempDate)
        {
            date = tempDate;
            ReloadSourceData();
        }

        private void ReloadSourceData()
        {
            tableItems = DataAccess.GetEvents(date);
        }


        //
        //  Method for adding an item
        //
        public void AddItem(EventData item)
        {
            //  Inserts the object into the index provided.
            //tableItems.Insert(index, item);
            DataAccess.SaveObject(item);
            ReloadSourceData();
            
        }
        //  END AddItem()


        //
        //  Custom method for retrieving amount of items in list
        //
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            var items = DataAccess.Count(date);
            if (items == 0)
            {
                UILabel noDataLabel = new UILabel(new CoreGraphics.CGRect(0, 0, tableview.Bounds.Width, tableview.Bounds.Height));
                noDataLabel.Text = "No events today.";
                noDataLabel.TextColor = UIColor.Black;
                noDataLabel.TextAlignment = UITextAlignment.Center;
                tableview.BackgroundView = noDataLabel;
            }
            //  Returns how many items are in the list.
            return items;

        }
        //  END RowsInSection()


        //
        //  Allows cells to be deleted
        //
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            // Return false if you do not want the specified item to be editable.
            if (RootViewController.isEditingEnabled == true)
                return true;
            else
                return false;
        }
        //  END CanEditRow()


        //
        //  Deletes/Inserts cells
        //
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                EventData obj = tableItems[indexPath.Row];
                // Delete the row from the data source.
                DataAccess.DeleteObject(obj);
                ReloadSourceData();
                controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            }
            else if (editingStyle == UITableViewCellEditingStyle.Insert)
            {
                // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
            }
        }
        //  END CommitEditingStyle()
        

        //
        //  When the row is touched
        //
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
			//  Displays alert when touched - Replace with segue call?
			//new UIAlertView("Hey!", "You touched " + tableItems[indexPath.Row].Title, null, "OK", null).Show();
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
            //tableView.AllowsSelection = true;
            tableView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
            if (cell == null)
                cell = new AgendaCell(cellIdentifier);
            cell.UpdateCell(tableItems[indexPath.Row].Title, tableItems[indexPath.Row].Desc, tableItems[indexPath.Row].Start, tableItems[indexPath.Row].End, tableItems[indexPath.Row].Image);
            Console.WriteLine("Image Path for " + tableItems[indexPath.Row].Title + " is: " + tableItems[indexPath.Row].Image);

            
            return cell;
        }
        //  END GetCell()

    }
    //  END TableSource Class

}
//  END App12 Nammespace