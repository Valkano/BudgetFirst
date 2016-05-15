namespace BudgetFirst.Presentation.Wpf
{
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using ViewModel.Desktop;
    using Views;
    using ViewModel;
    using ViewModel.Services;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainDesktopViewModel mainViewModel;
        private Window mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            registerServices();

            this.mainViewModel = ViewModelContainer.Default.Resolve<MainDesktopViewModel>();
            IWindowService windowService = ViewModelContainer.Default.Resolve<IWindowService>();

            this.mainWindow = (Window)windowService.showWindow(mainViewModel);
            this.mainWindow.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void registerServices()
        {
            WpfWindowService.registerWindow<MainDesktopViewModel, MainWindow>();

            ViewModelContainer.Default.Container.Register<IWindowService, WpfWindowService>(SimpleInjector.Lifestyle.Singleton);
        }
    }
}
