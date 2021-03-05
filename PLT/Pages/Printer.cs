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

        private string warrantycode = string.Empty;
        private string model = string.Empty;
        private string ip = string.Empty;
        private string tickethistory = string.Empty;


        public string WarrantyCode
        {
            get { return warrantycode; }
            set { warrantycode = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string TicketHistory 
        {
            get { return tickethistory; }
            set { tickethistory +="\n" + DateTime.Now + ": " + value; }

        }


        public Printer(string warrantycode, string model, string ip)
        {

            WarrantyCode = warrantycode;
            Model = model;
            Ip = ip;
 
        }
    }
}
