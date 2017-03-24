using System;

using SQLite;
using UserNotifications;
using Foundation;
using UIKit;
using System.IO;
using CoreGraphics;
using System.Drawing;

namespace App12
{
	public enum TypeOfImage { Default, Custom }
	public class EventData
	{
		//  SQLite setup information
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int ID { get; set; }

		//  Event Properties
		public string Title { get; set; }
		public string Desc { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Image { get; set; }


		public int Sunday { get; set; }
		public int Monday { get; set; }
		public int Tuesday { get; set; }
		public int Wednesday { get; set; }
		public int Thursday { get; set; }
		public int Friday { get; set; }
		public int Saturday { get; set; }

		public int Color { get; set; } = -1;
		public int TintColor { get; set; } = -1;

		public int Displayed { get; set; } = 0;

		public int Repeat { get; set; } = 1;

		public TypeOfImage TypeOfImage { get; set; }

		public UIImage GetImage(bool isThumbnail)
		{
			switch (TypeOfImage)
			{
				//	For icons selected by the app - no need for thumbnail
				case TypeOfImage.Default:
					{
						try
						{
							Console.WriteLine("Get Image: " + Image);
							return UIImage.FromFile(Image);
						}
						catch
						{
							return UIImage.FromFile("alarm.png");
						}
					}

				//	For images selected by the user
				case TypeOfImage.Custom:
					{
						if (isThumbnail == true)
						{
							Console.WriteLine("From GetImage: " + Filename(true, true));
							return UIImage.FromFile(Filename(true, true));
						}
						return UIImage.FromFile(Filename(false, true));
					}
				default:
					return UIImage.FromFile("alarm.png");
			}
		}
		public void SetImage(UIImage image, string newTitle, TypeOfImage newImageType)
		{
			switch (newImageType)
			{
				//	Default icon - find image, and set icon
				case TypeOfImage.Default:
					{
						Title = newTitle;
						Image = FindImage.ParseForImage(Title);
						break;
					}
				case TypeOfImage.Custom:
					{
						var thumbnailPath = Filename(true, true);
						var fullPath = Filename(false, true);
						//	Save original image
						var fullPathData = image.AsPNG();
						NSError fullErr;
						fullPathData.Save(fullPath, false, out fullErr);

						//	Create the mask with the right tint
						var mask = UIImage.FromFile("imagemask.png");
						Console.WriteLine(mask.Size);
						mask = mask.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
						var view = new UIImageView(mask);
						view.TintColor = AgendaCell.GetColor(Color);
						UIGraphics.BeginImageContextWithOptions(mask.Size, false, 0);
						var context = UIGraphics.GetCurrentContext();
						view.Layer.RenderInContext(context);
						mask = UIGraphics.GetImageFromCurrentImageContext();
						Console.WriteLine(mask.Size);
						UIGraphics.EndImageContext();
						image = CropImage(image, 0,0,240,240);
						//	Draw the thumbnail image
						UIGraphics.BeginImageContext(new CGSize(240, 240));
						image.Draw(new CGRect(0, 0, 240, 240));
						mask.Draw(new CGRect(0, 0, 240, 240));
						var export = UIGraphics.GetImageFromCurrentImageContext();
						UIGraphics.EndImageContext();
						var thumbnailPathData = export.AsPNG();

						NSError thumbErr;
						thumbnailPathData.Save(thumbnailPath, false, out thumbErr);
						Console.WriteLine("Error: " + thumbErr);
						thumbnailPathData.Save(thumbnailPath + "_notif.png", false, out thumbErr);
						/*
						UIImage imageEdited;
						//UIGraphics.BeginImageContext(new CGSize(240, 240));
						//var context = UIGraphics.GetCurrentContext();
						//context.DrawImage(new CGRect(0, 0, 240, 240), image.CGImage);
						//image.Draw(new CGRect(0, 0, 240, 240));
						imageEdited = MaxResizeImage(image, 240, 240);//UIGraphics.GetImageFromCurrentImageContext();
						//UIGraphics.EndImageContext();
						var data = imageEdited.AsPNG();
						data.Save(Filename(true, true), false, out fullErr);
						*/
						TintColor = Color;
						break;
					}
			}
			TintColor = Color;
			TypeOfImage = newImageType;
		}

		UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, int width, int height)
		{
			var imgSize = sourceImage.Size;
			var maxResizeFactor = Math.Max(width / imgSize.Width, height / imgSize.Height);
			UIGraphics.BeginImageContext(new SizeF(width, height));
			var context = UIGraphics.GetCurrentContext();
			var clippedRect = new RectangleF(0, 0, width, height);
			context.ClipToRect(clippedRect);
			var drawRect = new RectangleF(-crop_x, -crop_y, (float)(imgSize.Width*maxResizeFactor), (float)(imgSize.Height*maxResizeFactor));
			sourceImage.Draw(drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return modifiedImage;
		}
		string Filename(bool isThumbnail, bool fullPath)
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string path = null;
			if (ID < 10)
			{
				path = "IMG_000" + ID.ToString();
			}
			else if (ID < 100)
			{
				path = "IMG_00" + ID.ToString();
			}
			else if (ID < 1000)
			{
				path = "IMG_0" + ID.ToString();
			}
			else if (ID < 10000)
			{
				path = "IMG_" + ID.ToString();
			}
			if (isThumbnail == true)
			{
			}
			else
			{
				path = path + "_Full.png";
			}
			Console.WriteLine("Before combine: "+path);
			if (fullPath == true)
			{
				path = Path.Combine(documents, path);
				Console.WriteLine("After combine: "+path);
			}
			
			return path;
		}

		public void enableNotification()
		{
			if (Repeat == 1)
			{
				if (Sunday == 0 && Monday == 0 && Tuesday == 0 && Wednesday == 0 && Thursday == 0 && Friday == 0 && Saturday == 0)
				{
					Repeat = 0;
				}
				var attachmentID = "image";
				var options = new UNNotificationAttachmentOptions();
				NSUrl imagePath = null;

				if (TypeOfImage == TypeOfImage.Default)
				{
					imagePath = NSUrl.FromFilename(Image);
				}
				else
				{
					imagePath = NSUrl.FromFilename(Filename(true, true) + "_notif.png");
				}

				NSError error;
				var attachment = UNNotificationAttachment.FromIdentifier(attachmentID, imagePath, options, out error);
				Console.WriteLine(error);
				var content = new UNMutableNotificationContent();
				content.Title = Title;
				if (Desc != "")
				{
					content.Body = Desc;
				}

				content.Subtitle = Start.ToShortTimeString() + " - " + End.ToShortTimeString();
				//content.CategoryIdentifier = "default";
				content.Attachments = new UNNotificationAttachment[] { attachment };
				content.Sound = UNNotificationSound.GetSound("notification.wav");
				var trigger = UNCalendarNotificationTrigger.CreateTrigger(ConvertDateTimeToNSDate(Start), false);


				var requestID = Convert.ToString(ID);
				var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

				UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => { if (err != null) { Console.WriteLine("Error: " + err); } });
			}
		}

		public void disableNotification()
		{
			UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { Convert.ToString(ID) });
		}

		NSDateComponents ConvertDateTimeToNSDate(DateTime date)
		{
			var comps = new NSDateComponents();
			var days = getTableItems(false);
			DateTime tempDay = DateTime.Now;
			Console.WriteLine("Conversion Started");
			int count = 0;
			foreach (bool a in days)
			{
				if (a == true)
				{
					count++;
				}
			}
			if (count == 0)
			{
				if (Repeat == 0)
				{
					tempDay = Start;
				}
				else
				{
					tempDay = date;
				}
			}
			else
			{
				DateTime j;
				if (date.Day == DateTime.Now.Day && ((date.Hour < DateTime.Now.Hour) || (date.Hour == DateTime.Now.Hour && date.Minute < DateTime.Now.Hour)))
				{
					Console.WriteLine("Add days");
					j = DateTime.Now.AddDays(1);

				}
				else
				{
					Console.WriteLine("Didn't add days");
					j = DateTime.Now;
				}
				for (int i = (int)j.DayOfWeek; i != ((int)DateTime.Now.DayOfWeek - 1); i++)
				{
					Console.WriteLine("i: " + i);
					if (i >= 7)
					{
						i -= 7;
					}
					if (days[i] == true)
					{
						Console.WriteLine(DateTime.Now.Hour.CompareTo(date.Hour) + " and " + DateTime.Now.Minute.CompareTo(date.Minute));
						if (i == (int)DateTime.Now.DayOfWeek && ((DateTime.Now.Hour.CompareTo(date.Hour) == -1) || (DateTime.Now.Hour.CompareTo(date.Hour) == 0 && DateTime.Now.Minute.CompareTo(date.Minute) == -1)))
						{
							Console.WriteLine("Same day");
							tempDay = j;
						}
						else
						{

							Console.WriteLine("Completed at i: " + i);
							tempDay = Next(j, i);
						}
						break;

					}
				}
			}

			comps.Year = tempDay.Year;
			comps.Month = tempDay.Month;
			comps.Day = tempDay.Day;
			comps.Hour = date.Hour;
			comps.Minute = date.Minute;
			comps.Second = date.Second;
			Console.WriteLine(comps);

			return comps;
		}

		public DateTime Next(DateTime from, int DayOfWeek)
		{
			var start = (int)from.DayOfWeek;
			int target = DayOfWeek;
			if (target <= start)
				target += 7;
			return from.AddDays(target - start);
		}
		public void convertSevenItemArray(bool[] tempArray)
		{
			if (tempArray[0] == true)
				Sunday = 1;
			else
				Sunday = 0;

			if (tempArray[1] == true)
				Monday = 1;
			else
				Monday = 0;

			if (tempArray[2] == true)
				Tuesday = 1;
			else
				Tuesday = 0;

			if (tempArray[3] == true)
				Wednesday = 1;
			else
				Wednesday = 0;

			if (tempArray[4] == true)
				Thursday = 1;
			else
				Thursday = 0;

			if (tempArray[5] == true)
				Friday = 1;
			else
				Friday = 0;

			if (tempArray[6] == true)
				Saturday = 1;
			else
				Saturday = 0;

		}
		public bool[] getTableItems(bool includeNever)
		{

			bool[] returnList = new bool[8];
			int count = 0;
			foreach (bool a in returnList)
			{
				returnList[count] = false;
				count++;
			}

			if (Sunday == 1)
				returnList[1] = true;
			if (Monday == 1)
				returnList[2] = true;
			if (Tuesday == 1)
				returnList[3] = true;
			if (Wednesday == 1)
				returnList[4] = true;
			if (Thursday == 1)
				returnList[5] = true;
			if (Friday == 1)
				returnList[6] = true;
			if (Saturday == 1)
				returnList[7] = true;
			count = 0;
			foreach (bool a in returnList)
			{
				if (a == false)
					count++;
			}
			if (includeNever == true)
			{
				if (count == 8)
					returnList[0] = true;
				return returnList;
			}
			bool[] returnList2 = new bool[7];
			for (int i = 1; i < 8; i++)
			{
				returnList2[i - 1] = returnList[i];
			}
			return returnList2;
		}
	}
}