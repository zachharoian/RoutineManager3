using Foundation;
using System;
using UIKit;
using UserNotifications;

namespace App12
{
    public partial class RootViewController : UIViewController
    {
        public ModelController ModelController
        {
            get;
            private set;
        }

        public UIPageViewController PageViewController
        {
            get;
            private set;
        }

        public RootViewController (IntPtr handle) : base (handle)
        {
        }

		public static RootViewController CurrentRootViewController;

        private int segmentIndex = (int)DateTime.Now.DayOfWeek;

		public static bool consentComfirmed = false;

		public static bool isEditingEnabled = DataAccess.GetEdit();

		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0.012f, 0.663f, 0.957f);
			NavigationController.NavigationBar.TintColor = UIColor.White;
            ModelController = new ModelController();
            PageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
			//PageViewController.WeakDelegate = this;
            var startingViewController = ModelController.GetViewController(segmentIndex, Storyboard);
            var viewController = new UIViewController[] { startingViewController };
            PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, false, null);
            PageViewController.DataSource = ModelController;
            AddChildViewController(PageViewController);
            View.AddSubview(PageViewController.View);
            segmentedControl.SelectedSegment = segmentIndex;
			PageViewController.DidFinishAnimating += valueChangeFromSwipe;
            segmentedControl.ValueChanged += valueChange;
			consentComfirmed = DataAccess.GetKey();
            UpdatePrompt();
			isEditingEnabled = DataAccess.GetEdit();
			if (consentComfirmed == false)
				PerformSegue("consentForm", null);
			CurrentRootViewController = this;
        }


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			var startingViewController = ModelController.GetViewController(segmentIndex, Storyboard);
			var viewController = new UIViewController[] { startingViewController };
			PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, false, null);
			UpdatePrompt();
            if (isEditingEnabled == true)
            {
                addButton.Enabled = true;
                addButton.TintColor = null;
            }else
            {
                addButton.Enabled = false;
                addButton.TintColor = UIColor.Clear;
            }
            
		}




        void valueChange(object sender, EventArgs ea) {
            var futureViewController = ModelController.GetViewController((int)segmentedControl.SelectedSegment, Storyboard);
            var viewController = new UIViewController[] { futureViewController };
            if (segmentIndex < segmentedControl.SelectedSegment)
                PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, true, null);
            else if (segmentedControl.SelectedSegment < segmentIndex)
                PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Reverse, true, null);
            segmentIndex = (int)segmentedControl.SelectedSegment;
            UpdatePrompt();
        }

		void valueChangeFromSwipe(object sender, EventArgs ea)
		{
			int newIndex = ModelController.IndexOf((MasterViewController)PageViewController.ViewControllers[0]);
			segmentedControl.SelectedSegment = newIndex;
			segmentIndex = newIndex;
			UpdatePrompt();

			
		}

        private void UpdatePrompt()
        {
            if (segmentIndex == (int)DateTime.Now.DayOfWeek)
            {
                NavigationItem.Prompt = "Today's Agenda";
            }
            else if (segmentIndex == (int)DateTime.Now.AddDays(1).DayOfWeek)
            {
                NavigationItem.Prompt = "Tomorrow's Agenda";
            }
            else if (segmentIndex == (int)DateTime.Now.AddDays(-1).DayOfWeek)
            {
                NavigationItem.Prompt = "Yesterday's Agenda";
            }
            else
            {
                DateTime obj = new DateTime(2017, 1, segmentIndex + 1);
                NavigationItem.Prompt = obj.DayOfWeek.ToString() + "'s Agenda";
            }
        }

		[Action("UnwindFromConsent:")]
		public void UnwindFromConsent(UIStoryboardSegue segue)
		{
		}
    }
}