using Foundation;
using System;
using UIKit;

namespace RoutineManager
{
	public partial class ColorNewViewController : UITableViewController
	{
		public ColorNewViewController(IntPtr handle) : base(handle)
		{
		}

		public App12.NewEventController controller;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//TableView.Source = dataSource = new ColorViewDataSource();
			//dataSource.EventColor = controller.EventColor;
			var cell = GetCell(TableView, NSIndexPath.FromRowSection(0, 0));
			cell.Accessory = UITableViewCellAccessory.Checkmark;
			TableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(0, 0) }, UITableViewRowAnimation.Automatic);
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return 17;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = GetCell(tableView, indexPath);
			//	If the last cell selected was the current cell selected.
			if (App12.AgendaCell.GetColorName(controller.EventColor) == cell.TextLabel.Text)
			{
				Console.WriteLine("Last Cell Selected: " + App12.AgendaCell.GetColorName(controller.EventColor) + "New Cell: " + cell.TextLabel.Text);

				//	If the last cell was not White, unselect it and select Whitee
				var cellWhite = GetCell(tableView, NSIndexPath.FromRowSection(0, 0));
				if (indexPath.Row != 0)
				{
					if (cell.Accessory == UITableViewCellAccessory.Checkmark)
					{
						Console.WriteLine("Set accessory of last cell to none");
						cell.Accessory = UITableViewCellAccessory.None;
						Console.WriteLine("Set White Accessory to check");
						cellWhite.Accessory = UITableViewCellAccessory.Checkmark;
					}
					else
					{
						Console.WriteLine("Set accessory to checkmark");
						cell.Accessory = UITableViewCellAccessory.Checkmark;
						Console.WriteLine("Set White accessory to none");
						cellWhite.Accessory = UITableViewCellAccessory.None;
					}
				}
				else
				{
					//	White shouldn't be able to be unselected if it was selected last
					cellWhite.Accessory = UITableViewCellAccessory.Checkmark;
					Console.WriteLine("White stays white");
				}
			}
			else
			{
				//	If the last row selected was not the same as the current row selected. 
				//	Change the accessory to the new one, and remove the old.
				var oldCell = GetCell(tableView, NSIndexPath.FromRowSection(controller.EventColor + 1, 0));
				cell.Accessory = UITableViewCellAccessory.Checkmark;
				Console.WriteLine("New Cell checked");
				oldCell.Accessory = UITableViewCellAccessory.None;
				Console.WriteLine("Old Cell unchecked");
				var cellWhite = GetCell(tableView, NSIndexPath.FromRowSection(0, 0));
				if (cellWhite.Accessory == UITableViewCellAccessory.Checkmark)
				{
					cellWhite.Accessory = UITableViewCellAccessory.None;
				}

			}
			controller.EventColor = indexPath.Row - 1;
			TableView.DeselectRow(indexPath, true);
		}
	}
}