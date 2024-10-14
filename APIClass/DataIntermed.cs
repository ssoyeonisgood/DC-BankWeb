using System.Net.NetworkInformation;
using System.Drawing;

namespace APIClass
{
    public class DataIntermed
    {
        public uint acctNo { get; set; }
        public uint pin { get; set; }
        public decimal balance { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Bitmap icon { get; set; }

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