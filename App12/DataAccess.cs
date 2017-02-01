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
                    command.CommandText = "UPDATE EventData SET Title = '" + obj.Title + "', Desc = '" + obj.Desc + "', Start = '" + obj.Start + "', End = '" + obj.End + "', Image = '" + obj.Image + "' Where _id = '" + obj.ID + "'";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database updated");
                }
                else
                {
                    db.Insert(obj);
                    Console.WriteLine("Object Added. Tuesday value:" + obj.Tuesday);
                    var o = db.Get<EventData>(obj.ID);
                    Console.WriteLine("ID: " +obj.ID+ " -> " +o.ID);
                    Console.WriteLine("Title: "+obj.Title + " -> " + o.Title);
                    Console.WriteLine("Desc: " + obj.Desc + " -> " + o.Desc);
                    Console.WriteLine("Start: "+ obj.Start + " -> " + o.Start);
                    Console.WriteLine("End: " + obj.End + " -> " + o.End);
                    Console.WriteLine("Sun: "+obj.Sunday + " -> " + o.Sunday);
                    Console.WriteLine("Mon: "+obj.Monday + " -> " + o.Monday);
                    Console.WriteLine("Tues: "+obj.Tuesday + " -> " + o.Tuesday);
                    Console.WriteLine("Wed: "+obj.Wednesday + " -> " + o.Wednesday);
                    Console.WriteLine("Thur: "+obj.Thursday + " -> " + o.Thursday);
                    Console.WriteLine("Fri: "+obj.Friday + " -> " + o.Friday);
                    Console.WriteLine("Sat: "+obj.Saturday + " -> " + o.Saturday);
                    Console.WriteLine("Image: "+obj.Image + " -> " + o.Image);
                    
                }
                
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

                var table = db.Table<EventData>();
                Console.WriteLine(table);

                //  Inserts the object into the database.
                List<EventData> temp = db.Table<EventData>().ToList();
                List<EventData> returnList = new List<EventData>();
                IOrderedEnumerable<EventData> sortQuery =
                    from EventData in temp
                    orderby EventData.Start //descending
                    select EventData;

                foreach (EventData EventData in sortQuery)
                {
                    int DayOftheWeek = (int)(Date.DayOfWeek);
                    switch (DayOftheWeek)
                    {
                        
                        case 0:
                            if (EventData.Sunday == 1)
                                returnList.Add(EventData);
                            break;
                        case 1:
                            if (EventData.Monday == 1)
                                returnList.Add(EventData);
                            break;
                        case 2:
                            if (EventData.Tuesday == 1)
                            {
                                returnList.Add(EventData);
                            }
                            break;
                        case 3:
                            if (EventData.Wednesday == 1)
                                returnList.Add(EventData);
                            break;
                        case 4:
                            if (EventData.Thursday == 1)
                                returnList.Add(EventData);
                            break;
                        case 5:
                            if (EventData.Friday == 1)
                                returnList.Add(EventData);
                            break;
                        case 6:
                            if (EventData.Saturday == 1)
                                returnList.Add(EventData);
                            break;
                        default:
                            break;
                        
                    }
                    if (EventData.Start.Date == Date.Date)
                    
                        returnList.Add(EventData);

                    /*
                    if (EventData.DaysActive[(int)(Date.DayOfWeek)] == true || EventData.Start.Date == Date.Date)
                    {
                        returnList.Add(EventData);
                        Console.WriteLine("Event:" + EventData.Title + "\nTime: " + EventData.Start);
                    }
                    */
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
                Console.WriteLine();
                foreach (EventData EventData in sortQuery)
                {                    
                    int DayOftheWeek = (int)(Date.DayOfWeek);
                    
                    Console.WriteLine("DateoftheWeek = " + DayOftheWeek);
                    switch (DayOftheWeek)
                    {
                        case 0:
                            int temp = EventData.Sunday;
                            if (EventData.Sunday == 1)
                                returnList.Add(EventData);
                            break;
                        case 1:
                            if (EventData.Monday == 1)
                                returnList.Add(EventData);
                            break;
                        case 2:
                            Console.WriteLine("About to be added");
                            if (EventData.Tuesday == 1)
                            {
                                Console.WriteLine("Added " + EventData.Title + "!");
                                returnList.Add(EventData);
                                
                            }
                            break;
                        case 3:
                            if (EventData.Wednesday == 1)
                                returnList.Add(EventData);
                            break;
                        case 4:
                            if (EventData.Thursday == 1)
                                returnList.Add(EventData);
                            break;
                        case 5:
                            if (EventData.Friday == 1)
                                returnList.Add(EventData);
                            break;
                        case 6:
                            if (EventData.Saturday == 1)
                                returnList.Add(EventData);
                            break;
                        default:
                            break;

                    }
                    

                    if (EventData.Start.Date == Date.Date)
                    
                        returnList.Add(EventData);
                        
                    /*
                    Console.WriteLine(EventData.Title + ": Days Active");
                    for (int i = 0; i < 7; i++)
                    {
                        Console.WriteLine("     "+EventData.DaysActive[i]);
                    }
                    
                    if (EventData.DaysActive[(int)(Date.DayOfWeek)] == true || EventData.Start.Date == Date.Date)
                    {
                        
                        returnList.Add(EventData);
                        Console.WriteLine("Event:" + EventData.Title + "\nTime: " + EventData.Start);
                    }
                    */
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