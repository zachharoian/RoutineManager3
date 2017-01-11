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
        public UIColor BackgroundColor { get; set; }

        //  Constructor
        public EventData()
        {
        }

        //  Overloaded Constructor
        public EventData(string tempTitle, string tempDesc, DateTime tempStart, DateTime tempEnd, UIColor color)
        {
            Title = tempTitle;
            Desc = tempDesc;
            Start = tempStart;
            End = tempEnd;
            BackgroundColor = color;
        }
    }
}