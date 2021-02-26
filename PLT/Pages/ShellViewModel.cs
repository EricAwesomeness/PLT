using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PLT.Pages
{
    public class ShellViewModel : Screen
    {

        #region Instantiating D1 Department; L1 Location; P1 Printer
        private Location l1 = new Location("Location 1");
        public Location L1
        {
            get { return l1; }
            set { }
        }

        private Department d1 = new Department("Department 1");
        public Department D1
        {
            get { return d1; }
            set { }
        }

        private Printer p1 = new Printer("Warrenty Code 1", "P1", "P1");
        #endregion

        #region Databinding input text boxs
        private string _activeMain;
        public string ActiveMain
        {
            get { return _activeMain; }
            set
            {
                SetAndNotify(ref this._activeMain, value);
                NotifyOfPropertyChange(nameof(CanAddLocation));
                NotifyOfPropertyChange(nameof(CanAddDepartment));
            }
        }
        private string _activeWarrantyCode;
        public string ActiveWarrantyCode
        {
            get {return _activeWarrantyCode; }
            set
            {
                SetAndNotify(ref this._activeWarrantyCode, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _activeModel;
        public string ActiveModel
        {
            get { return _activeModel; }
            set
            {
                SetAndNotify(ref this._activeModel, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _activeDepartment;
        public string ActiveDepartment
        {
            get { return _activeDepartment; }
            set
            {
                SetAndNotify(ref this._activeDepartment, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _activeLocation;
        public string ActiveLocation
        {
            get { return _activeLocation; }
            set
            {
                SetAndNotify(ref this._activeLocation, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _activeIP;
        public string ActiveIP
        {
            get { return _activeIP; }
            set
            {
                SetAndNotify(ref this._activeIP, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        public string ActiveTicketHistory
        {
            get;
            set;
        }
        #endregion


        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                SetAndNotify(ref this._selectedLocation, value);
                NotifyOfPropertyChange(nameof(CanAddLocation));
                NotifyOfPropertyChange(nameof(CanAddDepartment));
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }


        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { locations = value; }
        }



        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                SetAndNotify(ref this._selectedDepartment, value);
                NotifyOfPropertyChange(nameof(CanAddLocation));
                NotifyOfPropertyChange(nameof(CanAddDepartment));
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        public IEnumerable<Department> Departments => Locations.SelectMany(Location => Location.Departments);





        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (SetAndNotify(ref this._selectedItem, value)) 
                {
                    ActivateItemDetails(_selectedItem);
                    NotifyOfPropertyChange(nameof(CanAddLocation));
                    NotifyOfPropertyChange(nameof(CanAddDepartment));
                    NotifyOfPropertyChange(nameof(CanAddPrinter));
                }
            }
        }
        private void ActivateItemDetails(object item)
        {
            if (item is Location location)
            {
                ActiveLocation = location.LocationName;
                SelectedLocation = location;
                SelectedDepartment = null;
                ActiveWarrantyCode = null;
                ActiveModel = null;
                ActiveIP = null;
                
            }
            else if (item is Department department)
            {
                foreach (var Ltion in Locations.Where(x => x.Departments.Contains(department))) 
                {
                    SelectedLocation = Ltion;
                } 
                ActiveDepartment = department.DepartmentName;
                SelectedDepartment = department;
                ActiveWarrantyCode = null;
                ActiveModel = null;
                ActiveIP = null;

                
            }
            else if (item is Printer printer) 
            {
                // ActiveLocation = printer.Location;
                foreach (var Dment in Departments.Where(x => x.Printers.Contains(printer)))
                {
                    SelectedDepartment = Dment;
                    foreach (var Ltion in Locations.Where(x => x.Departments.Contains(Dment)))
                    {
                        SelectedLocation = Ltion;
                    }
                }
                ActiveWarrantyCode = printer.WarrantyCode;
                ActiveModel = printer.Model;
                ActiveIP = printer.Ip;
            }
        }



        #region Can Execute Action Bools
        public bool CanAddLocation
        {
            get { return !string.IsNullOrEmpty(ActiveMain) && !Locations.Any(x => x.LocationName == ActiveMain); }
        }
        public bool CanAddDepartment
        {
            get
            {
                if (SelectedLocation == null)
                {
                    return false;
                }
                else
                {
                    return !string.IsNullOrEmpty(ActiveMain) && !SelectedLocation.Departments.Any(x => x.DepartmentName == ActiveMain);
                }
            }
        }
        public bool CanAddPrinter
        {
            get 
            {
                if (SelectedDepartment == null)
                {
                    return false;
                }
                else
                {
                    return !D1.Printers.Any(x => x.WarrantyCode == ActiveWarrantyCode) && !string.IsNullOrEmpty(ActiveWarrantyCode);
                }
            }
        }
        public bool CanDeleteLocation
        {
            get { return true; } ///Changing later
        }
        public bool CanDeleteDepartment
        {
            get { return true; } ///Changing later
        }
        public bool CanDeletePrinter
        {
            get { return true; } ///Changing later
        }

        #endregion


        #region Action Creations
        public void AddLocation()
        {
            Locations.Add(new Location(ActiveMain));
            NotifyOfPropertyChange(nameof(CanAddLocation));
            NotifyOfPropertyChange(nameof(Locations));
        }
        public void AddDepartment()
        {
            SelectedLocation.Departments.Add(new Department(ActiveMain));
            NotifyOfPropertyChange(nameof(CanAddDepartment));
            NotifyOfPropertyChange(nameof(Departments));
            SelectedDepartment = Departments.LastOrDefault();
        }
        public void AddPrinter()
        {
            if (SelectedDepartment != null)
            {
                SelectedDepartment.Printers.Add(new Printer(ActiveWarrantyCode, ActiveModel, ActiveIP));
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        public void DeleteLocation() 
        {
            Locations.Remove(SelectedLocation);
            SelectedLocation = Locations.LastOrDefault();
        }
        public void DeleteDepartment()
        {
            SelectedLocation.Departments.Remove(SelectedDepartment);
            SelectedDepartment = SelectedLocation.Departments.LastOrDefault();
        }
        public void DeletePrinter()
        {
            SelectedDepartment.Printers.Remove((Printer)SelectedItem);
            SelectedDepartment = SelectedLocation.Departments.LastOrDefault();
        }

        #endregion

        public ShellViewModel()
        {
            this.DisplayName = "Printer Location Tracker";
            Locations = new ObservableCollection<Location>(){L1};
            SelectedLocation = Locations.LastOrDefault();
            
        }
    }
}
