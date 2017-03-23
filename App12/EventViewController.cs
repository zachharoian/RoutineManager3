using Foundation;
using System;
using UIKit;
using App12;
using CoreGraphics;
using System.IO;

namespace RoutineManager
{
	public partial class EventViewController : UITableViewController
	{
		public EventViewController(IntPtr handle) : base(handle)
		{
		}

		public EventData Event;

		public static EventViewController CurrentViewController;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			CurrentViewController = this;
			NavigationItem.Title = "Event Details";
			headerView.BackgroundColor = AgendaCell.GetColor(Event.Color);
			if (headerView.BackgroundColor != UIColor.White)
				imageView.TintColor = UIColor.White;
			else
				imageView.TintColor = UIColor.Black;
			NavigationItem.Prompt = " ";
			titleLabel.Text = Event.Title;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Edit, (object sender, EventArgs e) => { seguetoEdit(); });

			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

			descLabel.Text = Event.Desc;

			if (headerView.BackgroundColor == UIColor.White)
			{
				titleLabel.TextColor = UIColor.Black;
				speaker.TintColor = UIColor.Black;
			}
			else
			{
				titleLabel.TextColor = UIColor.White;
				speaker.TintColor = UIColor.White;
			}

			timeLabel.Text = Event.Start.ToShortTimeString() + " - " + Event.End.ToShortTimeString();

			if (RootViewController.isEditingEnabled == true)
			{
				NavigationItem.RightBarButtonItem.Enabled = true;
				NavigationItem.RightBarButtonItem.TintColor = null;
			}
			else
			{
				NavigationItem.RightBarButtonItem.Enabled = false;
				NavigationItem.RightBarButtonItem.TintColor = UIColor.Clear;
			}

			imageView.SetBackgroundImage(Event.GetImage(true), UIControlState.Normal);

			//imageView.TouchUpInside += OpenFullImage;
			speaker.TouchUpInside += delegate { 
				string text = Event.Title + ", happens from " + Event.Start.ToShortTimeString() + ". to " + Event.End.ToShortTimeString() + ". " + Event.Desc;
				TextToSpeechImplementation.Speak(text);
			};
		}


		void seguetoEdit()
		{
			PerformSegue("secondEditSegue", this);

		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if ((Event.Desc == " " || Event.Desc == "" )&& indexPath.Section == 1)
			{
				return 0;
			}
			return base.GetHeightForRow(tableView, indexPath);
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			if ((Event.Desc == " " || Event.Desc == "" )&& section == 1)
			{
				return 0;
			}
			return base.GetHeightForHeader(tableView, section);
		}

		[Action("UnwindToEventViewController:")]
		public void UnwindToEventViewController(UIStoryboardSegue segue)
		{
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (segue.Identifier == "secondEditSegue")
			{
				var transfer = segue.DestinationViewController as EditEventController;
				transfer.Event = Event;
			}
		}
	}
}