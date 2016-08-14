namespace BudgetFirst.Presentation.Windows
{
    using System;
    using System.Windows;

    using BudgetFirst.Application.ViewModels;
    using BudgetFirst.Application.ViewModels.ReplaceMe;
    using BudgetFirst.Common.Infrastructure.Wrappers;
    using BudgetFirst.Presentation.Windows.PlatformSpecific;
    using BudgetFirst.Presentation.Windows.Services;
    
    using MainWindow = BudgetFirst.Presentation.Windows.Views.MainWindow;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The main ViewModel for the application.
        /// </summary>
        private MainDesktopViewModel mainViewModel;

        /// <summary>
        /// The main window of the application.
        /// </summary>
        private Window mainWindow;

        /// <summary>
        /// A method that is called when the application starts.
        /// </summary>
        /// <param name="e">The <see cref="StartupEventArgs"/>.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup service locator, ioc etc.
            WindowsViewModelLocator.EnsureInitialised();

            // Register window service (to be replaced by navigation and dialog services)
            SimpleIocWrapper.Default.Register<IWindowService, WpfWindowService>();
            WpfWindowService.RegisterWindow<MainDesktopViewModel, MainWindow>();
            
            this.mainViewModel = ServiceLocatorWrapper.Current.GetInstance<MainDesktopViewModel>();
            var windowService = ServiceLocatorWrapper.Current.GetInstance<IWindowService>();

            this.mainWindow = (Window)windowService.ShowWindow(this.mainViewModel);
            this.mainWindow.Closed += this.MainWindow_Closed;
        }

        /// <summary>
        /// An event handler for the close event of the Main Window.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="e">The event's arguments</param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
