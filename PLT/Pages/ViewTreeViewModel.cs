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
        private EditTreeViewModel editTreeVM = new EditTreeViewModel();
        public EditTreeViewModel EditTreeVM
        {
            get { return editTreeVM; }
            set { SetAndNotify(ref this.editTreeVM, value); }
        }


        #region Databinding input text boxs
        private string _activeMain;
        public string ActiveMain
        {
            get { return _activeMain; }
            set
            {
                SetAndNotify(ref this._activeMain, value);
            }
        }
        private string _activeWarrantyCode;
        public string ActiveWarrantyCode
        {
            get { return _activeWarrantyCode; }
            set
            {
                SetAndNotify(ref this._activeWarrantyCode, value);
            }
        }
        private string _activeModel;
        public string ActiveModel
        {
            get { return _activeModel; }
            set
            {
                SetAndNotify(ref this._activeModel, value);
            }
        }
        private string _activeDepartment;
        public string ActiveDepartment
        {
            get { return _activeDepartment; }
            set
            {
                SetAndNotify(ref this._activeDepartment, value);
            }
        }
        private string _activeLocation;
        public string ActiveLocation
        {
            get { return _activeLocation; }
            set
            {
                SetAndNotify(ref this._activeLocation, value);
            }
        }
        private string _activeIP;
        public string ActiveIP
        {
            get { return _activeIP; }
            set
            {
                SetAndNotify(ref this._activeIP, value);
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

        private Printer selectedPrinter;
        public Printer SelectedPrinter
        {
            get { return selectedPrinter; }
            set
            {
                SetAndNotify(ref this.selectedPrinter, value);
            }
        }


        public void EditView()
        {
            ///Logic Required to Deactivate this and Activate EditTreeViewModel
        }



        public ViewTreeViewModel()
        {
           
        }

    }
}
