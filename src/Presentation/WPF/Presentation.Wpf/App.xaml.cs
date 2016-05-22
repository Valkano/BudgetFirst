namespace BudgetFirst.Presentation.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Services;
    using ViewModel;
    using ViewModel.Desktop;
    using ViewModel.Services;
    using Views;

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
            this.RegisterServices();

            this.mainViewModel = ViewModelContainer.Default.Resolve<MainDesktopViewModel>();
            IWindowService windowService = ViewModelContainer.Default.Resolve<IWindowService>();

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

        /// <summary>
        /// A method to register application specific services with the ViewModel Container.
        /// </summary>
        private void RegisterServices()
        {
            WpfWindowService.RegisterWindow<MainDesktopViewModel, MainWindow>();

            ViewModelContainer.Default.Container.Register<IWindowService, WpfWindowService>(SimpleInjector.Lifestyle.Singleton);
        }
    }
}
