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
        public bool[] DaysActive = new bool[7] { false, false, false, false, false, false, false };
        //  Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6
        


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

        public EventData(string tempTitle, string tempDesc, DateTime tempStart, DateTime tempEnd, string tempImage)
        {
            Title = tempTitle;
            Desc = tempDesc;
            Start = tempStart;
            End = tempEnd;
            Image = tempImage;

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