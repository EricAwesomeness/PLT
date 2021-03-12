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
        private static Location _instance;
        public static Location Instance => _instance ??= new Location("");

        private string locationName;
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
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
