using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace App12
{
	public partial class RepeatEditViewController : UITableViewController
	{
		public EditEventController controller;
		//  Never -> 0, Sunday = 1, Monday = 2, etc. Saturday = 7

		static public string[] tableNames = new string[8] { "Never", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

		bool neverDatePickerHidden;


		public RepeatEditViewController(IntPtr handle) : base(handle)
		{

		}
		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();
			if (controller.tableItems[0] == true)
			{
				enableDatePicker();
			}
			else
			{
				disableDatePicker();
			}
			for (int i = 0; i < 8; i++)
			{
				NSIndexPath path;
				if (i != 0)
				{
					path = NSIndexPath.FromRowSection(i + 2, 0);
				}
				else
				{
					path = NSIndexPath.FromRowSection(0, 0);
				}
				var cell = GetCell(TableView, path);
				if (controller.tableItems[i] == true)
					cell.Accessory = UITableViewCellAccessory.Checkmark;
				else
					cell.Accessory = UITableViewCellAccessory.None;
			}

			DatePickerChanged(new object(), new EventArgs());
			neverDatePicker.ValueChanged += DatePickerChanged;

		}

		public void DatePickerChanged(object sender, EventArgs e)
		{
			subtitleLabel.Text = NSDateFormatter.ToLocalizedString(neverDatePicker.Date, NSDateFormatterStyle.Medium, NSDateFormatterStyle.None);
			controller.dateOfNever = neverDatePicker.Date;
		}
		/*
		public void toggleDatePicker()
		{
			//  Toggle visibilty
			neverDatePickerHidden = !neverDatePickerHidden;

			//  Update the cell
			TableView.BeginUpdates();
			TableView.EndUpdates();
		}// END toggleStartDataPicker()
		*/
		public void enableDatePicker()
		{
			neverDatePickerHidden = false;
			TableView.BeginUpdates();
			TableView.EndUpdates();
		}

		public void disableDatePicker()
		{
			neverDatePickerHidden = true;
			TableView.BeginUpdates();
			TableView.EndUpdates();
		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row != 1 && indexPath.Row != 2)
			{
				if (indexPath.Row != 0)
				{
					if (controller.tableItems[0] == false)
						controller.tableItems[indexPath.Row - 2] = !controller.tableItems[indexPath.Row - 2];
					else
					{
						controller.tableItems[indexPath.Row - 2] = true;
						controller.tableItems[0] = false;
					}
				}
				else
				{
					if (controller.tableItems[0] == false && indexPath.Row == 0)
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
					NSIndexPath path;
					if (i != 0)
					{
						path = NSIndexPath.FromRowSection(i + 2, 0);
					}
					else
					{
						path = NSIndexPath.FromRowSection(0, 0);
					}

					var cell = GetCell(tableView, path);
					if (controller.tableItems[i] == true)
						cell.Accessory = UITableViewCellAccessory.Checkmark;
					else
						cell.Accessory = UITableViewCellAccessory.None;

				}
			}

			if (controller.tableItems[0] == true)
			{
				enableDatePicker();
			}
			else
			{
				disableDatePicker();
			}
			tableView.DeselectRow(indexPath, true);
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Row == 1 || indexPath.Row == 2)
			{
				if (neverDatePickerHidden == false)
				{
					if (indexPath.Row == 1)
					{
						return 44;
					}
					else
					{
						return 216;
					}
				}
				else
				{
					return 0;
				}
			}
			else
			{
				return base.GetHeightForRow(tableView, indexPath);
			}
		}

	}
}