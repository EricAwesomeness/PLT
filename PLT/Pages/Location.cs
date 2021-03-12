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
        public int Id { get; }
        public string Name { get; set; }
        
        public ObservableCollection<Department> Departments { get; }
        
        public Location(int id, string name, ObservableCollection<Department> departments = default) : this(name,departments)
        {
            Id = id;
        }
        
        public Location(string name, ObservableCollection<Department> departments = default)
        {
            Name = name;
            Departments = departments ?? new ObservableCollection<Department>();
        }
    }
}
