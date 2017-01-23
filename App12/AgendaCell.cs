using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace App12
{
    public partial class AgendaCell : UITableViewCell
    {
        UILabel title, desc, time;
        UIColor backgroundColor = UIColor.White;
        UIView card;
        UIImage image;

        public AgendaCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            ContentView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
            
            title = new UILabel()
            {
                Font = UIFont.SystemFontOfSize(20),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
            desc = new UILabel()
            {
                
                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear
            };
            desc.LineBreakMode = UILineBreakMode.WordWrap;
            desc.Lines = 3;
            time = new UILabel()
            {
                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.Gray,
                BackgroundColor = UIColor.Clear

            };
            
            card = new UIView()
            {
                BackgroundColor = backgroundColor
            };
            card.Layer.ShadowColor = UIColor.Black.CGColor;
            card.Layer.ShadowOpacity = 0.1f;
            card.Layer.ShadowOffset = new CGSize(0, 2);
            card.Layer.CornerRadius = 1;

            image = new UIImage();

            ContentView.AddSubviews(new UIView[] {card, title, desc, time});
        }

        public void UpdateCell(string tempTitle, string tempDesc, DateTime start, DateTime end)
        {
            
            title.Text = tempTitle;
            desc.Text = tempDesc;
            time.Text = start.ToShortTimeString() + " - " + end.ToShortTimeString();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            int imageOffset = 35;
            card.Frame = new CGRect(5, 5, ContentView.Bounds.Width - 10, ContentView.Bounds.Height - 10);
            title.Frame = new CGRect(10 + imageOffset, 10, ContentView.Bounds.Width - (10 + imageOffset), 20);
            time.Frame = new CGRect(10 + imageOffset, 30, ContentView.Bounds.Width - (10 + imageOffset), 12);
            desc.Frame = new CGRect(10 + imageOffset, 42, ContentView.Bounds.Width - (10 + imageOffset), 12);
            //  Image will be 10 pixels from the left of the cell, 10 pixels from the top, 30 pixels diameter
            //  5 pixel margin from other text and card
            //image.Frame = new CGRect(10, 10, 30, 30);
                        
        }

    }
}
