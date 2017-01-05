using System;
using System.Collections.Generic;

using Foundation;

//  Additional Packages
using UIKit;
using SQLite;
using System.IO;

namespace App12
{
    public partial class MasterViewController : UITableViewController
    {
        TableSource dataSource;

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.LeftBarButtonItem = EditButtonItem;

            //  Add events button
            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddEvent);
            addButton.AccessibilityLabel = "addButton";
            NavigationItem.RightBarButtonItem = addButton;

            //  Test data
            List<EventData> data = new List<EventData>();
            data[0] = new EventData("Red", "NewDefault1");
            data[1] = new EventData();
            data[2] = new EventData();
            data[3] = new EventData();
            data[1].Title = "Blue";
            data[1].Desc = "Default2";
            data[2].Title = "Green";
            data[2].Desc = "Default3";
            data[3].Title = "Yellow";
            data[3].Desc = "Default4";

            DataAccess.SaveObject(data[1]);
            //DataAccess.SaveObject(data[1]);
            //DataAccess.SaveObject(data[2]);
            //DataAccess.SaveObject(data[3]);

            //  Insert Data
            TableView.Source = dataSource = new TableSource(data, this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void AddEvent(object sender, EventArgs args)
        {
            EventData newEvent = new EventData("Default Title", "Default Desc"); 
            dataSource.AddItem(0, newEvent);

            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.tableItems[indexPath.Row];

                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }

        /*
        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");
            readonly List<object> objects = new List<object>();
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller)
            {
                this.controller = controller;
            }

            public IList<object> Objects
            {
                get { return objects; }
            }

            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return objects.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.TextLabel.Text = objects[indexPath.Row].ToString();

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    objects.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
            
        }*/
    }
}

