using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace App12
{
    public partial class RepeatViewController : UITableViewController
    {
        public NewEventController controller;
        //  Never -> 0, Sunday = 1, Monday = 2, etc. Saturday = 7
        
        static public string[] tableNames = new string[8] { "Never", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};


        public RepeatViewController(IntPtr handle) : base(handle)
		{
            
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            for (int i = 0; i < 8; i++)
            {
                var path = NSIndexPath.FromRowSection(i, 0);
                var cell = GetCell(TableView, path);
                if (controller.tableItems[i] == true)
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                else
                    cell.Accessory = UITableViewCellAccessory.None;
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row != 0)
            {
                if (controller.tableItems[0] == false)
                    controller.tableItems[indexPath.Row] = !controller.tableItems[indexPath.Row];
                else
                {
                    controller.tableItems[indexPath.Row] = true;
                    controller.tableItems[0] = false;
                }
            }
            else 
            {
                if (controller.tableItems[0] == false)
                {
                    controller.tableItems[0] = true;
                    for (int i = 1; i < 8; i++)
                    {
                        controller.tableItems[i] = false;
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if (controller.tableItems[i] == false)
                {
                    count++;
                }
            }
            if (count == 8)
            {
                controller.tableItems[0] = true;
            }

            for (int i = 0; i < 8; i++)
            {
                var path = NSIndexPath.FromRowSection(i, 0);
                var cell = GetCell(tableView, path);
                if (controller.tableItems[i] == true)
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                else
                    cell.Accessory = UITableViewCellAccessory.None;
            }
            tableView.DeselectRow(indexPath, true);
            
        }

        
    }
}