using Foundation;
using System;
using UIKit;
using App12;
namespace RoutineManager
{
    public partial class EditRootViewController : UINavigationController
    {
        public EditRootViewController (IntPtr handle) : base (handle)
        {
        }

		public static EventData Event;
		public static NSIndexPath currentTableCell;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationBar.BarTintColor = AgendaCell.GetColor(Event.Color);
		}
    }
}