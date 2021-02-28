using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PLT.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {


 

        public ShellViewModel(ViewTreeViewModel ViewTreeVM)
        {
            this.DisplayName = "Printer Location Tracker";
            ActiveItem = ViewTreeVM;
        }
    }
}
