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

        private int segmentIndex = 6;//(int)DateTime.Now.DayOfWeek;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            NavigationController.NavigationBar.BarTintColor = UIColor.White;
            NavigationController.NavigationBar.TintColor = UIColor.Red;
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
            /*
            addButton.Enabled = false;
            addButton.TintColor = UIColor.Clear;
            */
            enableEditButton.Clicked += ToggleEditing;
            

        }

        void valueChange(object sender, EventArgs ea) {
            var futureViewController = ModelController.GetViewController((int)segmentedControl.SelectedSegment, Storyboard);
            var viewController = new UIViewController[] { futureViewController };
            if (segmentIndex < segmentedControl.SelectedSegment)
                PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Forward, true, null);
            else if (segmentedControl.SelectedSegment < segmentIndex)
                PageViewController.SetViewControllers(viewController, UIPageViewControllerNavigationDirection.Reverse, true, null);
            segmentIndex = (int)segmentedControl.SelectedSegment;
        }


        public static bool isEditingEnabled = true;
        void ToggleEditing(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("https://goo.gl/forms/pd8e5aonYweOewLw1"));

        }

    }
}