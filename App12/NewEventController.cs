using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class NewEventController : UITableViewController
    {
		string titleFieldText;
        UIDatePicker startDatePicker;

        public NewEventController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            startDatePicker = new UIDatePicker();
			toggleStartDatePicker();
			startDatePickerChanged();
		}

		public void startDatePickerChanged()
		{
			NSIndexPath[] rows = new NSIndexPath[]
			{
				NSIndexPath.FromRowSection(1,0)
			};

			startDateSubtitle.Text = NSDateFormatter.ToLocalizedString(startDatePicker.Date, NSDateFormatterStyle.Medium, NSDateFormatterStyle.Short);
			TableView.ReloadRows(rows, UITableViewRowAnimation.None);

		}

		bool startDatePickerHidden = false;


		public void toggleStartDatePicker()
		{
			NSIndexPath[] rows = new NSIndexPath[]
			{
				NSIndexPath.FromRowSection(2,0)
			};
			startDatePicker.Enabled = !startDatePicker.Enabled;
			startDatePickerHidden = !startDatePickerHidden;
			//if (startDatePicker.Enabled == true)
			TableView.ReloadRows(rows, UITableViewRowAnimation.None);
			//TableView.ReloadData();
		}

		[Export ("MySelector:")]
		void Update(UIStoryboardSegue segue)
		{
			startDatePickerChanged();
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0 && indexPath.Row == 1)
			{
				toggleStartDatePicker();
			}
			else
			{
				base.RowSelected(tableView, indexPath);
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (startDatePickerHidden == true && indexPath.Section == 0 && indexPath.Row == 2)
			{
				return 0;
			}
			else
			{
				return base.GetHeightForRow(tableView, indexPath);
			}
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			titleFieldText = titleField.Text;
			base.PrepareForSegue(segue, sender);
			var transferdata = segue.DestinationViewController as MasterViewController;
			transferdata.tempTitleFieldText = titleFieldText;
		}
    }
}