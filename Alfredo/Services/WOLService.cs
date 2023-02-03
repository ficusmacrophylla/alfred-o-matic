using System.Net.Sockets;
using System.Net;
using Alfredo.Models;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace Alfredo.Services
{
    public class WOLService
    {
        private int MAXELEMENTS = 254;
        //TODO: List
        //Add time based signatures
        //Add Logging window
        //Add DiscoveryService to add a computer based on network discovery
        //Add Authentication
        //Add Persistence on 
        List<Computer> computers = new List<Computer>();

        public WOLService()
        {

#if DEBUG
            computers.Add(new Computer ("MOCK1", "192.168.1.1", "00-11-22-33-44-55"));
            computers.Add(new Computer ("MOCK2", "192.168.1.2", "66-77-88-99-AA-BB"));
#endif
        }

        //CRUD METHODS ON MODEL

        //Create AddComputer
        //Read  LoadComputers
        //Update 
        //Delete
        public void Add(Computer computer)
        {
            //var index = computers.FindIndex(x => x.Id == computer.Id);
            //if (index == -1) //no index taken
                computers.Add(computer);
            //else
            //    Computer newC = new Computer(RandomNumberGenerator.GetInt32(MAXELEMENTS))

        }
       public void LoadComputers()
        {
            //TODO Da implementare
        }

        public void UpdateComputer()
        {
            //TODO Implementare
        }

        public void Delete(int id) 
        {
            computers.Remove(GetById(id));
            //TODO Add confirmation message
        }

        //UTILITY METHODS
        //Wake
        //GetAll
        //GetById
        //UpdateStatus
        //UpdateStatusForAll

        public void Wake(int id)
        {
            Computer selectedComputer = this.GetById(id);
            WakeOnLan(selectedComputer.Id);
        }
        public List<Computer> GetAll() => computers;

        private Computer GetById(int id)
        {
            Computer? c = computers.FirstOrDefault(c => c.Id == id);
            if (c == null)
                throw new Exception("Computer Not Found"); //TODO SISTEMARE
            else
                return c;
        }


        public void UpdateStatus(int id)
        {
            Computer computer = GetById(id);
            Ping ping = new Ping();
            PingReply reply = ping.Send(computer.IP);
            if (reply.Status == IPStatus.Success)
                computer.Status = IPStatus.Success;
            else
                computer.Status = reply.Status;
        }
        public void UpdateStatusForAll(object sender, EventArgs e)
        {
            foreach (var computer in computers)
            {
                UpdateStatus(computer.Id);
                //add logger message "ping
            }
        }


        private void WakeOnLan(int id)
        {
            Computer computer = GetById(id);
            string[] macDigits = computer.MAC;
            int port = 40000;
            byte[] packet = new byte[102];

            for (int i = 0; i <= 5; i++)
                packet[i] = 0xff;

            int start = 6;
            for (int i = 0; i < 16; i++)
                for (int x = 0; x < 6; x++)
                {
                    packet[start + i * 6 + x] = (byte)Convert.ToInt32(macDigits[x], 16);
                }

            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, port);
            int res = client.Send(packet, packet.Length);
            //TODO add confirmation bytes sent
        }

        

        

    }
}
