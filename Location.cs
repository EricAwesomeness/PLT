using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class Location
    {


        private string locationName;
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }


        private Department d1 = new Department("Department 1");
        public Department D1
        {
            get { return d1; }
            set { }
        }


        private ObservableCollection<Department> departments;
        public ObservableCollection<Department> Departments
        {
            get { return departments; }
            set { departments = value; }
        }



        public Location(string locationName)
        {
            LocationName = locationName;
            departments = new ObservableCollection<Department>(){};
        }
    }
}
