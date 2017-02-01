using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;
using UIKit;
using UserNotifications;

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


        //  Constructor
        public EventData()
        {
        }

        //  Overloaded Constructor
        public EventData(string tempTitle, string tempDesc, DateTime tempStart, DateTime tempEnd)
        {
            Title = tempTitle;
            Desc = tempDesc;
            Start = tempStart;
            End = tempEnd;
        }

        public EventData(string tempTitle, string tempDesc, DateTime tempStart, DateTime tempEnd, string tempImage, bool[] tempArray)
        {
            Title = tempTitle;
            Desc = tempDesc;
            Start = tempStart;
            End = tempEnd;
            Image = tempImage;
            if (tempArray[0] == true)
                Sunday= 1;
            else
                Sunday= 0;

            if (tempArray[1] == true)
                Monday= 1;
            else
                Monday= 0;

            if (tempArray[2] == true)
                Tuesday= 1;
            else
                Tuesday= 0;

            if (tempArray[3] == true)
                Wednesday= 1;
            else
                Wednesday= 0;

            if (tempArray[4] == true)
                Thursday= 1;
            else
                Thursday= 0;

            if (tempArray[5] == true)
                Friday= 1;
            else
                Friday= 0;

            if (tempArray[6] == true)
                Saturday= 1;
            else
                Saturday= 0;          

        }

        public EventData(string tempTitle, string tempDesc, DateTime tempStart, DateTime tempEnd, int tempID, string tempImage)
        {
            Title = tempTitle;
            Desc = tempDesc;
            Start = tempStart;
            End = tempEnd;
            ID = tempID;
            Image = tempImage;
  
            
        }

        
        
    }
}