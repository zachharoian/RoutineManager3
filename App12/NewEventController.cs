using Foundation;
using System;
using UIKit;
using System.Linq;

namespace App12
{
	public partial class NewEventController : UITableViewController
	{
		//  Variables for data transfer for creating a new event.
		public EventData Event = new EventData();
		//  Variable for Start Date Picker - checks if the Date Picker is visible. 
		bool startDatePickerHidden = true;
		bool endDatePickerHidden = true;
		bool startDatePickerTextChanged;
		bool endDatePickerTextChanged;
		public int EventColor = -1;


		UIImagePickerController imagePicker;

		public NSDate dateOfNever;
		//  Variable for repeat
		//bool[] DaysActive = new bool[8] {false, false, false, false, false, false, false, false };
		public string repeatSubtitle;
		//  ---------------------------------
		//  NewEventController(): Constructor used for UI Construction
		//  ---------------------------------
		public NewEventController(IntPtr handle) : base(handle)
		{
		}// END NewEventController()


		//  ---------------------------------
		//  ViewDidLoad(): Method for loading data after the screen is ready to be displayed.
		//  ---------------------------------
		public override void ViewDidLoad()
		{
			//  View did load command.
			base.ViewDidLoad();

			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0.012f, 0.663f, 0.957f);
			NavigationController.NavigationBar.TintColor = UIColor.White;
			startDatePicker.MinuteInterval = 1;
			endDatePicker.MinuteInterval = 1;
			endDatePicker.SetDate(startDatePicker.Date.AddSeconds(3600), false);
			//  Update the text of the date cell to match the Date Picker.
			startDatePickerChanged();
			endDatePickerChanged();
			repeatSubtitle = OverviewReturn();
			repeatText.Text = repeatSubtitle;
			if (descField.Text.Equals("") == true || descField.Text.Equals("Description") == true)
			{
				descField.Text = "Description";
				descField.TextColor = UIColor.FromRGB(199, 199, 205);
			}
			descField.Started += EditingStarted;
			descField.Ended += EditingEnded;
			imageView.SetTitle("Edit", UIControlState.Normal);
			imageView.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
			imageView.VerticalAlignment = UIControlContentVerticalAlignment.Top;
			imageView.TitleEdgeInsets = new UIEdgeInsets(85f, 0f, 0f, 0f);
			SetImageToDefault();
			imageView.TouchUpInside += AddPhoto;
			titleField.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				if (Event.TypeOfImage == TypeOfImage.Default)
					imageView.SetBackgroundImage(UIImage.FromFile(FindImage.ParseForImage(titleField.Text)), UIControlState.Normal);
				return true;
			};

			tableItems[RootViewController.segmentIndex + 1] = true;

			//	New Changes
			//EventColor = Event.Color for Edit;
			colorLabel.Text = AgendaCell.GetColorName(EventColor);
			colorCell.ImageView.Image = AgendaCell.GetColorImage(EventColor);

		}// END ViewDidLoad()

		void AddPhoto(object sender, EventArgs e)
		{
			var temp = FindImage.ParseForImage(titleField.Text);
			var tempstring = temp.Split(new char[] { '.' });
			temp = tempstring[0];
			temp = temp.First().ToString().ToUpper() + temp.Substring(1);
			var actionSheetAlert = UIAlertController.Create("Add Photo", "Take a new photo or upload an existing one.", UIAlertControllerStyle.ActionSheet);
			actionSheetAlert.AddAction(UIAlertAction.Create("Use " + temp + " Icon", UIAlertActionStyle.Default, (obj) => { SetImageToDefault(); }));
			actionSheetAlert.AddAction(UIAlertAction.Create("Take Photo", UIAlertActionStyle.Default, (obj) => { OpenImagePicker("Camera"); }));
			actionSheetAlert.AddAction(UIAlertAction.Create("Choose Photo", UIAlertActionStyle.Default, (obj) => { OpenImagePicker("Library"); }));
			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (obj) => { }));
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			PresentViewController(actionSheetAlert, true, null);
		}

		void SetImageToDefault()
		{
			Event.TypeOfImage = TypeOfImage.Default;
			Event.Image = FindImage.ParseForImage(titleField.Text);
			imageView.SetBackgroundImage(UIImage.FromFile(Event.Image), UIControlState.Normal);
		}

		void OpenImagePicker(string type)
		{
			switch (type)
			{
				case "Library":
					imagePicker = new UIImagePickerController();
					imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
					//imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
					imagePicker.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
					imagePicker.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;

					imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
					imagePicker.Canceled += Handle_Canceled;

					NavigationController.PresentModalViewController(imagePicker, true);
					break;
				case "Camera":
					imagePicker = new UIImagePickerController();
					imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
					imagePicker.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
					imagePicker.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;

					imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
					imagePicker.Canceled += Handle_Canceled;
					NavigationController.PresentModalViewController(imagePicker, true);
					break;
			}
		}

		void Handle_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissModalViewController(true);
		}

		UIImage OriginalImage;
		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			var originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;

			if (originalImage != null)
			{
				//	Equivalent to Event.SetImage()
				OriginalImage = originalImage;
				Event.TypeOfImage = TypeOfImage.Custom;
				imageView.SetBackgroundImage(originalImage, UIControlState.Normal);
			}
			imagePicker.DismissModalViewController(true);
		}

		DateTime RoundUp(NSDate dt, TimeSpan d)
		{
			var ns = NSDateToDateTime(dt);
			var time = new DateTime(((ns.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
			return time;
		}


		public NSDate ConvertDateTimeToNSDate(DateTime date)
		{
			var newDate = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));
			return NSDate.FromTimeIntervalSinceReferenceDate(
				(date - newDate).TotalSeconds);
		}

		void EditingStarted(object sender, EventArgs ea)
		{
			if (descField.Text.Equals("Description") == true)
			{
				descField.Text = "";
				descField.TextColor = UIColor.Black;
			}
		}

		void EditingEnded(object sender, EventArgs ea)
		{
			if (descField.Text.Equals("") == true)
			{
				descField.Text = "Description";
				descField.TextColor = UIColor.FromRGB(199, 199, 205);
			}
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			repeatText.Text = OverviewReturn();
			colorLabel.Text = AgendaCell.GetColorName(EventColor);
			colorCell.ImageView.Image = AgendaCell.GetColorImage(EventColor);
			TableView.ReloadData();
		}
		public string OverviewReturn()
		{
			if (tableItems[0] == true)
				return NSDateFormatter.ToLocalizedString(dateOfNever, NSDateFormatterStyle.Medium, NSDateFormatterStyle.None);
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
						day = RepeatNewViewController.tableNames[i];
					}
				}
				if (count == 1)
					return day + "s";
			}
			return "Custom";

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
		}// END toggleStartDataPicker()


		//  ---------------------------------
		//  toggleEndDatePicker(): Flips the state of the End Date Picker
		//  ---------------------------------
		public void toggleEndDatePicker()
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
		}// END toggleEndDataPicker()


		//  ---------------------------------
		//  UpdateStartDatePicker: An action to tell when the Start Date Picker changes value.
		//  ---------------------------------
		[Export("UpdateStartDatePicker:")]
		void UpdateStart(UIStoryboardSegue segue)
		{
			//  Updates the subtitle text
			startDatePickerChanged();

			//  Saves the text currently in the subtitle
			string tempText = startDateSubtitle.Text;

			//  If the Start date is after the End date, strike out the date. Else, unstrike the other if it is no longer an offender.
			if (startDatePicker.Date.SecondsSinceReferenceDate > endDatePicker.Date.SecondsSinceReferenceDate)
			{
				startDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
				buttonSave.Enabled = false;
			}
			else
			{
				string tempTextEnd = endDateSubtitle.Text;
				endDateSubtitle.AttributedText = new NSAttributedString(tempTextEnd, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
				buttonSave.Enabled = true;
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
			{
				endDateSubtitle.AttributedText = new NSAttributedString(tempText, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
				buttonSave.Enabled = false;
			}
			else
			{
				string tempTextStart = startDateSubtitle.Text;
				startDateSubtitle.AttributedText = new NSAttributedString(tempTextStart, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.None });
				buttonSave.Enabled = true;
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
			if (indexPath.Section == 0 && ((indexPath.Row == 2 && startDatePickerHidden == false) || (indexPath.Row == 4 && endDatePickerHidden == false)))
				return 216;
			//  Else, return the standard row height

			return base.GetHeightForRow(tableView, indexPath);
		}// END GetHeightForRow()


		//  ---------------------------------
		//  NSDateToDateTime(): Convert NSDate to DateTime for turning the Date Picker times into DateTime variables
		//  ---------------------------------
		public static DateTime NSDateToDateTime(NSDate date)
		{
			return ((DateTime)date).ToLocalTime();
		}

		public bool[] tableItems = new bool[8];



		//  ---------------------------------
		//  PrepareForSegue(): Save the date and transfer it back to the main screen to create a new event
		//  ---------------------------------
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			//  Call the prepare for segue method
			base.PrepareForSegue(segue, sender);
			//Console.WriteLine("test");
			if (segue.Identifier != "repeatSegue" && segue.Identifier != "unwindFromCancel" && segue.Identifier != "colorSegue")
			{
				//	NEW
				Event.Color = EventColor;


				//  Save the text from the Title Field
				Event.Title = titleField.Text;

				//  If the Title Field is null, set it to "New Event"
				if (Event.Title == "")
					Event.Title = "New Event";

				//Event.Image = FindImage.ParseForImage(Event.Title);

				if (descField.Text.Equals("Description") == true)
				{
					descField.Text = " ";
				}

				//  Save the text from the Description Field
				Event.Desc = descField.Text;

				//  Save the NSDate from Start Date Picker and convert it to DateTime
				Event.Start = NSDateToDateTime(startDatePicker.Date);
				Event.Start = new DateTime(2017, 1, 1, Event.Start.Hour, Event.Start.Minute, 0);
				//  Save the NSDate from End Date Picker and convert it to DateTime
				Event.End = NSDateToDateTime(endDatePicker.Date);
				Event.End = new DateTime(2017, 1, 1, Event.End.Hour, Event.End.Minute, 0);

				if (tableItems[0] == true)
				{
					var date = ((DateTime)(dateOfNever)).ToLocalTime();
					Event.Start = new DateTime(date.Year, date.Month, date.Day, Event.Start.Hour, Event.Start.Minute, 0);
					Event.End = new DateTime(date.Year, date.Month, date.Day, Event.End.Hour, Event.End.Minute, 0);
				}
				bool[] tempArray = new bool[7];
				for (int i = 1; i < 8; i++)
				{
					tempArray[i - 1] = tableItems[i];
				}

				Event.convertSevenItemArray(tempArray);

				//  Create the transfer path to the Main controller
				var transferdata = segue.DestinationViewController as MasterViewController;

				//  Transfer the Title Field to Main
				transferdata.Event = Event;

				//	Transfer new image
				transferdata.incomingUIImage = OriginalImage;

				transferdata.daysActive = tempArray;
			}
			else if (segue.Identifier == "repeatSegue")
			{
				var transferdata = segue.DestinationViewController as RepeatNewViewController;
				transferdata.controller = this;
			}
			if (segue.Identifier == "colorSegue")
			{
				var transferdata = segue.DestinationViewController as RoutineManager.ColorNewViewController;
				transferdata.controller = this;
			}
		}// END PrepareForSegue()
	}// END NewEventController
}// END App12