using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Media.Media3D;
using Accounting.Model.Data.Abstract;
using Accounting.ViewModel;
using Accounting.Model.Data.EntityFramework;
using Autofac;

namespace Accounting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentUICulture;
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<AccountingDBContext>().AsSelf();
            builder.RegisterType<EFAccountingEntity>().As<IRepository<AccountingEntity>>();
            var container = builder.Build();
            var mainWindowViewModel = container.Resolve<MainWindowViewModel>();
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
            mainWindow.Show();
        }
    }
}
