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

        //
        //  Constructor
        //
        public TableSource(MasterViewController controller)
        {
            //  Set the current view controller to the controller input
            this.controller = controller;

            //  Beginning data
            DateTime Random = new DateTime(2017, 1, 9, 13, 53, 36);
            UIColor red = UIColor.Red;
            tableItems.Add(new EventData("Blue", "NewDefault1", Random, Random, red));
            tableItems.Add(new EventData("Blue", "NewDefault1", Random, Random, red));
            tableItems.Add(new EventData("Blue", "NewDefault1", Random, Random, red));
            tableItems.Add(new EventData("Blue", "NewDefault1", Random, Random, red));
        }
		//  END TableSource()


		public void EditItem(int index, EventData item) 
		{
			tableItems[index].Title = item.Title;
			tableItems[index].Desc = item.Desc;
			tableItems[index].Start = item.Start;
			tableItems[index].End = item.End;
			tableItems[index].BackgroundColor = item.BackgroundColor;
		}

        //
        //  Method for adding an item
        //
        public void AddItem(int index, EventData item)
        {
            //  Inserts the object into the index provided.
            tableItems.Insert(index, item);
        }
        //  END AddItem()


        //
        //  Custom method for retrieving amount of items in list
        //
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //  Returns how many items are in the list.
            return tableItems.Count;

        }
        //  END RowsInSection()


        //
        //  Allows cells to be deleted
        //
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            // Return false if you do not want the specified item to be editable.
            return true;
        }
        //  END CanEditRow()


        //
        //  Deletes/Inserts cells
        //
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                // Delete the row from the data source.
                tableItems.RemoveAt(indexPath.Row);
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
            return 115;
        }// END GetHeightForRow()

        //  
        //  Custom method for cell retreival
        //
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(cellIdentifier) as AgendaCell;
            tableView.AllowsSelection = true;
            tableView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
            if (cell == null)
                cell = new AgendaCell(cellIdentifier);
            cell.UpdateCell(tableItems[indexPath.Row].Title, tableItems[indexPath.Row].Desc, tableItems[indexPath.Row].Start, tableItems[indexPath.Row].End, tableItems[indexPath.Row].BackgroundColor);
            return cell;
        }
        //  END GetCell()

    }
    //  END TableSource Class

}
//  END App12 Nammespace