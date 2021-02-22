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
        
        private Printer p1 = new Printer("Warrenty Code 1", "P1", "P1", "P1", "P1");
        #endregion

        #region Databinding input text boxs
        private string _mainTextBoxInput;
        public string MainTextBoxInput
        {
            get { return _mainTextBoxInput; }
            set
            {
                SetAndNotify(ref this._mainTextBoxInput, value);
                NotifyOfPropertyChange(nameof(CanAddLocation));
                NotifyOfPropertyChange(nameof(CanAddDepartment));
            }
        }
        private string _warrantyCodeTextBoxInput;
        public string WarrantyCodeTextBoxInput
        {
            get {return _warrantyCodeTextBoxInput; }
            set
            {
                SetAndNotify(ref this._warrantyCodeTextBoxInput, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _modelTextBoxInput;
        public string ModelTextBoxInput
        {
            get { return _modelTextBoxInput; }
            set
            {
                SetAndNotify(ref this._modelTextBoxInput, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _departmentComboBox;
        public string DepartmentComboBox
        {
            get { return _departmentComboBox; }
            set
            {
                SetAndNotify(ref this._departmentComboBox, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _locationComboBoxInput;
        public string LocationComboBoxInput
        {
            get { return _locationComboBoxInput; }
            set
            {
                SetAndNotify(ref this._locationComboBoxInput, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        private string _ipTextBoxInput;
        public string IPTextBoxInput
        {
            get { return _ipTextBoxInput; }
            set
            {
                SetAndNotify(ref this._ipTextBoxInput, value);
                NotifyOfPropertyChange(nameof(CanAddPrinter));
            }
        }
        public string TicketHistoryTextBoxInput
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





        #region Can Execute Action Bools
        public bool CanAddLocation
        {
            get { return !string.IsNullOrEmpty(MainTextBoxInput) && !Locations.Any(x => x.LocationName == MainTextBoxInput); }
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
                    return !string.IsNullOrEmpty(MainTextBoxInput) && !SelectedLocation.Departments.Any(x => x.DepartmentName == MainTextBoxInput);
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
                    return !D1.Printers.Any(x => x.WarrantyCode == WarrantyCodeTextBoxInput) && !string.IsNullOrEmpty(WarrantyCodeTextBoxInput);
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

        #endregion


        #region Action Creations
        public void AddLocation()
        {
            Locations.Add(new Location(MainTextBoxInput));
            NotifyOfPropertyChange(nameof(CanAddLocation));
            NotifyOfPropertyChange(nameof(Locations));
        }
        public void AddDepartment()
        {
            SelectedLocation.Departments.Add(new Department(MainTextBoxInput));
            NotifyOfPropertyChange(nameof(CanAddDepartment));
            NotifyOfPropertyChange(nameof(Departments));
            SelectedDepartment = Departments.LastOrDefault();
        }
        public void AddPrinter()
        {
            if (SelectedDepartment != null)
            {
                SelectedDepartment.Printers.Add(new Printer(WarrantyCodeTextBoxInput, ModelTextBoxInput, LocationComboBoxInput, IPTextBoxInput, DepartmentComboBox));
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

        #endregion

        public ShellViewModel()
        {
            this.DisplayName = "Printer Location Tracker";
            Locations = new ObservableCollection<Location>(){L1};
            SelectedLocation = Locations.LastOrDefault();
        }
    }
}
