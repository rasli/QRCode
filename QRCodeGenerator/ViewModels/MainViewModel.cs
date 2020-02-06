using QRGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.ViewModels
{
    public class MainViewModel :BaseViewModel
    {
        LoginViewModel _LoginVM;
        QRGeneratorViewModel _QRGeneratorVM;
        private BaseViewModel _currentVM;
        public RelayCommand<string> NavCommand { get; private set; }



        public MainViewModel(LoginViewModel loginViewModel,QRGeneratorViewModel qRGeneratorViewModel)
        {
            _LoginVM = loginViewModel;
            _QRGeneratorVM = qRGeneratorViewModel;

            NavCommand = new RelayCommand<string>(OnNavigate);
            OnNavigate("login");

            _LoginVM.OnLoginSuccess += Navigate;
        }

        private void Navigate()
        {
            OnNavigate("qrcode");
        }

        private void OnNavigate(string location)
        {
            switch (location)
            {
                case "login":
                    CurrentViewModel = _LoginVM;
                    break;
                case "qrcode":
                    CurrentViewModel = _QRGeneratorVM;
                    break;
            }
        }

        public BaseViewModel CurrentViewModel
        {
            get { return _currentVM; }
            set { SetProperty(ref _currentVM, value); }
        }
    }
}
