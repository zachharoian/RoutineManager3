using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class NewEventController : UITableViewController
    {
        //  Variables for data transfer for creating a new event.
		string titleFieldText;
        string descFieldText;

        //  Variable for Start Date Picker - checks if the Date Picker is visible. 
        bool startDatePickerHidden = false;

        //  ---------------------------------
        //  NewEventController(): Constructor
        //  ---------------------------------
        public NewEventController (IntPtr handle) : base (handle)
        {

        }// END NewEventController()

        //  ---------------------------------
        //  ViewDidLoad(): Method for loading data after the screen is ready to be displayed.
        //  ---------------------------------
        public override void ViewDidLoad()
		{
            //  View did load command.
			base.ViewDidLoad();

            toggleStartDatePicker();

            //  Update the text of the date cell to match the Date Picker.
			startDatePickerChanged();
		}// END ViewDidLoad()

        //  ---------------------------------
        //  startDatePickerChanged(): Method for updating the cell containing Date Picker information.
        //  ---------------------------------
        public void startDatePickerChanged()
		{
            //  Create the path for the cell
			NSIndexPath[] rows = new NSIndexPath[]
			{
				NSIndexPath.FromRowSection(1,0)
			};

            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
			startDateSubtitle.Text = NSDateFormatter.ToLocalizedString(startDatePicker.Date, NSDateFormatterStyle.Medium, NSDateFormatterStyle.Short);

            //  Reload the table to display changes
			TableView.ReloadRows(rows, UITableViewRowAnimation.None);
		}// END startDatePickerChanged()

        //  ---------------------------------
        //  toggleStartDatePicker(): Flips the state of the Start Date Picker.
        //  ---------------------------------
        public void toggleStartDatePicker()
		{
            //  Create the path for the cell
            NSIndexPath[] rows = new NSIndexPath[]
            {
                NSIndexPath.FromRowSection(2,0)
            };
            //  Flips the state of the validation variable.
            startDatePickerHidden = !startDatePickerHidden;

            //  Reload the cell to reflect the changes
			TableView.ReloadRows(rows, UITableViewRowAnimation.None);
		}// END toggleStartDataPicker()

        //  ---------------------------------
        //  MySelector: An action to tell when start Date Picker changes value.
        //  ---------------------------------
        [Export ("UpdateStartDatePicker:")]
		void Update(UIStoryboardSegue segue)
		{
            //  Forces the cell to update to reflect the change in value
			startDatePickerChanged();
		}// END Update()

        //  ---------------------------------
        //  RowSelected(): Overrides the selection method for cells. 
        //  ---------------------------------
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            //  If the cell is the Date Picker overview cell, toggle the Date Picker. Else, select the row as normal.
            if (indexPath.Section == 0 && (indexPath.Row == 1 || indexPath.Row == 2))
            {
                if (indexPath.Row == 1)
                {
                    toggleStartDatePicker();
                    tableView.DeselectRow(indexPath, true);
                }
            }
            else
                tableView.DeselectRow(indexPath, true);
		}// END RowSelected()

        //  ---------------------------------
        //  GetHeightForRow(): Overrides the method for setting cell height.
        //  ---------------------------------
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            //  If the cell is the Date Picker Cell, and it is hidden, set the height to 0. If it is visible, set it to 216 (height of the Date Picker).
            //  Else, return the current height of the cell. 
            if (startDatePickerHidden == true && indexPath.Section == 0 && indexPath.Row == 2)
                return 0;
            else if (startDatePickerHidden == false && indexPath.Section == 0 && indexPath.Row == 2)
                return 216;
            else
				return base.GetHeightForRow(tableView, indexPath);
		}// END GetHeightForRow()

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			titleFieldText = titleField.Text;
            //  TODO: Add description field
            //descFieldText = descField.Text;
			base.PrepareForSegue(segue, sender);
			var transferdata = segue.DestinationViewController as MasterViewController;
			transferdata.tempTitleFieldText = titleFieldText;
		}
    }
}