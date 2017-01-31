using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace App12
{
    public partial class RepeatViewController : UITableViewController
    {
        private bool[] tableItems = new bool[8] { true, false, false, false, false, false, false, false };
        //  Never -> 0, Sunday = 1, Monday = 2, etc. Saturday = 7

        private string[] tableNames = new string[8] { "Never", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};


        public RepeatViewController(IntPtr handle) : base(handle)
		{
		}

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row != 0)
            {
                if (tableItems[0] == false)
                    tableItems[indexPath.Row] = !tableItems[indexPath.Row];
                else
                {
                    tableItems[indexPath.Row] = true;
                    tableItems[0] = false;
                }
            }
            else 
            {
                if (tableItems[0] == false)
                {
                    tableItems[0] = true;
                    for (int i = 1; i < 8; i++)
                    {
                        tableItems[i] = false;
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if (tableItems[i] == false)
                {
                    count++;
                }
            }
            if (count == 8)
            {
                tableItems[0] = true;
            }

            for (int i = 0; i < 8; i++)
            {
                var path = NSIndexPath.FromRowSection(i, 0);
                var cell = GetCell(tableView, path);
                if (tableItems[i] == true)
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                else
                    cell.Accessory = UITableViewCellAccessory.None;
            }
            tableView.DeselectRow(indexPath, true);
            
        }

        public string OverviewReturn()
        {
            if (tableItems[0] == true)
                return "Never";
            if (tableItems[1] == true && tableItems[7] == true)
            {
                int count = 0;
                for (int i = 2; i < 7; i++)
                {
                    if (tableItems[i] == false)
                        count++;
                }
                if (count == 5)
                    return "Weekends";
                if (count == 0)
                    return "Everyday";
            }
            else if (tableItems[1] == false && tableItems[7] == false)
            {
                int count = 0;
                for (int i = 2; i < 7; i++)
                {
                    if (tableItems[i] == true)
                        count++;
                }
                if (count == 5)
                    return "Weekdays";
            }
            
            return "Custom";
            
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            var transferdata = segue.DestinationViewController as NewEventController;
            transferdata.repeatSubtitle = OverviewReturn();
        }
    }
}