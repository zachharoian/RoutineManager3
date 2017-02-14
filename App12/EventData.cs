using System;

using SQLite;
using UserNotifications;
using Foundation;

namespace App12
{
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

        public int Displayed { get; set; } = 0;

		public void enableNotification()
		{

            var attachmentID = "image";
            var options = new UNNotificationAttachmentOptions();
            NSUrl imagePath;
            try
            {
                imagePath = NSUrl.FromFilename(Image);
            }
            catch
            {
                imagePath = NSUrl.FromFilename("alarm.png");
            }
            NSError error;
            var attachment = UNNotificationAttachment.FromIdentifier(attachmentID, imagePath, options, out error);

            var content = new UNMutableNotificationContent();            
            content.Title = Title;
            if (Desc != "")
            {
                content.Body = Desc;
            }
            
            content.CategoryIdentifier = "default";
            content.Attachments = new UNNotificationAttachment[] { attachment };
            content.Sound = UNNotificationSound.Default;
            var trigger = UNCalendarNotificationTrigger.CreateTrigger(ConvertDateTimeToNSDate(Start), false);

            //var requestID = Convert.ToString(ID);
            var requestID = "sampleRequest";
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => { if (err != null) { Console.WriteLine("Error: " + err); }; });
        }

        private NSDateComponents ConvertDateTimeToNSDate(DateTime date)
		{
			NSDateComponents comps = new NSDateComponents();
			bool[] days = getTableItems(false);
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
				tempDay = date;
			}
			else {
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
						Console.WriteLine(DateTime.Now.Hour.CompareTo(date.Hour) +" and "+ DateTime.Now.Minute.CompareTo(date.Minute));
						if (i == (int)DateTime.Now.DayOfWeek && ((DateTime.Now.Hour.CompareTo(date.Hour) == -1) || (DateTime.Now.Hour.CompareTo(date.Hour) == 0 && DateTime.Now.Minute.CompareTo(date.Minute) == -1)))
						{
							Console.WriteLine("Same day");
							tempDay = j;
						}
						else {

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
			int start = (int)from.DayOfWeek;
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
				if (count == 0)
					returnList[0] = true;
				return returnList;
			}
			else
			{
				bool[] returnList2 = new bool[7];
				for (int i = 1; i < 8; i++)
				{
					returnList2[i - 1] = returnList[i];
				}
				return returnList2;
			}
		}
	}
}