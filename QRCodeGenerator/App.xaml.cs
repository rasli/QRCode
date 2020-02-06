using QRGenerator.StartUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace QRGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ViewManager viewManager;
        bool mutexCreatedNew;
        Mutex mutex;

        public App()
        {
            mutex = new Mutex(true, "f09fb410-51c2-438b-8cd3-fccb6b0e9f23", out mutexCreatedNew);
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;
        }

        private void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var aEx = e.Exception.Flatten();
            foreach (var ex in aEx.InnerExceptions)
            {
                Console.WriteLine("Global Unhandled exception: " + ex.Message, ex);
            }
            e.SetObserved();
            RelunchApp();
        }

        private void RelunchApp()
        {
            if (viewManager != null)
            {
               
                viewManager.Dispose();
                viewManager = null;
            }
            LunchApp();
           
        }
        private void CloseApp()
        {
            TaskScheduler.UnobservedTaskException -= UnobservedTaskException;
            Close();
            Current.Shutdown(0);
        }

        private void Close()
        {
            try
            {
                if (viewManager != null)
                {
                    
                    viewManager.Dispose();
                    viewManager = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }
        private void LunchApp()
        {
            try
            {
                viewManager = ContainerManager.Resolve<ViewManager>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                CloseApp();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (!mutexCreatedNew)
            {
                MessageBox.Show("This application cannot continue because another instance is currently running.");
                Current.Shutdown(0);
                return;
            }
            LunchApp();
        }
    }
}
