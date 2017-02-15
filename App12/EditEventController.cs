using Foundation;
using System;
using UIKit;

namespace App12
{
    public partial class EditEventController : UITableViewController
    {
        //  Variables for data transfer for creating a new event.
		public EventData Event;
        public UIColor backgroundColor;

        //  Variable for Start Date Picker - checks if the Date Picker is visible. 
        bool startDatePickerHidden = true;
        bool endDatePickerHidden = true;
        bool startDatePickerTextChanged = false;
        bool endDatePickerTextChanged = false;
		public string repeatSubtitle;
        //  ---------------------------------
        //  NewEventController(): Constructor used for UI Construction
        //  ---------------------------------
        public EditEventController (IntPtr handle) : base (handle)
        {
            
        }// END NewEventController()


        //  ---------------------------------
        //  ViewDidLoad(): Method for loading data after the screen is ready to be displayed.
        //  ---------------------------------
        public override void ViewDidLoad()
		{
            //  View did load command.
            base.ViewDidLoad();
            if (RootViewController.isEditingEnabled == true)
            {
                Title = NSBundle.MainBundle.LocalizedString("Edit Event", "Edit Event");
            }
            else
            {
                Title = NSBundle.MainBundle.LocalizedString(Event.Title, Event.Title);
            }
            NavigationItem.Prompt = " ";
            titleField.Text = Event.Title;
			descField.Text = Event.Desc;
            startDatePicker.SetDate(DateTimeToNSDate(Event.Start), false);
            endDatePicker.SetDate(DateTimeToNSDate(Event.End), false);
			tableItems = Event.getTableItems(false);

            startDatePicker.MinuteInterval = 5;
            endDatePicker.MinuteInterval = 5;
            
            //  Update the text of the date cell to match the Date Picker.
            startDatePickerChanged();
            endDatePickerChanged();

            if (RootViewController.isEditingEnabled == true)
            {
                titleField.Enabled = true;
                descField.Editable = true;
                buttonSave.Enabled = true;
                buttonSave.TintColor = null;
            }
            else
            {
                titleField.Enabled = false;
                descField.Editable = false;
                buttonSave.Enabled = false;
                buttonSave.TintColor = UIColor.Clear;
            }
			tableItems = Event.getTableItems(true);
        	repeatSubtitle = OverviewReturn();
			repeatText.Text = repeatSubtitle;
            if (descField.Text.Equals("") == true || descField.Text.Equals(" ") == true)
            {
                descField.Text = "Description";
                descField.TextColor = UIColor.Gray;
            }
            descField.Started += EditingStarted;
            descField.Ended += EditingEnded;
		}// END ViewDidLoad()


        void EditingStarted (object sender, EventArgs ea)
        {
            if (descField.Text.Equals("Description") == true)
            {
                descField.Text = "";
                descField.TextColor = UIColor.Black;
            }
        }

        void EditingEnded (object sender, EventArgs ea)
        {
            if(descField.Text.Equals("") == true)
            {
                descField.Text = "Description";
                descField.TextColor = UIColor.Gray;
            }
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			repeatText.Text = OverviewReturn();
			TableView.ReloadData();
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
			if (tableItems[0] == false)
			{
				int count = 0;
				string day = "";
				for (int i = 1; i < 8; i++)
				{
					if (tableItems[i] == true)
					{
						count++;
						day = RepeatEditViewController.tableNames[i];
					}
				}
				if (count == 1)
					return day + "s";
			}
			return "Custom";

		}


		public NSDate DateTimeToNSDate (DateTime date)
        {
            DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate((date - newDate).TotalSeconds);
        }

        //  ---------------------------------
        //  startDatePickerChanged(): Method for updating the cell containing Date Picker information.
        //  ---------------------------------
        public void startDatePickerChanged()
		{
            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
			startDateSubtitle.Text = NSDateFormatter.ToLocalizedString(startDatePicker.Date, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
		}// END startDatePickerChanged()


        //  ---------------------------------
        //  endDatePickerChanged(): Method for updating the cell containing Date Picker information.
        //  ---------------------------------
        public void endDatePickerChanged()
        {
            //  Convert the Date Picker information into a string and set it to the subtitle field of the cell.
            endDateSubtitle.Text = NSDateFormatter.ToLocalizedString(endDatePicker.Date, NSDateFormatterStyle.None, NSDateFormatterStyle.Short);
        }// END endDatePickerChanged()

        
        //  ---------------------------------
        //  toggleStartDatePicker(): Flips the state of the Start Date Picker.
        //  ---------------------------------
        public void toggleStartDatePicker()
		{
			if (RootViewController.isEditingEnabled == true)
            {
                //  Toggle visibilty
                startDatePickerHidden = !startDatePickerHidden;

                //  Toggle text color
                startDatePickerTextChanged = !startDatePickerTextChanged;

                //  Flip the color of the subtitle text color from Black to Red if the DatePicker is active
                if (startDatePickerTextChanged == true)
                    startDateSubtitle.TextColor = UIColor.Red;
                else
                    startDateSubtitle.TextColor = UIColor.Black;

                //  Update the cell
                TableView.BeginUpdates();
                TableView.EndUpdates();
            }
        }// END toggleStartDataPicker()


        //  ---------------------------------
        //  toggleEndDatePicker(): Flips the state of the End Date Picker
        //  ---------------------------------
        public void toggleEndDatePicker()
        {
            if (RootViewController.isEditingEnabled == true)
            {

                //  Toggle visiblity
                endDatePickerHidden = !endDatePickerHidden;

                //  Toggle text color
                endDatePickerTextChanged = !endDatePickerTextChanged;

                //  Flip the color of the subtitle text color from Black to Red if the DatePicker is active
                if (endDatePickerTextChanged == true)
                    endDateSubtitle.TextColor = UIColor.Red;
                else
                    endDateSubtitle.TextColor = UIColor.Black;

                //  Update the cell
                TableView.BeginUpdates();
                TableView.EndUpdates();
            }
        }// END toggleEndDataPicker()


        //  ---------------------------------
        //  UpdateStartDatePicker: An action to tell when the Start Date Picker changes value.
        //  ---------------------------------
        [Export ("UpdateStartDatePicker:")]
		void UpdateStart(UIStoryboardSegue segue)
		{
            //  Updates the subtitle text
            startDatePickerChanged();

            //  Saves the text currently in the subtitle
            string tempText = startDateSubtitle.Text;

            //  If the Start date is after the End date, strike out the date. Else, unstrike the other if it is no longer an offender.
            if (startDatePicker.Date.SecondsSinceReferenceDate > endDatePicker.Date.SecondsSinceReferenceDate)
                startDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
            else
            {
                string tempTextEnd = endDateSubtitle.Text;
                endDateSubtitle.AttributedText = new NSAttributedString(tempTextEnd, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
            }
        }// END UpdateStart()


        //  ---------------------------------
        //  UpdateEndDatePicker: An action to tell when the End Date Picker changes value
        //  ---------------------------------
        [Export("UpdateEndDatePicker:")]
        void UpdateEnd(UIStoryboardSegue segue)
        {
            //  Updates the subtitle text
            endDatePickerChanged();

            //  Saves the text currently in the subtitle
            string tempText = endDateSubtitle.Text;

            //  If the Start date is after the End date, strike out the date. Else, unstrike the other if it is no longer an offender.
            if (startDatePicker.Date.SecondsSinceReferenceDate > endDatePicker.Date.SecondsSinceReferenceDate)
                endDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
            else
            {
                string tempTextStart = startDateSubtitle.Text;
                startDateSubtitle.AttributedText = new NSAttributedString(tempTextStart, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
            }
        }// END UpdateEnd()


        //  ---------------------------------
        //  RowSelected(): Overrides the selection method for cells. 
        //  ---------------------------------
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        { 
            //  If the cell is a Date Picker overview cell, toggle the Date Picker. Else, select the row as normal.
            if (indexPath.Section == 0 && (indexPath.Row == 1 || indexPath.Row == 3))
            {
                //  If it is the Start Overview, toggle the Date Picker
                if (indexPath.Row == 1)
                {
                    toggleStartDatePicker();

                    //  If the End Date Picker is open, close it
                    if (endDatePickerHidden == false)
                        toggleEndDatePicker();
                }

                //  If it is the End Overview, toggle the Date Picker
                if (indexPath.Row == 3)
                {
                    toggleEndDatePicker();

                    //  If the Start Date Picker is open, close it
                    if (startDatePickerHidden == false)
                        toggleStartDatePicker();
                }
                
                //  Deselect the Overview row with an animation
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
            //  If the cell is a Date Picker Cell, and it is hidden, set the height to 0.
            if (indexPath.Section == 0 && ((indexPath.Row == 2 && startDatePickerHidden == true) || (indexPath.Row == 4 && endDatePickerHidden == true)))
                return 0;

            //  If the cell is Date Picker Cell, and it is not hidden, set the height to the height of the Date Picker Element (216 pts)
            else if (indexPath.Section == 0 && ((indexPath.Row == 2 && startDatePickerHidden == false) || (indexPath.Row == 4 && endDatePickerHidden == false)))
                return 216;
            //  Else, return the standard row height
            else if ((indexPath.Section == 2 || indexPath.Section == 3 )&&RootViewController.isEditingEnabled == false)
                return 0;
            else
                return base.GetHeightForRow(tableView, indexPath);
        }// END GetHeightForRow()


        //  ---------------------------------
        //  NSDateToDateTime(): Convert NSDate to DateTime for turning the Date Picker times into DateTime variables
        //  ---------------------------------
        public static DateTime NSDateToDateTime(NSDate date)
        {
            //  Create a reference date for conversion
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));

            //  Add the amount of time since the reference date from the NSDate input.
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

		public NSIndexPath currentTableCell;
		public bool[] tableItems;
        //  ---------------------------------
        //  PrepareForSegue(): Save the date and transfer it back to the main screen to create a new event
        //  ---------------------------------
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
            //  Call the prepare for segue method
            base.PrepareForSegue(segue, sender);
			if (segue.Identifier != "repeatSegue")
			{
				//  Save the text from the Title Field
				Event.Title = titleField.Text;

				//  If the Title Field is null, set it to "New Event"
				if (Event.Title == "")
					Event.Title = "New Event";

                if (descField.Text.Equals("Description") == true)
                {
                    descField.Text = " ";
                }

				Event.Image = FindImage.ParseForImage(Event.Title);

				//  Save the text from the Description Field
				Event.Desc = descField.Text;

				//  Save the NSDate from Start Date Picker and convert it to DateTime
				Event.Start = NSDateToDateTime(startDatePicker.Date);

				//  Save the NSDate from End Date Picker and convert it to DateTime
				Event.End = NSDateToDateTime(endDatePicker.Date);

				bool[] tempArray = new bool[7];
				for (int i = 1; i < 8; i++)
				{
					tempArray[i - 1] = tableItems[i];
				}

				Event.convertSevenItemArray(tempArray);

				//  Create the transfer path to the Main controller
				var transferdata = segue.DestinationViewController as MasterViewController;

				transferdata.Event = Event;
                if (currentTableCell == null)
                    currentTableCell = new NSIndexPath();
				transferdata.tempIndexPath = currentTableCell;
                
			}
			else
			{
				var transferdata = segue.DestinationViewController as RepeatEditViewController;
				tableItems = Event.getTableItems(true);
				transferdata.controller = this;
			}
        }// END PrepareForSegue()
    }// END NewEventController
}// END App12