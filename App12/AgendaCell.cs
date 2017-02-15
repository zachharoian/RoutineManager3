using UIKit;
using CoreGraphics;

namespace App12
{
    //
    //
    //  AgendaCell: The custom cell used for the Agenda screen in the app.
    //  
    //
    public partial class AgendaCell : UITableViewCell
    {
        #region Variables
        UILabel title, time;
        UILabel desc;
        UIColor backgroundColor = UIColor.White;
        UIView card;
        UIImageView imageView;
        #endregion

        #region Constructor
        //
        //  AgendaCell(): Constructor 
        //
        public AgendaCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
        {
            //  Set the background color of the table to be a mid-gray.
            ContentView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;

            //  Instantiate the Title
            title = new UILabel()
            {   //  Set text properties
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Font = UIFont.FromName("Helvetica-Bold", 20f)
            };

            //  Instantiate the Description
            desc = new UILabel()
            {   // Set text properties
                Font = UIFont.SystemFontOfSize(17),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 3
                
                };
            
            //  Instantiate the Time label
            time = new UILabel()
            {   //  Set text properties
                Font = UIFont.SystemFontOfSize(17),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear
            };
            
            //  Instantiate the card behind the text
            card = new UIView()
            {
                BackgroundColor = backgroundColor,
            };
            
            //  Set view properties
            card.Layer.ShadowColor = UIColor.Black.CGColor;
            card.Layer.ShadowOpacity = 0.1f;
            card.Layer.ShadowOffset = new CGSize(0, 2);
            card.Layer.CornerRadius = 2;

            //  Instantiate the iamge view
            imageView = new UIImageView();
            
            //  Add the above items to the view.
            ContentView.AddSubviews(new UIView[] {card, title, desc, time, imageView});
        }
        #endregion

        #region Data Insertion
        //  
        //  UpdateCell(): Gets the data from the input object and fills the cell information.
        //
        public void UpdateCell(EventData obj)
        {
            //  Set the labels to the text from the object.
            title.Text = obj.Title;
            desc.Text = obj.Desc;
            time.Text = obj.Start.ToShortTimeString() + " - " + obj.End.ToShortTimeString();
            //  Try/Catch incase the image path is not valid. Fallback is the default alarm picture.
            try
            {
                imageView.Image = UIImage.FromFile(obj.Image);
            }
            catch
            {
                imageView.Image = UIImage.FromFile("alarm.png");
            }
            
        }
        #endregion

        #region Layout
        //
        //  LayoutSubviews(): Sets the placement of the views within the cell.
        //
        public override void LayoutSubviews()
        {
            //  140 px Total height of the cell
            //  130 px card height
            //  120 px margins height
            base.LayoutSubviews();
            //  Accounts for image size
            int imageOffset = 80 + 10;
            
            //  Sets frames for views.
            card.Frame = new CGRect(5, 5, ContentView.Bounds.Width - 10, ContentView.Bounds.Height - 10);
            title.Frame = new CGRect(10 + imageOffset, 10, ContentView.Bounds.Width - (10 + imageOffset)-10, 23);
            time.Frame = new CGRect(10 + imageOffset, 33, ContentView.Bounds.Width - (10 + imageOffset)-10, 19);
            desc.Frame = new CGRect(10 + imageOffset, 52, ContentView.Bounds.Width - (10 + imageOffset)-10, 78);
            //  Image will be 10 pixels from the left of the cell, 10 pixels from the top, 80 pixels diameter
            imageView.Frame = new CGRect(10, 10, 80, 80);
            desc.SizeToFit();
            if (desc.Frame.Height > 78)
            {
                desc.Frame = new CGRect(desc.Frame.X, desc.Frame.Y, desc.Frame.Width, 78);
            }
        }
        #endregion
    }
}
