// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================

namespace BudgetFirst.Presentation.Wpf.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using BudgetFirst.ViewModel.Services;
    using BudgetFirst.ViewModel.Shared;

    /// <summary>
    /// The WPF implementation of the <see cref="IWindowService"/>.
    /// </summary>
    public class WpfWindowService : IWindowService
    {
        /// <summary>
        /// A dictionary to hold window registrations for each ViewModel.
        /// </summary>
        private static Dictionary<Type, Type> registeredWindows = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers a type of ViewModel to be shown in a Type of Window.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the ViewModel</typeparam>
        /// <typeparam name="TWindow">The type of the Window.</typeparam>
        public static void RegisterWindow<TViewModel, TWindow>() where TViewModel : ClosableViewModel where TWindow : Window
        {
            if (registeredWindows.ContainsKey(typeof(TViewModel)))
            {
                throw new Exception("This ViewModel has already been registered");
            }

            registeredWindows.Add(typeof(TViewModel), typeof(TWindow));
        }

        /// <summary>
        /// Shows a message box with a message.
        /// </summary>
        /// <param name="message">The message</param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Shows a Window based on a ViewModel.
        /// </summary>
        /// <typeparam name="T">The type of the ViewModel, must subclass <see cref="ClosableViewModel"/></typeparam>
        /// <param name="viewModel">The viewModel to show a window for.</param>
        /// <returns>The <see cref="Window"/> that is created to show the ViewModel.</returns>
        public object ShowWindow<T>(T viewModel) where T : ClosableViewModel
        {
            if (!registeredWindows.ContainsKey(typeof(T)))
            {
                throw new Exception("This ViewModel has NOT been registered");
            }

            Type windowType = registeredWindows[typeof(T)];
            Window window = (Window)Activator.CreateInstance(windowType);

            window.DataContext = viewModel;

            // If the _windowClosing flag is not set then call the window close method
            bool viewModelClose = false;
            bool windowClose = false;

            viewModel.RequestClose += (sender, e) =>
            {
                viewModelClose = true;

                if (!windowClose)
                {
                    window.Close();
                }
            };

            window.Closing += (sender, e) =>
            {
                windowClose = true;
                
                // Call RequestClose on the ViewModel and block the thread until it runs it closing routines
                if (!viewModelClose)
                {
                    viewModel.CloseCommand.Execute(null);
                }
            };

            window.Show();

            return window;
        }
    }
}
