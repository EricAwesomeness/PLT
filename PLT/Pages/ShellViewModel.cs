using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using Microsoft.Data.Sqlite;

namespace PLT.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {

        private EditTreeViewModel editTreeVM = new EditTreeViewModel();
        public EditTreeViewModel EditTreeVM
        {
            get { return editTreeVM; }
            set { SetAndNotify(ref this.editTreeVM, value); }
        }

        private ViewTreeViewModel viewTreeVM = new ViewTreeViewModel();
        public ViewTreeViewModel ViewTreeVM
        {
            get { return viewTreeVM; }
            set { SetAndNotify(ref this.viewTreeVM, value); }
        }

        public string _activeSearchItem;
        public string ActiveSearchItem
        {
            get { return _activeSearchItem; }
            set
            {
                if (SetAndNotify(ref this._activeSearchItem, value))
                {
                    Search();
                    NotifyOfPropertyChange(nameof(Search));
                };

            }
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
                    NotifyOfPropertyChange(nameof(Search));
                }
            }
        }
        public void ActivateItemDetails(object item)
        {
            if (item is Location location)
            {
                EditTreeVM.ActiveLocation = location.LocationName;
                EditTreeVM.SelectedLocation = location;
                EditTreeVM.SelectedDepartment = null;


                ViewTreeVM.ActiveLocation = location.LocationName;
                ViewTreeVM.SelectedLocation = location;
                ViewTreeVM.ActiveDepartment = null;
                ViewTreeVM.SelectedDepartment = null;
                ViewTreeVM.SelectedPrinter = null;
                ViewTreeVM.ActiveWarrantyCode = null;
                ViewTreeVM.ActiveModel = null;
                ViewTreeVM.ActiveIP = null;
                ViewTreeVM.ActiveTicketHistory = null;
            }
            else if (item is Department department)
            {
                foreach (var Ltion in EditTreeVM.Locations.Where(x => x.Departments.Contains(department)))
                {
                    EditTreeVM.SelectedLocation = Ltion;
                }
                EditTreeVM.ActiveDepartment = department.DepartmentName;
                EditTreeVM.SelectedDepartment = department;


                ViewTreeVM.ActiveDepartment = department.DepartmentName;
                ViewTreeVM.SelectedDepartment = department;
                ViewTreeVM.ActiveWarrantyCode = null;
                ViewTreeVM.SelectedPrinter = null;
                ViewTreeVM.ActiveModel = null;
                ViewTreeVM.ActiveIP = null;
                ViewTreeVM.ActiveTicketHistory = null;

            }
            else if (item is Printer printer)
            {
                foreach (var Dment in EditTreeVM.Departments.Where(x => x.Printers.Contains(printer)))
                {
                    EditTreeVM.SelectedDepartment = Dment;
                    ViewTreeVM.SelectedDepartment = Dment;
                    ViewTreeVM.ActiveDepartment = Dment.DepartmentName;

                    foreach (var Ltion in EditTreeVM.Locations.Where(x => x.Departments.Contains(Dment)))
                    {
                        EditTreeVM.SelectedLocation = Ltion;
                        ViewTreeVM.SelectedLocation = Ltion;
                        ViewTreeVM.ActiveLocation = Ltion.LocationName;
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
                ViewTreeVM.ActiveTicketHistory = printer.TicketHistory;
            }
        }

        public void ChangeView()
        {

            if (ActiveItem == ViewTreeVM)
            {
                ActiveItem = EditTreeVM;
            }
            else if (ActiveItem == EditTreeVM)
            {
                ActiveItem = ViewTreeVM;
            }

        }
        public void Search()
        {
            if (EditTreeVM.Locations.Any(g => g.LocationName == ActiveSearchItem))
            {
                SelectedItem = EditTreeVM.Locations.First(g => g.LocationName == ActiveSearchItem);
            }
            else if (EditTreeVM.Departments.Any(g => g.DepartmentName == ActiveSearchItem))
            {
                SelectedItem = EditTreeVM.Departments.First(g => g.DepartmentName == ActiveSearchItem);
            }
            else if (EditTreeVM.Printers.Any(g => g.WarrantyCode == ActiveSearchItem))
            {
                SelectedItem = EditTreeVM.Printers.First(g => g.WarrantyCode == ActiveSearchItem);
            }
        }








        public ShellViewModel()
        {
            this.DisplayName = "Printer Location Tracker";
            ActiveItem = ViewTreeVM;
        }
    }
}
