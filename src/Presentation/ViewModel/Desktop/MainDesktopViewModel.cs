using BudgetFirst.ViewModel.Services;
using BudgetFirst.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetFirst.ViewModel.Desktop
{
    public class MainDesktopViewModel : CloseableViewModel
    {
        private IWindowService windowService;

        public MainDesktopViewModel(IWindowService windowService)
        {
            this.windowService = windowService;
        }
    }
}
