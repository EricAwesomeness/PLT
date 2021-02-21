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
            get
            {
                if (string.IsNullOrEmpty(departmentName))
                {
                    return "N/A";
                }
                return departmentName;
            }
            set
            {
                departmentName = value;
                
            }
        }


        private ObservableCollection<Printer> printers;
        public ObservableCollection<Printer> Printers
        {
            get
            {
                return printers;
            }
            set
            {
                printers = value;
            }
        }



        public Department(string departmentName)
        {
            DepartmentName = departmentName;
            printers = new ObservableCollection<Printer>(){};
        }

    }
}
