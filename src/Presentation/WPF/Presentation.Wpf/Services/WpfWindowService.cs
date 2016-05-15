using BudgetFirst.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetFirst.ViewModel.Shared;
using System.Windows;
using System.Windows.Controls;

namespace BudgetFirst.Presentation.Wpf.Services
{
    public class WpfWindowService : IWindowService
    {
        private static Dictionary<Type, Type> registeredWindows;

        public void showMessage(string message)
        {
            MessageBox.Show(message);
        }

        public object showWindow<T>(T viewModel) where T : CloseableViewModel
        {
            if (!registeredWindows.ContainsKey(typeof(T)))
                throw new Exception("This ViewModel has NOT been registered");

            Type windowType = registeredWindows[typeof(T)];
            Window window = (Window)Activator.CreateInstance(windowType);

            window.DataContext = viewModel;

            //If the _windowClosing flag is not set then call the window close method
            bool viewModelClose = false;
            bool windowClose = false;

            viewModel.RequestClose += (sender, e) =>
            {
                if (!windowClose)
                    window.Close();
            };


            window.Closing += (sender, e) =>
            {
                //Call RequestClose on the ViewModel and block the thread until it runs it closing routines
                if (!viewModelClose)
                    viewModel.RaiseRequestClose();
            };

            window.Show();

            return window;
        }

        public static void registerWindow<TViewModel, TWindow>() where TViewModel : CloseableViewModel where TWindow : Window
        {
            if (registeredWindows.ContainsKey(typeof(TViewModel)))
                throw new Exception("This ViewModel has already been registered");

            registeredWindows.Add(typeof(TViewModel), typeof(TWindow));
        }
    }
}
