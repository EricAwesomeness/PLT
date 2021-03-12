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

        private static EditTreeViewModel _instance;
        public static EditTreeViewModel Instance => _instance ??= new EditTreeViewModel();




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

        private Printer p1 = new Printer("Warrenty Code 1", "P1", "P1", "P1");
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
            SelectedLocation.Departments.Add(new Department(ActiveMain));
            NotifyOfPropertyChange(nameof(CanAddDepartment));
            NotifyOfPropertyChange(nameof(Departments));
            SelectedDepartment = Departments.LastOrDefault();
        }
        public void AddPrinter()
        {
            if (SelectedDepartment != null)
            {
                SelectedDepartment.Printers.Add(new Printer(ActiveWarrantyCode, ActiveModel, ActiveIP, ActiveTicketHistory));
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
                string locationName = loc.LocationName;
                db.AddLocation(locationName);
            }
            foreach (var dep in Departments)
            {
                string departmentName = dep.DepartmentName;
                db.AddDepartment(departmentName);
            }
            foreach (var printer in Printers)
            {
                var department = Departments.First(x => x.Printers.Contains(printer));
                var location = Locations.First(x => x.Departments.Contains(department));
                
                string priDepartmentName = department.DepartmentName;
                string priLocationName = location.LocationName;
                string priWarrantyCode = printer.WarrantyCode;
                string priModel = printer.Model;
                string priIp = printer.Ip;
                string priTicketHistory = printer.TicketHistory;

                db.AddPrinter(priWarrantyCode, priModel, priIp, priTicketHistory);
            }
        }
        public void LoadingData() 
        {
            var db = Database.Instance;

            db.LoadLocations().ForEach(x => Locations.Add(new Location(x)));

            Locations.ToList().ForEach(x => db.GetDepartmentsAtLocation(x.LocationName).ForEach(y => x.Departments.Add(new Department(y))));

            Locations.ToList().ForEach(x => x.Departments.ToList().ForEach(y => db.GetPrintersAtDepartment(y.DepartmentName).ForEach(z => y.Printers.Add(new Printer(z.GetValue(0).ToString(), z.GetValue(1).ToString(), z.GetValue(2).ToString(), z.GetValue(3).ToString())))));
        }



        public EditTreeViewModel() 
        {
            Locations = new ObservableCollection<Location>(){};
            SelectedLocation = Locations.LastOrDefault();
            LoadingData();
        }
    }
}
