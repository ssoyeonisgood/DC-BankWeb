using APIClasses;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DataAPI.Models.Database
{
    public class Database
    {
        private readonly List<DataIntermed> _database;

        public static Database Instance { get; } = new Database();
        static Database() { }

        private Database()
        {
            // Create the fake values for our fake database
            _database = new List<DataIntermed>();
            var generator = new DataGen();
            int size = generator.NumOAccts();
            Console.WriteLine("Size database: " + size);
            for (var i = 0; i < 10; i++)
            {
                DataIntermed temp = generator.GetNextAccount();
                _database.Add(temp);

            }
            for (var i = 0; i < _database.Count; i++)
            {
                Console.WriteLine(_database[i].ToString());
            }

            Console.WriteLine("Database size: " + _database.Count);
        }
        //public DataIntermed SearchByName(string name)
        //{
        //    for (int i = 0; i < _database.Count; i++)
        //    {
        //        DataIntermed cur = _database[i];
        //        if (cur.lastName == name)
        //        {
        //            return cur;
        //        }
        //    }
        //    return new DataIntermed();
        //}

        public uint GetAcctNoByIndex(int index)
        {
            return _database[index].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return _database[index].pin;
        }

        public string GetFirstNameByIndex(int index)
        {
            return _database[index].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return _database[index].lastName;
        }

        public int GetBalanceByIndex(int index)
        {
            return _database[index].balance;
        }

        public List<DataIntermed> GetAllAccount()  
        {
            return _database;
        } 

        public DataIntermed GetAccountByIndex(int index)
        { 
            return _database[index]; 
        }

        public int GetNumRecords()
        {
            return _database.Count;
        }
    }
}
