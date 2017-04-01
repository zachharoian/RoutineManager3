using System;
using UIKit;
using CoreGraphics;

namespace RoutineManager
{
    public partial class PresentImageView : UIViewController
    {
        public PresentImageView (IntPtr handle) : base (handle)
        {
        }

		public static UIImageView currentImage;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var controller = EventViewController.CurrentViewController;
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0.012f, 0.663f, 0.957f);
			NavigationController.NavigationBar.TintColor = UIColor.White;
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			var image = new UIImageView(controller.Event.GetImage(false));

			if (controller.Event.TypeOfImage == App12.TypeOfImage.Default)
			{
				UIGraphics.BeginImageContext(new CGSize(image.Image.Size.Width, image.Image.Size.Height));
				var context = UIGraphics.GetCurrentContext();
				context.SetFillColor(App12.AgendaCell.GetColor(controller.Event.Color).CGColor);
				context.FillRect(new CGRect(0, 0, image.Image.Size.Width, image.Image.Size.Height));
				context.DrawImage(new CGRect(0, 0, image.Image.Size.Width, image.Image.Size.Height), image.Image.CGImage);
				image.Image = UIGraphics.GetImageFromCurrentImageContext();
				image.Image = new UIImage(image.Image.CGImage, 0, UIImageOrientation.DownMirrored);
				UIGraphics.EndImageContext();
			}
			image.SizeToFit();
			imageView.ContentSize = image.Image.Size;
			imageView.AddSubview(image);

			var imageViewSize = image.Bounds.Size;
			var scrollViewSize = imageView.Bounds.Size;
			var widthScale = scrollViewSize.Width / imageViewSize.Width;
			var heightScale = scrollViewSize.Height / scrollViewSize.Height;

			var minZoomScale = Math.Min(widthScale, heightScale);

			//imageView.MaximumZoomScale = 3f;
			imageView.MinimumZoomScale = (nfloat) minZoomScale;
			imageView.ViewForZoomingInScrollView += (UIScrollView scrollView) => { return image; };
			currentImage = image;
			imageView.SetZoomScale((nfloat)minZoomScale, false);
		}
    }

	public class ImageDelegate : UIScrollViewDelegate
	{
		public override void DidZoom(UIScrollView scrollView)
		{
			base.DidZoom(scrollView);
			UIImageView imageView = PresentImageView.currentImage;

			CGRect innerFrame = imageView.Frame;
			CGRect scrollerBounds = scrollView.Bounds;

			if ((innerFrame.Size.Width < scrollerBounds.Size.Width) || (innerFrame.Size.Height < scrollerBounds.Size.Height))
			{
				var tempx = imageView.Center.X - (scrollerBounds.Size.Width / 2);
				var tempy = imageView.Center.Y - (scrollerBounds.Size.Height / 2);
				var myScrollViewOffset = new CGPoint(tempx, tempy);

				scrollView.ContentOffset = myScrollViewOffset;
			}

			var anEdgeInset = new UIEdgeInsets( 0, 0, 0, 0 );
			if (scrollerBounds.Size.Width > innerFrame.Size.Width)
			{
				anEdgeInset.Left = (scrollerBounds.Size.Width - innerFrame.Size.Width) / 2;
				anEdgeInset.Right = -anEdgeInset.Left;
			}

			if (scrollerBounds.Size.Height > innerFrame.Size.Height)
			{
				anEdgeInset.Top = (scrollerBounds.Size.Height - innerFrame.Size.Height) / 2;
				anEdgeInset.Bottom = -anEdgeInset.Top;
			}
			scrollView.ContentInset = anEdgeInset;
		}
		
	}
}