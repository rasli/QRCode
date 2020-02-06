using QRGenerator.ViewModels;
using QRGenerator.Views;
using System;
using System.ComponentModel;
using System.Windows;

namespace QRGenerator.StartUp
{
    class ViewManager : IDisposable
    {
        
        MainViewModel _mainViewModel;
        private Window mainWindow;

        public ViewManager(  MainViewModel mainViewModel)
        {
            try
            {
                _mainViewModel = mainViewModel;
                mainWindow = new MainWindow(mainViewModel);
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message+ex.Source);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
    }
}
