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

		public static bool GetKey()
		{
			//  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
			object locker = new object();
			lock (locker)
			{
				//  Sets the database path
				string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

				//  Connects to the database
				var db = new SQLiteConnection(dbPath);
				db.CreateTable<Key>();
				Key obj;
				try
				{
					obj = db.Get<Key>(1);
				}
				catch 
				{
					obj = new Key { ConsentFormComfirmation = 0 };
				}

				if (obj.ConsentFormComfirmation == 1)
					return true;
				else
					return false;

			}
		}
		class Key
		{
			[PrimaryKey, AutoIncrement, Column("ID")]
			public int ID { get; set; }
			public int ConsentFormComfirmation { get; set;}
		}

		public static void SaveKey()
		{
			//  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
			object locker = new object();
			lock (locker)
			{
				//  Sets the database path
				string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

				//  Connects to the database
				var db = new SQLiteConnection(dbPath);

				db.CreateTable<Key>();
				int temp;
				if (RootViewController.consentComfirmed == true)
					temp = 1;
				else
					temp = 0;
				db.Insert(new Key { ConsentFormComfirmation = temp} );
			}
		}

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
                    command.CommandText = "UPDATE EventData SET Title = '" + obj.Title + 
                                          "', Desc = '" + obj.Desc + 
                                          "', Start = '" + obj.Start + 
                                          "', End = '" + obj.End + 
                                          "', Image = '" + obj.Image + 
                                          "', Sunday = '" + obj.Sunday + 
                                          "', Monday = '" + obj.Monday + 
                                          "', Tuesday = '" + obj.Tuesday + 
                                          "', Wednesday = '"+ obj.Wednesday + 
                                          "', Thursday = '" + obj.Thursday + 
                                          "', Friday = '" + obj.Friday + 
                                          "', Saturday = '" + obj.Saturday + 
                                          "', Where _id = '" + obj.ID + "'";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database updated");
                }
                else
                {
                    db.Insert(obj);
                    
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

                //  Inserts the object into the database.
                List<EventData> tempList = db.Table<EventData>().ToList();
                List<EventData> returnList = new List<EventData>();
                IOrderedEnumerable<EventData> sortQuery =
                    from EventData in tempList
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
                return tempList.Count;
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
            return tempList;
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