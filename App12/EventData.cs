using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;
using UIKit;

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