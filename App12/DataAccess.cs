using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SQLite;


namespace App12
{
    //
    //
    //  Database Access: Allows data to be stored after the application is closed.
    //
    //
    public class DataAccess
    {
         #region Key Class
        //
        //  Key: An int value that saves the decision for the consent form
        //
        class Key
        {
            //  ID column for the database that autoincrements
            [PrimaryKey, AutoIncrement, Column("ID")]
            public int ID { get; set; }
            //  0 is false, 1 is true.
            public int ConsentFormComfirmation { get; set; }
        }
        #endregion

		class Editing
		{
			//  ID column for the database that autoincrements
			[PrimaryKey, AutoIncrement, Column("ID")]
			public int ID { get; set; }
			//  0 is false, 1 is true.
			public int EditEnabled { get; set; }
		}

        #region Get ConsentForm Key
        //
        //  GetKey(): Retrieves the Consent Form key upon application launch to ensure 
        //            the consent form has been accepted.
        //
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
                //  Finds the table, creates it if it doesn't exist
				db.CreateTable<Key>();

                //  Creates a key to be saved
				Key obj;
				try
				{
                    //  If the key is retrieved, set it.
					obj = db.Get<Key>(1);
				}
				catch 
				{
                    //  If it failed, create a new key and set it to false.
					obj = new Key { ConsentFormComfirmation = 0 };
				}
				//db.Insert(obj);
                //  If the key is true, return that consent has been confirmed. Else, it hasn't been confirmed.
                if (obj.ConsentFormComfirmation == 1)
                    return true;
                else
                    return false;
			}
		}
        #endregion

		public static bool GetEdit()
		{
			//  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
			object locker = new object();
			lock (locker)
			{
				//  Sets the database path
				string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
				//  Connects to the database
				var db = new SQLiteConnection(dbPath);
				//  Finds the table, creates it if it doesn't exist
				db.CreateTable<Editing>();

				//  Creates a key to be saved
				Editing obj;
				try
				{
					//  If the key is retrieved, set it.
					obj = db.Get<Editing>(1);
				}
				catch
				{
					//  If it failed, create a new key and set it to true.
					obj = new Editing { EditEnabled = 1 };
				}
				db.Insert(obj);
				//Console.WriteLine(obj.EditEnabled);
				//  If the key is true, return that consent has been confirmed. Else, it hasn't been confirmed.
				if (obj.EditEnabled == 1)
					return true;
				else
					return false;
				
			}
		}

        #region Save ConsentForm Key
        //
        //  SaveKey(): Saves the ConsentForm Key to the database.
        //
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
                //  Finds the table, creates it if it doesn't 
                db.CreateTable<Key>();
				int temp;
				if (RootViewController.consentComfirmed == true)
					temp = 1;
				else
					temp = 0;
				db.Insert(new Key { ConsentFormComfirmation = temp} );
			}
		}
        #endregion

		public static void SaveEdit()
		{
			//  Creates a locker to prevent other objects from accessing the SQL database when this object is using it.
			object locker = new object();
			lock (locker)
			{
				//  Sets the database path
				string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
				//  Connects to the database
				var db = new SQLiteConnection(dbPath);
				//  Finds the table, creates it if it doesn't 
				db.CreateTable<Editing>();
				int temp;
				if (RootViewController.isEditingEnabled == true)
					temp = 1;
				else
					temp = 0;
				Console.WriteLine(temp);
				try
				{
					SQLiteCommand command = new SQLiteCommand(db);
					command.CommandText = "UPDATE Editing SET EditEnabled = '" + temp + "' Where ID = '1'";
					command.ExecuteNonQuery();

					Console.WriteLine("obj = " + db.Get<Editing>(1).EditEnabled);
				}
				catch
				{
					Console.WriteLine("Save failed");
					//  If it failed, create a new key and set it to true.
					db.Insert(new Editing { EditEnabled = temp});
				}

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

				obj.Title = obj.Title.Replace("'", "''");
				obj.Desc = obj.Desc.Replace("'", "''");
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
                                          "' Where _id = '" + obj.ID + "'";
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
			List<EventData> returnList = new List<EventData>();
			List<EventData> tempList = new List<EventData>();
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
                    int DayOftheWeek = (int)(Date.DayOfWeek)+1;
					//Console.WriteLine("Day of the Week: " + DayOftheWeek);
					//Console.WriteLine(EventData.Title + " - Sunday: " + EventData.Sunday);
                    switch (DayOftheWeek)
                    {
                        
                        case 1:
                            if (EventData.Sunday == 1)
                                returnList.Add(EventData);
							//Console.WriteLine("Count: " + EventData.Sunday);
                            break;
							
                        case 2:
                            if (EventData.Monday == 1)
                                returnList.Add(EventData);
                            break;
                        case 3:
							if (EventData.Tuesday == 1)
								returnList.Add(EventData);
                            break;
                        case 4:
                            if (EventData.Wednesday == 1)
                                returnList.Add(EventData);
                            break;
                        case 5:
                            if (EventData.Thursday == 1)
                                returnList.Add(EventData);
                            break;
                        case 6:
                            if (EventData.Friday == 1)
                                returnList.Add(EventData);
                            break;
                        case 7:
                            if (EventData.Saturday == 1)
                                returnList.Add(EventData);
                            break;
                            
                        default:
                            break;
                        
                    }
					if (EventData.Start.Date.DayOfWeek == Date.Date.DayOfWeek)
					{
						DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
						if (dt.DayOfYear <= EventData.Start.DayOfYear && EventData.Start.DayOfYear < dt.DayOfYear + 7)
						{
							returnList.Add(EventData);
						}
					}
                }

            }
			//Console.WriteLine("DataAccess Count: " + returnList.Count);
			return returnList.Count;
        }
		public static EventData GetObject(int ID)
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

				return db.Get<EventData>(ID);
			}
		}

        public static EventData GetNotification(int ID)
        {
            object locker = new object();
            lock (locker)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");

                var db = new SQLiteConnection(dbPath);

                db.CreateTable<EventData>();

                var obj = db.Get<EventData>(ID);
                return obj;
            }
        }
        public static DateTime NSDateToDateTime(Foundation.NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
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
					int DayOftheWeek = (int)(Date.DayOfWeek)+1;
					//Console.WriteLine("Get Events Day of Week: " + DayOftheWeek);
					switch (DayOftheWeek)
					{
						case 1:
							if (EventData.Sunday == 1)
							{
								returnList.Add(EventData);
							}
							break;
							
						case 2:
							if (EventData.Monday == 1)
							{
								returnList.Add(EventData);

							}
							break;
						case 3:
							if (EventData.Tuesday == 1)
							{
								returnList.Add(EventData);
							}
							break;
						case 4:
							if (EventData.Wednesday == 1)
							{
								returnList.Add(EventData);
							}
							break;
						case 5:
							if (EventData.Thursday == 1)
							{
								returnList.Add(EventData);
							}
							break;
						case 6:
							if (EventData.Friday == 1)
							{
								returnList.Add(EventData);
							}
							break;
						case 7:
							if (EventData.Saturday == 1)
							{
								returnList.Add(EventData);
							}
							break;
							
						default:
							break;

					}
					Console.WriteLine("EventData.Start: " + EventData.Start.Date + ". Date.Date: " + Date.Date);
					if (EventData.Start.Date.DayOfWeek == Date.Date.DayOfWeek)
					{ 
						DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
						if (dt.DayOfYear <= EventData.Start.DayOfYear && EventData.Start.DayOfYear < dt.DayOfYear + 7)
						{
							Console.WriteLine("True");
							returnList.Add(EventData);
						}
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