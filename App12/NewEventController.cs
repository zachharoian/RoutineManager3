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
        bool startDatePickerHidden = true;
        bool endDatePickerHidden = true;
        bool startDatePickerTextChanged = false;
        bool endDatePickerTextChanged = false;

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
            //  Reenable when fixed
            //toggleStartDatePicker();
            startDatePicker.MinuteInterval = 5;
            endDatePicker.MinuteInterval = 5;
            //  Update the text of the date cell to match the Date Picker.
            startDatePickerChanged();
            endDatePickerChanged();
            //GetHeightForRow(TableView, NSIndexPath.FromRowSection(2, 0));

            Console.WriteLine("ViewDidLoad() was executed and completed.");
        }// END ViewDidLoad()

        

        //  ---------------------------------
        //  startDatePickerChanged(): Method for updating the cell containing Date Picker information.
        //  ---------------------------------
        public void startDatePickerChanged()
		{
            Console.WriteLine("startDatePickerChanged() was initiated.");

            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
			startDateSubtitle.Text = NSDateFormatter.ToLocalizedString(startDatePicker.Date, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
            Console.WriteLine("startDatePickerChanged() was executed and completed.");
		}// END startDatePickerChanged()

        public void endDatePickerChanged()
        {
            Console.WriteLine("endDatePickerChanged() was initiated.");

            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
            endDateSubtitle.Text = NSDateFormatter.ToLocalizedString(endDatePicker.Date, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
            Console.WriteLine("endDatePickerChanged() was executed and completed.");
        }

        
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
            startDatePickerHidden = !startDatePickerHidden;
            startDatePickerTextChanged = !startDatePickerTextChanged;
            if (startDatePickerTextChanged == true)
                startDateSubtitle.TextColor = UIColor.Red;
            else
                startDateSubtitle.TextColor = UIColor.Black;
            TableView.BeginUpdates();
            TableView.EndUpdates();
        }// END toggleStartDataPicker()

        public void toggleEndDatePicker()
        {
            //  Create the path for the cell
            NSIndexPath[] rows = new NSIndexPath[]
            {
                NSIndexPath.FromRowSection(4,0)
            };
            endDatePickerHidden = !endDatePickerHidden;
            endDatePickerTextChanged = !endDatePickerTextChanged;
            if (endDatePickerTextChanged == true)
                endDateSubtitle.TextColor = UIColor.Red;
            else
                endDateSubtitle.TextColor = UIColor.Black;
            TableView.BeginUpdates();
            TableView.EndUpdates();
        }// END toggleStartDataPicker()

        //  ---------------------------------
        //  MySelector: An action to tell when start Date Picker changes value.
        //  ---------------------------------
        [Export ("UpdateStartDatePicker:")]
		void UpdateStart(UIStoryboardSegue segue)
		{
            Console.WriteLine("UpdateStart() was initiated.");
            //  Forces the cell to update to reflect the change in value
            startDatePickerChanged();
            string tempText = startDateSubtitle.Text;
            if (startDatePicker.Date.SecondsSinceReferenceDate > endDatePicker.Date.SecondsSinceReferenceDate)
                startDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
            else
            {
                string tempTextEnd = endDateSubtitle.Text;
                endDateSubtitle.AttributedText = new NSAttributedString(tempTextEnd, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
            }
        }// END Update()

        [Export("UpdateEndDatePicker:")]
        void UpdateEnd(UIStoryboardSegue segue)
        {
            Console.WriteLine("Update() was initiated.");
            //  Forces the cell to update to reflect the change in value
            endDatePickerChanged();
            string tempText = endDateSubtitle.Text;
            if (startDatePicker.Date.SecondsSinceReferenceDate > endDatePicker.Date.SecondsSinceReferenceDate)
                endDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
            else
            {
                string tempTextStart = startDateSubtitle.Text;
                startDateSubtitle.AttributedText = new NSAttributedString(tempTextStart, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
            }
            Console.WriteLine("Update() was completed.");
        }// END Update()

        //  ---------------------------------
        //  RowSelected(): Overrides the selection method for cells. 
        //  ---------------------------------
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        { 
            //  If the cell is the Date Picker overview cell, toggle the Date Picker. Else, select the row as normal.
            if (indexPath.Section == 0 && (indexPath.Row == 1 || indexPath.Row == 3))
            {
                if (indexPath.Row == 1)
                {
                    toggleStartDatePicker();
                    if (endDatePickerHidden == false)
                        toggleEndDatePicker();
                }

                if (indexPath.Row == 3)
                {
                    toggleEndDatePicker();
                    if (startDatePickerHidden == false)
                        toggleStartDatePicker();
                }
                
                tableView.DeselectRow(indexPath, true);
            }
            else 
                tableView.DeselectRow(indexPath, true);
        }// END RowSelected()


        //  ---------------------------------
        //  GetHeightForRow(): Overrides the method for setting cell height.
        //  ---------------------------------
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            Console.WriteLine("GetHeightForRow() was initiated.");
            //  If the cell is the Date Picker Cell, and it is hidden, set the height to 0. If it is visible, set it to 216 (height of the Date Picker).
            //  Else, return the current height of the cell.
            
            if (indexPath.Section == 0 && ( (indexPath.Row == 2 && startDatePickerHidden == true) || (indexPath.Row == 4 && endDatePickerHidden == true) ))
                return 0;
            else if (indexPath.Section == 0 && ((indexPath.Row == 2 && startDatePickerHidden == false) || (indexPath.Row == 4 && endDatePickerHidden == false)))
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
            descFieldText = descField.Text;
            startTime = NSDateToDateTime(startDatePicker.Date);
            endTime = NSDateToDateTime(endDatePicker.Date);
            base.PrepareForSegue(segue, sender);
			var transferdata = segue.DestinationViewController as MasterViewController;
			transferdata.tempTitleFieldText = titleFieldText;
            transferdata.tempStart = startTime;
            transferdata.tempEnd = endTime;
            transferdata.tempDesc = descFieldText;
            transferdata.tempColor = backgroundColor;
            Console.WriteLine("PrepareForSegue() was completed.");
        }
    }
}