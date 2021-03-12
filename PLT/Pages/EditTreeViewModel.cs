using Microsoft.Data.Sqlite;
using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class EditTreeViewModel : Screen
    {
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
            get { return _activeWarrantyCode; }
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

        private Printer _selectedPrinter;
       
        public Printer SelectedPrinter 
        {
            get { return _selectedPrinter; }
            set
            {
                SetAndNotify(ref this._selectedPrinter, value);
                NotifyOfPropertyChange(nameof(CanAddLocation));
                NotifyOfPropertyChange(nameof(CanAddDepartment));
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        
        public ObservableCollection<Location> Locations { get; set; }

        public IEnumerable<Department> Departments => Locations.SelectMany(location => location.Departments);
        public IEnumerable<Printer> Printers => Departments.SelectMany(department => department.Printers);



        #region Action Methods
        public bool CanAddLocation
        {
            get { return !string.IsNullOrEmpty(ActiveMain) && !Locations.Any(x => x.Name == ActiveMain); }
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
                    return !string.IsNullOrEmpty(ActiveMain) && !SelectedLocation.Departments.Any(x => x.Name == ActiveMain);
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
                    return !SelectedDepartment.Printers.Any(x => x.WarrantyCode == ActiveWarrantyCode) && !string.IsNullOrEmpty(ActiveWarrantyCode);
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

        public void AddLocation()
        {
            Locations.Add(new Location(ActiveMain));
            NotifyOfPropertyChange(nameof(CanAddLocation));
            NotifyOfPropertyChange(nameof(Locations));

        }
        public void AddDepartment()
        {
            SelectedLocation.Departments.Add(new Department(ActiveMain, SelectedLocation));
            NotifyOfPropertyChange(nameof(CanAddDepartment));
            NotifyOfPropertyChange(nameof(Departments));
            SelectedDepartment = Departments.LastOrDefault();
        }
        public void AddPrinter()
        {
            if (SelectedDepartment != null)
            {
                SelectedDepartment.Printers.Add(new Printer(ActiveWarrantyCode, ActiveModel, ActiveIP, ActiveTicketHistory, SelectedDepartment));
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
            SelectedDepartment.Printers.Remove((Printer)SelectedPrinter);
            SelectedDepartment = SelectedLocation.Departments.LastOrDefault();
        }
        public void ChangeView() 
        {
           
        }
        #endregion


        public void SaveDB()
        {
            var db = Database.Instance;

            foreach (var loc in Locations) 
            {
                string locationName = loc.Name;
                db.AddLocation(locationName);
            }
            foreach (var dep in Departments)
            {
                string departmentName = dep.Name;
                db.AddDepartment(departmentName);
            }
            foreach (var printer in Printers)
            {
                var department = Departments.First(x => x.Printers.Contains(printer));
                var location = Locations.First(x => x.Departments.Contains(department));
                
                string priDepartmentName = department.Name;
                string priLocationName = location.Name;

                db.AddPrinter(printer);
            }
        }
        public void LoadingData() 
        {
            var db = Database.Instance;

            var locs = db.LoadLocations();
            foreach (var loc in locs)
            {
                Locations.Add(loc);
                foreach (var dep in loc.Departments)
                {
                    var printers = db.GetPrintersAtDepartment(dep);
                    foreach (var printer in printers)
                    {
                        dep.Printers.Add(printer);
                    }
                }
            }
        }



        public EditTreeViewModel() 
        {
            Locations = new ObservableCollection<Location>(){};
            SelectedLocation = Locations.LastOrDefault();
            LoadingData();
        }
    }
}
