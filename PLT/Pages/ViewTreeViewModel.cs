using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLT.Pages
{
    public class ViewTreeViewModel : Screen
    {
        EditTreeViewModel EditTreeVM = new EditTreeViewModel();
        public ObservableCollection<Location> Locations
        {
            get { return EditTreeVM.Locations; }
            private set { }
        }
        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                SetAndNotify(ref this._selectedLocation, value);
            }
        }

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                SetAndNotify(ref this._selectedDepartment, value);
            }
        }

        public void EditView()
        {
            ///Logic Required to Deactivate this and Activate EditTreeViewModel
        }



        public ViewTreeViewModel()
        {
            SelectedLocation = Locations.LastOrDefault();
        }

    }
}
