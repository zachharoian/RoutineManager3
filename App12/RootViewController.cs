using Foundation;
using System;
using UIKit;

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

        private int segmentIndex = (int)DateTime.Now.DayOfWeek;

		public static bool consentComfirmed = false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            NavigationController.NavigationBar.BarTintColor = UIColor.White;
			NavigationController.NavigationBar.TintColor = UIColor.Purple;
            ModelController = new ModelController();
            PageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
            PageViewController.WeakDelegate = this;
            var startingViewController = ModelController.GetViewController(segmentIndex, Storyboard);
            var viewController = new UIViewController[] { startingViewController };
            PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, false, null);
            PageViewController.DataSource = null;
            AddChildViewController(PageViewController);
            View.AddSubview(PageViewController.View);
            segmentedControl.SelectedSegment = segmentIndex;
            segmentedControl.ValueChanged += valueChange;
			consentComfirmed = DataAccess.GetKey();
            //enableEditButton.Clicked += ToggleEditing;
            UpdatePrompt();
			if (consentComfirmed == false)
				PerformSegue("consentForm", null);
            
        }


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			PageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
			PageViewController.WeakDelegate = this;
			var startingViewController = ModelController.GetViewController(segmentIndex, Storyboard);
			var viewController = new UIViewController[] { startingViewController };
			PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, false, null);
			PageViewController.DataSource = null;
            PageViewController.View.Frame = new CoreGraphics.CGRect(0, base.View.Bounds.Y, base.View.Bounds.Width, base.View.Bounds.Height);
			AddChildViewController(PageViewController);
			View.AddSubview(PageViewController.View);
            if (isEditingEnabled == true)
            {
                addButton.Enabled = true;
                addButton.TintColor = null;
            }else
            {
                addButton.Enabled = false;
                addButton.TintColor = UIColor.Clear;
            }
            UpdatePrompt();
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

        public static bool isEditingEnabled = false;

        

		[Action("UnwindFromConsent:")]
		public void UnwindFromConsent(UIStoryboardSegue segue)
		{
		}
    }
}