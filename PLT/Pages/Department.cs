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
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Location Location { get; set;  }
        public ObservableCollection<Printer> Printers { get; set; }


        public Department(string name, Location location)
        {
            Location = location;
            Name = name;
            Printers = new ObservableCollection<Printer>(){};
        }
        
        public Department(int id, string name, Location location) : this(name,location)
        {
            Id = id;
        }

    }
}
