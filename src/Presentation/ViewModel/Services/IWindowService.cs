namespace BudgetFirst.ViewModel.Services
{
    using BudgetFirst.ViewModel.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWindowService
    {
        object showWindow<T>(T viewModel) where T : CloseableViewModel;
        void showMessage(string message);
    }
}
