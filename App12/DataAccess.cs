using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  Database Package
using SQLite;

//  File System Access Package
using System.IO;

namespace App12
{
    //
    //
    //  Database Access
    //
    //
    public class DataAccess
    {   
        //  Database path
        public static string dbPath { get; set; }

        //
        //  Saves the object in the database
        //
        public static void SaveObject(EventData obj)
        {
            //  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
            object locker = new object();
            lock (locker)
            {
                //  Sets the database path
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

                //  Connects to the database
                var db = new SQLiteConnection(dbPath);

                //  Creates the table for the data, if it doesn't exist already.
                db.CreateTable<EventData>();
                

                //  Inserts the object into the database.
                if (obj.ID != 0)
                {
                    SQLiteCommand command = new SQLiteCommand(db);
                    command.CommandText = "UPDATE EventData SET Title = '"+obj.Title+"', Desc = '"+obj.Desc+"', Start = '"+obj.Start+"', End = '"+obj.End+"', Image = '"+obj.Image+"' Where _id = '"+obj.ID+"'";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database updated");
                }
                else
                    db.Insert(obj);
                
            }

        }

        public static nint Count (DateTime Date)
        {
            object locker = new object();
            lock (locker)
            {
                //  Sets the database path
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

                //  Connects to the database
                var db = new SQLiteConnection(dbPath);

                //  Creates the table for the data, if it doesn't exist already.
                db.CreateTable<EventData>();

                //  Inserts the object into the database.
                List<EventData> temp = db.Table<EventData>().ToList();
                List<EventData> returnList = new List<EventData>();
                IOrderedEnumerable<EventData> sortQuery =
                    from EventData in temp
                    orderby EventData.Start //descending
                    select EventData;

                foreach (EventData EventData in sortQuery)
                {

                    if (EventData.DaysActive[(int)(Date.DayOfWeek)] == true || EventData.Start.Date == Date.Date)
                    {
                        returnList.Add(EventData);
                        Console.WriteLine("Event:" + EventData.Title + "\nTime: " + EventData.Start);
                    }
                }
                return returnList.Count;

            }

        }

        public static List<EventData> GetEvents(DateTime Date)
        {
            List<EventData> tempList = new List<EventData>();
            List<EventData> returnList = new List<EventData>();
            //  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
            object locker = new object();
            lock (locker)
            {
                //  Sets the database path
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

                //  Connects to the database
                var db = new SQLiteConnection(dbPath);

                //  Creates the table for the data, if it doesn't exist already.
                db.CreateTable<EventData>();

                //  Inserts the object into the database.
                tempList = db.Table<EventData>().ToList();

                IOrderedEnumerable<EventData> sortQuery =
                    from EventData in tempList
                    orderby EventData.Start //descending
                    select EventData;

                foreach (EventData EventData in sortQuery)
                {

                    if (EventData.DaysActive[(int)(Date.DayOfWeek)] == true || EventData.Start.Date == Date.Date)
                    {
                        returnList.Add(EventData);
                        Console.WriteLine("Event:" + EventData.Title + "\nTime: " + EventData.Start);
                    }
                }

            }
            return returnList;
        } 

        public static void DeleteObject (EventData obj)
        {
            //  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
            object locker = new object();
            lock (locker)
            {
                //  Sets the database path
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

                //  Connects to the database
                var db = new SQLiteConnection(dbPath);

                //  Creates the table for the data, if it doesn't exist already.
                db.CreateTable<EventData>();

                //  Inserts the object into the database.
                db.Delete(obj);
            }
        }

    }
}