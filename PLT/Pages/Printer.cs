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
        private string location = string.Empty;
        private string ip = string.Empty;
        private string department = string.Empty;


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
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }



        public Printer(string warrantycode, string model, string location, string ip, string department)
        {

            WarrantyCode = warrantycode;
            Model = model;
            Location = location;
            Ip = ip;
            Department = department;
           
        }
    }
}
