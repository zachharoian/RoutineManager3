using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace App12
{
	public partial class RepeatViewController : UITableViewController
	{
		private bool enableDatePicker = false;
		private bool repeat = false;

		public RepeatViewController(IntPtr handle) : base(handle)
		{
		}

		private void toggleDatePickerRows(UITableView tableView, NSIndexPath indexPath)
		{
			enableDatePicker = !enableDatePicker;
			TableView.BeginUpdates();
			TableView.EndUpdates();

		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				if (indexPath.Row == 0)
					toggleDatePickerRows(tableView, indexPath);


			}

			tableView.DeselectRow(indexPath, true);

		}



		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell("cellIdentifier");



			if (indexPath.Section == 0)
			{
				if (indexPath.Row == 0)
				{
					cell.TextLabel.Text = "Never";
					if (enableDatePicker == true)
						cell.Accessory = UITableViewCellAccessory.Checkmark;
					else
						cell.Accessory = UITableViewCellAccessory.None;
					// if there are no cells to reuse, create a new one
					if (cell == null)
					{
						cell = new UITableViewCell(UITableViewCellStyle.Default, "cellIdentifier");
					}

				}
				else if (indexPath.Row == 1)
				{
					cell.TextLabel.Text = "Date";
					cell.DetailTextLabel.Text = NSDateFormatter.ToLocalizedString(datePicker.Date, NSDateFormatterStyle.Short, NSDateFormatterStyle.None);
					// if there are no cells to reuse, create a new one
					if (cell == null)
					{
						cell = new UITableViewCell(UITableViewCellStyle.Value1, "cellIdentifier");
					}
				}


			}
			//cell.TextLabel.Text = tableItems[indexPath.Row];
			//cell.Accessory = UITableViewCellAccessory.Checkmark;
			//cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			//cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;

			// implement AccessoryButtonTapped
			//cell.Accessory = UITableViewCellAccessory.None; // to clear the accessory
			return cell;
		}

	}
}