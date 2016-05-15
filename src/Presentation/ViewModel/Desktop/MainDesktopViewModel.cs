using BudgetFirst.SharedInterfaces.Commands;
using BudgetFirst.SharedInterfaces.Messaging;
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
        private IMessageBus messageBus;
        private ICommandBus commandBus;

        public MainDesktopViewModel(IWindowService windowService)
        {
            this.windowService = windowService;

            BootstrapDomain();
        }

        private void BootstrapDomain()
        {
            //Bootstrap domain and access 
            this.messageBus = null;
            this.commandBus = null;
        }

        protected override void Close()
        {
            this.windowService.showMessage("test");
            base.Close();
        }
    }
}
