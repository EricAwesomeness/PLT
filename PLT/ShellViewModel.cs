using Stylet;
using System.Collections.ObjectModel;
using System.Linq;

namespace PLT.Pages
{
    public class ShellViewModel : Screen
    {

        #region Instantiating D1 Department; L1 Location; P1 Printer
        private Location l1 = new Location("L1");
        public Location L1
        {
            get { return l1; }
            set { }
        }

        private Department d1 = new Department("D1");
        public Department D1
        {
            get { return d1; }
            set { }
        }
        
        private Printer p1 = new Printer("P1", "P1", "P1", "P1", "P1");
        public Printer P1
        {
            get {return p1;}
            set { }
        }
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

        #region Action Creations
        public bool CanAddLocation
        {
            get { return !string.IsNullOrEmpty(MainTextBoxInput) && !Locations.Any(x => x.LocationName == MainTextBoxInput); }
        }
        public bool CanAddDepartment
        {
            get { return !string.IsNullOrEmpty(MainTextBoxInput) && !L1.Departments.Any(x => x.DepartmentName == MainTextBoxInput); }
        }
        public bool CanAddPrinter
        {
            get { return !D1.Printers.Any(x => x.WarrantyCode == WarrantyCodeTextBoxInput) && !string.IsNullOrEmpty(WarrantyCodeTextBoxInput); }
        }
        public void AddLocation()
        {
            Locations.Add(new Location(MainTextBoxInput));
            NotifyOfPropertyChange(nameof(CanAddLocation));
        }
        public void AddDepartment()
        {
            L1.Departments.Add(new Department(MainTextBoxInput));
            NotifyOfPropertyChange(nameof(CanAddDepartment));
        }
        public void AddPrinter()
        {
            D1.Printers.Add(new Printer(WarrantyCodeTextBoxInput, ModelTextBoxInput, LocationComboBoxInput, IPTextBoxInput, DepartmentComboBox));
            NotifyOfPropertyChange(nameof(CanAddPrinter));
        }

        #endregion

        #region Locations List
        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { locations = value; }
        }

        #endregion

        public ShellViewModel()
        {
            this.DisplayName = "Printer Location Tracker";

            Locations = new ObservableCollection<Location>(){L1};
        }
    }
}
