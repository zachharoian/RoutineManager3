using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;

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

        //  Constructor
        public EventData()
        {
        }

        //  Overloaded Constructor
        public EventData(string tempTitle, string tempDesc)
        {
            Title = tempTitle;
            Desc = tempDesc;
        }
    }
}