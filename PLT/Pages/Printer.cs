using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class Printer
    {
        private string _tickethistory = string.Empty;


        public string WarrantyCode { get; set; }

        public string Model { get; set; }

        public string Ip { get; set; }

        public string TicketHistory 
        {
            get { return _tickethistory; }
            set { _tickethistory += value; }

        }


        public Printer(string warrantycode, string model, string ip, string tickettickethistory)
        {

            WarrantyCode = warrantycode;
            Model = model;
            Ip = ip;
            TicketHistory = _tickethistory;
        }

        public override string ToString()
        {
            return $"{nameof(WarrantyCode)}: {WarrantyCode}, {nameof(Model)}: {Model}, {nameof(Ip)}: {Ip}, {nameof(TicketHistory)}: {TicketHistory}";
        }
    }
}
