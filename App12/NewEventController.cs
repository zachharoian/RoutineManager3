using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class NewEventController : UITableViewController
    {
        //  Variables for data transfer for creating a new event.
		string titleFieldText, descFieldText;
        DateTime startTime, endTime;
        UIColor backgroundColor;

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
            Console.WriteLine("ViewDidLoad() was initiated.");
            //  View did load command.
            base.ViewDidLoad();
            toggleStartDatePicker();

            //  Update the text of the date cell to match the Date Picker.
            startDatePickerChanged();
            Console.WriteLine("ViewDidLoad() was executed and completed.");
        }// END ViewDidLoad()

        

        //  ---------------------------------
        //  startDatePickerChanged(): Method for updating the cell containing Date Picker information.
        //  ---------------------------------
        public void startDatePickerChanged()
		{
            Console.WriteLine("startDatePickerChanged() was initiated.");
            //  Create the path for the cell
            NSIndexPath[] rows = new NSIndexPath[]
			{
				NSIndexPath.FromRowSection(1,0)
			};

            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
			startDateSubtitle.Text = NSDateFormatter.ToLocalizedString(startDatePicker.Date, NSDateFormatterStyle.Medium, NSDateFormatterStyle.Short);
            Console.WriteLine("startDatePickerChanged() was executed and completed.");
		}// END startDatePickerChanged()

        //  ---------------------------------
        //  toggleStartDatePicker(): Flips the state of the Start Date Picker.
        //  ---------------------------------
        public void toggleStartDatePicker()
		{
            Console.WriteLine("toggleStartDatePicker() was initiated.");
            //  Create the path for the cell
            NSIndexPath[] rows = new NSIndexPath[]
            {
                NSIndexPath.FromRowSection(2,0)
            };
            Console.WriteLine("Hidden? " + startDatePickerHidden);
            Console.WriteLine("Row Height: " + GetHeightForRow(TableView, NSIndexPath.FromRowSection(2, 0)));
            startDatePickerHidden = !startDatePickerHidden;
            Console.WriteLine("Hidden? " + startDatePickerHidden);
            Console.WriteLine("Row Height: " + GetHeightForRow(TableView, NSIndexPath.FromRowSection(2, 0)));
            //  Reload the cell to reflect the changes
            TableView.ReloadRows(rows, UITableViewRowAnimation.None);
            //TableView.ReloadData();
            Console.WriteLine("Hidden? " + startDatePickerHidden);
            Console.WriteLine("Row Height: " + GetHeightForRow(TableView, NSIndexPath.FromRowSection(2, 0)));
            Console.WriteLine("toggleStartDatePicker() was executed and completed.");
        }// END toggleStartDataPicker()

        /*
        [Export("UpdateSaveButton:")]
        void UpdateSave(UIStoryboardSegue segue)
        {
            //if (System.String.IsNullOrWhiteSpace(titleField.Text) == true )
            if (titleField.HasText == false)
                buttonSave.Enabled = false;
            else
                buttonSave.Enabled = true;
            
        }
        */

        //  ---------------------------------
        //  MySelector: An action to tell when start Date Picker changes value.
        //  ---------------------------------
        [Export ("UpdateStartDatePicker:")]
		void Update(UIStoryboardSegue segue)
		{
            Console.WriteLine("Update() was initiated.");
            //  Forces the cell to update to reflect the change in value
            startDatePickerChanged();
            Console.WriteLine("Update() was completed.");
        }// END Update()

        //  ---------------------------------
        //  RowSelected(): Overrides the selection method for cells. 
        //  ---------------------------------
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            Console.WriteLine("RowSelected() was initiated.");
            //  If the cell is the Date Picker overview cell, toggle the Date Picker. Else, select the row as normal.
            if (indexPath.Section == 0 && (indexPath.Row == 1 || indexPath.Row == 2))
            {
                if (indexPath.Row == 1)
                {
                    toggleStartDatePicker();
                }
                tableView.DeselectRow(indexPath, true);
            }
            else
                tableView.DeselectRow(indexPath, true);
            Console.WriteLine("RowSelected() was completed.");
        }// END RowSelected()

        //  ---------------------------------
        //  GetHeightForRow(): Overrides the method for setting cell height.
        //  ---------------------------------
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            Console.WriteLine("GetHeightForRow() was initiated.");
            //  If the cell is the Date Picker Cell, and it is hidden, set the height to 0. If it is visible, set it to 216 (height of the Date Picker).
            //  Else, return the current height of the cell. 
            if (startDatePickerHidden == true && indexPath.Section == 0 && indexPath.Row == 2)
                return 0;
            else if (startDatePickerHidden == false && indexPath.Section == 0 && indexPath.Row == 2)
                return 216;
            else
				return base.GetHeightForRow(tableView, indexPath);
        }// END GetHeightForRow()

        public static DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
            Console.WriteLine("PrepareForSegue() was initiated.");
            titleFieldText = titleField.Text;
            if (titleFieldText == "")
                titleFieldText = "New Event";
            descFieldText = /*descField.Text*/"";
            startTime = NSDateToDateTime(startDatePicker.Date);
            endTime = new DateTime(2017, 1, 9, 9, 12, 34);
            base.PrepareForSegue(segue, sender);
			var transferdata = segue.DestinationViewController as MasterViewController;
			transferdata.tempTitleFieldText = titleFieldText;
            transferdata.tempStart = startTime;
            transferdata.tempEnd = endTime;
            transferdata.tempDesc = descFieldText;
            Console.WriteLine("PrepareForSegue() was completed.");
        }
    }
}