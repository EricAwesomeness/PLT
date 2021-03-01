using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PLT.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {

        private EditTreeViewModel editTreeVM = new EditTreeViewModel();
        public EditTreeViewModel EditTreeVM
        {
            get {return editTreeVM; }
            set { SetAndNotify(ref this.editTreeVM, value); }
        }

        private ViewTreeViewModel viewTreeVM = new ViewTreeViewModel();
        public ViewTreeViewModel ViewTreeVM
        {
            get { return viewTreeVM; }
            set { SetAndNotify(ref this.viewTreeVM, value); }
        }



        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (SetAndNotify(ref this._selectedItem, value))
                {
                    ActivateItemDetails(_selectedItem);
                    NotifyOfPropertyChange(nameof(EditTreeVM.CanAddLocation));
                    NotifyOfPropertyChange(nameof(EditTreeVM.CanAddDepartment));
                    NotifyOfPropertyChange(nameof(EditTreeVM.CanAddPrinter));
                }
            }
        }
        private void ActivateItemDetails(object item)
        {
            if (item is Location location)
            {
                EditTreeVM.ActiveLocation = location.LocationName;
                EditTreeVM.SelectedLocation = location;
                EditTreeVM.SelectedDepartment = null;
                EditTreeVM.ActiveWarrantyCode = null;
                EditTreeVM.ActiveModel = null;
                EditTreeVM.ActiveIP = null;

                ViewTreeVM.ActiveLocation = location.LocationName;
                ViewTreeVM.SelectedLocation = location;
                ViewTreeVM.SelectedDepartment = null;
                ViewTreeVM.ActiveWarrantyCode = null;
                ViewTreeVM.ActiveModel = null;
                ViewTreeVM.ActiveIP = null;


            }
            else if (item is Department department)
            {
                foreach (var Ltion in EditTreeVM.Locations.Where(x => x.Departments.Contains(department)))
                {
                    EditTreeVM.SelectedLocation = Ltion;
                }
                EditTreeVM.ActiveDepartment = department.DepartmentName;
                EditTreeVM.SelectedDepartment = department;
                EditTreeVM.ActiveWarrantyCode = null;
                EditTreeVM.ActiveModel = null;
                EditTreeVM.ActiveIP = null;

                ViewTreeVM.ActiveDepartment = department.DepartmentName;
                ViewTreeVM.SelectedDepartment = department;
                ViewTreeVM.ActiveWarrantyCode = null;
                ViewTreeVM.ActiveModel = null;
                ViewTreeVM.ActiveIP = null;

            }
            else if (item is Printer printer)
            {
                foreach (var Dment in EditTreeVM.Departments.Where(x => x.Printers.Contains(printer)))
                {
                    EditTreeVM.SelectedDepartment = Dment;
                    ViewTreeVM.SelectedDepartment = Dment;
                    foreach (var Ltion in EditTreeVM.Locations.Where(x => x.Departments.Contains(Dment)))
                    {
                        EditTreeVM.SelectedLocation = Ltion;
                        ViewTreeVM.SelectedLocation = Ltion;
                    }
                }
                EditTreeVM.ActiveWarrantyCode = printer.WarrantyCode;
                EditTreeVM.ActiveModel = printer.Model;
                EditTreeVM.ActiveIP = printer.Ip;
                EditTreeVM.SelectedPrinter = printer;

                ViewTreeVM.ActiveWarrantyCode = printer.WarrantyCode;
                ViewTreeVM.ActiveModel = printer.Model;
                ViewTreeVM.ActiveIP = printer.Ip;
                ViewTreeVM.SelectedPrinter = printer;

            }
        }


        public void ChangeView()
        {
            
            if (ActiveItem == ViewTreeVM) 
            {
                ActiveItem = EditTreeVM;
            }
            else if(ActiveItem == EditTreeVM) 
            {
                ActiveItem = ViewTreeVM;
            }
            
        }

        public ShellViewModel()
        {
            this.DisplayName = "Printer Location Tracker";
            ActiveItem = EditTreeVM;
        }
    }
}
