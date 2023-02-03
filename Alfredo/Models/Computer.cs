using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Alfredo.Models
{
    public enum PingStatus { Running, NoReply, Unknown}
    public class Computer
    {
        public readonly int Id;

        [Required]
        public string Name { get; set; }
        public IPAddress? IP { get; set; }
        public string[]? MAC { get; set; }
        public IPStatus Status { get; set; }
        //TODO Aggiungere una firma temporale LastTimeCheck
        public Computer() 
        {            
        }

        public Computer(string name, string ip, string mac)
        {
            this.Name = name;
            SetIP(ip);
            SetMAC(mac);
            Status = IPStatus.Unknown;

            string[] charArray = ip.Split();
            Id = Int32.Parse(charArray.ToString());
        }

        public void SetIP(string ip) => IP = IPAddress.Parse(ip);

        public void SetMAC(string mac)
        {
            string[] macDigits = null;
            if (mac.Contains("-"))
                macDigits = mac.Split('-');
            else
                macDigits = mac.Split(':');

            if (macDigits.Length != 6)
                throw new ArgumentException("Incorrect MAC address format");

            MAC = macDigits;
        }

        
    }
}
