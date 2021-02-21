using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class Department
    {

        private string departmentName = string.Empty;
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }


        private ObservableCollection<Printer> printers;
        public ObservableCollection<Printer> Printers
        {
            get { return printers; }
            set { printers = value; }
        }
        
        private Printer p1 = new Printer("Warrenty Code 1", "P1", "P1", "P1", "P1");
        public Printer P1
        {
            get { return p1; }
            set { }
        }


        public Department(string departmentName)
        {
            DepartmentName = departmentName;
            printers = new ObservableCollection<Printer>(){};
        }

    }
}
