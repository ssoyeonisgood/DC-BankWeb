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
            _database = new List<DataIntermed>();
            var generator = new DataGen();
            int size = generator.NumOAccts();
            Console.WriteLine("Size database: " + size);
            for (var i = 0; i < size; i++)
            {
                DataIntermed temp = generator.GetNextAccount();
                _database.Add(temp);

            }


            Console.WriteLine("Database size: " + _database.Count);
        }


        public async Task<List<DataIntermed>> GetAllAccount()
        {
            return await Task.Run(() => _database);
        }

        public async Task<DataIntermed?> GetAccountByIndex(int index)
        {
            return await Task.Run(() => _database[index]);
        }

        public async Task<int> GetNumRecords()
        {
            return await Task.Run(() => _database.Count);
        }

        public async Task<DataIntermed?> GetAccountByLastName(string searchData)
        {
            return await Task.Run(() =>
            {
                Console.WriteLine("searchname: " + searchData);

                for (int i = 0; i < _database.Count; i++)
                {
                    DataIntermed cur = _database[i];
                    if (cur.lastName == searchData)
                    {
                        Console.WriteLine($"Searched account: {cur.acctNo}, {cur.pin}, {cur.balance}, {cur.firstName}, {cur.lastName}");
                        return cur;
                    }
                }
                return null;

            });
        }
    }
}
