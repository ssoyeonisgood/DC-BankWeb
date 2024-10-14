using System.Net.NetworkInformation;
using System.Drawing;

namespace APIClasses
{
    public class DataIntermed
    {
        public uint acctNo;
        public uint pin;
        public decimal balance;
        public string firstName;
        public string lastName;
        public Bitmap icon;

        public DataIntermed()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
            icon = null;
        }

        public override string ToString()
        {
            return $"Account: {acctNo}, Name: {firstName} {lastName}, Balance: ${balance:N2}, PIN: {pin:D4}";
        }
    }


}