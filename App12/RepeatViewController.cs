using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace App12
{
    public partial class RepeatViewController : UITableViewController
    {
        private bool[] tableItems = new bool[8] { false, false, false, false, false, false, false, false };
        //  Never -> 0, Sunday = 1, Monday = 2, etc. Saturday = 7

        private string[] tableNames = new string[8] { "Never", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};


        public RepeatViewController(IntPtr handle) : base(handle)
		{
		}

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine(indexPath.Row);
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
                else
                    tableItems[0] = false;
            }
            var cell = GetCell(tableView, indexPath);
            for (int i = 0; i < 8; i++)
            {
                if (tableItems[i] == true)
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                else
                    cell.Accessory = UITableViewCellAccessory.None;
            }
            tableView.DeselectRow(indexPath, true);
        }

	}
}