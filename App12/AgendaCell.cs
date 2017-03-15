using UIKit;
using CoreGraphics;
using CoreImage;
using System;

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
		public UILabel title, time, desc;
        UIColor backgroundColor = UIColor.White;
        UIView card, highlight;
        UIImageView imageView;
		public UIButton speech;
        #endregion

        #region Constructor
        //
        //  AgendaCell(): Constructor 
        //
        public AgendaCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
        {
			Console.WriteLine("AgendaCell Launched");
            //  Set the background color of the table to be a mid-gray.
            ContentView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;

            //  Instantiate the Title
            title = new UILabel()
            {   //  Set text properties
				TextColor = UIColor.FromRGB(30, 30,30),
                BackgroundColor = UIColor.Clear,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Font = UIFont.FromName("HelveticaNeue-Medium", 20f)
            };

			//  Instantiate the Time label
			time = new UILabel()
			{   //  Set text properties
				Font = UIFont.FromName("HelveticaNeue-LightItalic", 17f),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear
			};

			//  Instantiate the Description
			desc = new UILabel()
			{   // Set text properties
				Font = UIFont.FromName("HelveticaNeue-Light", 17f),
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3
                
             };

			highlight = new UIView();
            
            //  Instantiate the card behind the text
            card = new UIView()
            {
                BackgroundColor = backgroundColor,
            };
            
            //  Set view properties
            card.Layer.ShadowColor = UIColor.Black.CGColor;
            card.Layer.ShadowOpacity = 0.1f;
            card.Layer.ShadowOffset = new CGSize(0, 1);
            //card.Layer.CornerRadius = 2;

			card.Layer.ShadowRadius = 2;
			card.Layer.BorderColor = UIColor.FromRGB(220,220,220).CGColor;
			card.Layer.BorderWidth = 1;
            //  Instantiate the iamge view
			imageView = new UIImageView();

			speech = new UIButton();
			speech.SetBackgroundImage(UIImage.FromFile("speakers.png"), UIControlState.Normal);

            
            //  Add the above items to the view.
            ContentView.AddSubviews(new UIView[] {card, title, desc, time, highlight, imageView, speech});
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
			UIImage Image = new UIImage();
			//  Try/Catch incase the image path is not valid. Fallback is the default alarm picture.
            try
            {
                Image = UIImage.FromFile(obj.Image);
            }
            catch
            {
                Image = UIImage.FromFile("alarm.png");
            }
			/*
			CIImage coreImage = CIImage.FromCGImage(Image.CGImage);
			CIFilter filter = CIFilter.FromName("CIColorInvert");
			filter.Image = coreImage;
			CIImage result = filter.OutputImage;


			imageView.Image = new UIImage(result);
			imageView.Opaque = false;
			imageView.Alpha = 0.75f;*/
			highlight.BackgroundColor = GetColor(obj.Color);
			//UIImage test = Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			imageView.Image = Image;
			//imageView.TintColor = UIColor.White;
			//imageView.Alpha = 0.5f;
			Console.WriteLine("AgendaCell Finished.");
        }
		#endregion

		static public UIColor GetColor(int colorIndex)
		{
			switch (colorIndex)
			{
				case 0:
					return UIColor.FromRGB(0.957f, 0.263f, 0.212f);
				case 1:
					return UIColor.FromRGB(0.914f, 0.118f, 0.388f);
				case 2:
					return UIColor.FromRGB(0.612f, 0.153f, 0.690f);
				case 3:
					return UIColor.FromRGB(0.404f, 0.227f, 0.718f);
				case 4:
					return UIColor.FromRGB(0.247f, 0.318f, 0.710f);
				case 5:
					return UIColor.FromRGB(0.129f, 0.588f, 0.953f);
				case 6:
					return UIColor.FromRGB(0.012f, 0.663f, 0.957f);
				case 7:
					return UIColor.FromRGB(0.000f, 0.737f, 0.831f);
				case 8:
					return UIColor.FromRGB(0.000f, 0.588f, 0.533f);
				case 9:
					return UIColor.FromRGB(0.298f, 0.686f, 0.314f);
				case 10:
					return UIColor.FromRGB(0.545f, 0.765f, 0.290f);
				case 11:
					return UIColor.FromRGB(0.804f, 0.863f, 0.224f);
				case 12:
					return UIColor.FromRGB(1.000f, 0.922f, 0.231f);
				case 13:
					return UIColor.FromRGB(1.000f, 0.757f, 0.027f);
				case 14:
					return UIColor.FromRGB(1.000f, 0.596f, 0.000f);
				case 15:
					return UIColor.FromRGB(1.000f, 0.341f, 0.133f);
				default:
					return UIColor.White;
			}
		}

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
            title.Frame = new CGRect(10 + imageOffset, 10, ContentView.Bounds.Width - imageOffset-55, 23);
            time.Frame = new CGRect(10 + imageOffset, 33, ContentView.Bounds.Width - imageOffset-45, 19);
            desc.Frame = new CGRect(10 + imageOffset, 52, ContentView.Bounds.Width - imageOffset-10, 78);
            //  Image will be 10 pixels from the left of the cell, 10 pixels from the top, 80 pixels diameter
            imageView.Frame = new CGRect(10, 25, 80, 80);
			speech.Frame = new CGRect(ContentView.Bounds.Width - 50, 10, 40, 40);
			highlight.Frame = new CGRect(6, 6, 90-2, 130-2);
            desc.SizeToFit();
            if (desc.Frame.Height > 78)
            {
                desc.Frame = new CGRect(desc.Frame.X, desc.Frame.Y, desc.Frame.Width, 78);
            }
        }
        #endregion
    }
}
