using APIClasses;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DataAPI.Models.Database
{
    public class DataGen
    {
        private readonly Random _rand = new Random();

        private readonly string[] _fNameList = {
            "Robert", "Jack", "John", "Jane", "Michael", "William", "David", "Stefan", "Nelson", "Richard", "Charlie", "Mary", "Linda", "Susan", "Jessica", "Kathleen", "Ann"
        };

        private readonly string[] _lNameList = {
            "Smith", "Johnson", "Williams", "Jones", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "Citizen", "Doe"
        };


        private string GetFirstName() => _fNameList[_rand.Next(_fNameList.Length)];

        private string GetLastName() => _lNameList[_rand.Next(_lNameList.Length)];

        private uint GetPIN() => (uint)_rand.Next(9999);

        private uint GetAcctNo() => (uint)_rand.Next(100000000, 999999999);

        private int GetBalance() => _rand.Next(-10000, 10000);


        public DataIntermed GetNextAccount()
        {
            DataIntermed account = new DataIntermed();
            account.pin = GetPIN();
            account.acctNo = GetAcctNo();
            account.firstName = GetFirstName();
            account.lastName = GetLastName();
            account.balance = GetBalance();
            return account;
        }

        // Generate to Create at least 100.000 records
        public int NumOAccts() => _rand.Next(100000, 999999);
    }
}