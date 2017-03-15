using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UIKit;

namespace App12
{
    public class ModelController : UIPageViewControllerDataSource
    {
        List<DateTime> pageData;

        public ModelController ()
        {
            pageData = new List<DateTime>();
            pageData.Add(new DateTime(2017, 1, 1));
            pageData.Add(pageData[0].AddDays(1));
            pageData.Add(pageData[1].AddDays(1));
            pageData.Add(pageData[2].AddDays(1));
            pageData.Add(pageData[3].AddDays(1));
            pageData.Add(pageData[4].AddDays(1));
            pageData.Add(pageData[5].AddDays(1));
        }


        public MasterViewController GetViewController (int index, UIStoryboard storyboard)
        {
            if (index >= pageData.Count)
                return null;
            //  Create page
            var dataViewController = (MasterViewController)storyboard.InstantiateViewController("MasterViewController");
            //  Sets the day of the week for the controller
            dataViewController.Day = pageData[index];

            return dataViewController;
        }

        public int IndexOf(MasterViewController viewController)
        {   
            //  Finds the index of the specific page.
			return pageData.IndexOf(viewController.Day);
        }

        public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            Console.WriteLine("Next Page");
            //  Finds the index of the input page by turning it into a MasterViewController.
            int index = IndexOf((MasterViewController)referenceViewController);

            //  If the page is at the beginning or end of the array, return nothing.
            if (index == -1 || index == pageData.Count - 1)
                return null;

            return GetViewController(index + 1, referenceViewController.Storyboard);
        }

        public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            //  Finds the index of the input page by turning it into a MasterViewController.
            int index = IndexOf((MasterViewController)referenceViewController);

            //  If the page is at the beginning or end of the array, return nothing.
            if (index == -1 || index == 0)
                return null;

            return GetViewController(index - 1, referenceViewController.Storyboard);
        }
    }
}