using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace App12
{
    //  Inherets from UITableViewSource
    public class TableSource : UITableViewSource
    {
        //  List for data to be stored
        public List<EventData> tableItems = new List<EventData>();
        //  View Controller
        readonly MasterViewController controller;
        //  Cell ID
        string cellIdentifier = "TableCell";



        //  Constructor
        public TableSource(List<EventData> data, MasterViewController controller)
        {
            //  Set the current view controller to the controller input
            this.controller = controller;

            //  Fill object list with array input in constructor
            tableItems = data;
        }

        //  Method for adding an item
        public void AddItem(int index, EventData item)
        {
            tableItems.Insert(index, item);
        }

        //  Custom method for retrieving amount of items in array
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //  Returns how many items are in the array.
            return tableItems.Count;

        }

        //  Allows cells to be deleted
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            // Return false if you do not want the specified item to be editable.
            return true;
        }

        //  Deletes/Inserts cells
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

        //  When the row is touched
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //  Displays alert when touched
            new UIAlertView("Hey!", "You touched " + tableItems[indexPath.Row].Title, null, "OK", null).Show();

            //  Unselect row when completed, and show an animation for it.
            tableView.DeselectRow(indexPath, true);
        }

        //  Custom method for cell retreival
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //  Creates a table cell called 'cell' and allows it to be recycled
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            //  Checks if a cell needs to be created, and allows the cell to be recycled
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);

            //  Change the text of the cell
            cell.TextLabel.Text = tableItems[indexPath.Row].Title;

            //  Change the subtitle of the cell
            cell.DetailTextLabel.Text = tableItems[indexPath.Row].Desc;
            return cell;

        }
    }
}