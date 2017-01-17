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
                db.Insert(obj);
            }

        }

        public static List<EventData> GetEvents()
        {
            List<EventData> tempList = new List<EventData>();
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
            }
            return tempList;

            
        }

    }
}