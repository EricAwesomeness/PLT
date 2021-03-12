﻿using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class Printer
    {
        private string _ticketHistory = string.Empty;

        public Department Department { get; set; }
        public string WarrantyCode { get; set; }
        public string Model { get; set; }
        public string Ip { get; set; }
        public string TicketHistory 
        {
            get { return _ticketHistory; }
            set { _ticketHistory += value; }

        }


        public Printer(string warrantycode, string model, string ip, string ticketHistory, Department department)
        {
            Department = department;
            WarrantyCode = warrantycode;
            Model = model;
            Ip = ip;
            TicketHistory = _ticketHistory;
        }

        public override string ToString()
        {
            return $"{nameof(WarrantyCode)}: {WarrantyCode}, {nameof(Model)}: {Model}, {nameof(Ip)}: {Ip}, {nameof(TicketHistory)}: {TicketHistory}";
        }
    }
}
