using QRGenerator.Shared;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QRGenerator.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        QRGeneratorViewModel _qrGen;
        public ICommand LoginCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public Action OnLoginSuccess = delegate { };
        private Login _logindata;
        private Status _LoginButtonStatus;

        public LoginViewModel(QRGeneratorViewModel qRGeneratorViewModel)
        {
            _qrGen = qRGeneratorViewModel;
            LoginCommand = new RelayCommand(ValidateLogin);
            CancelCommand = new RelayCommand(ExitApplication);
            LoginData = new Login();
            LoginButtonStatus = Status.Disable;

            LoginData.OnServiceStatusChange += OnStatusChange;
        }

        private void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ValidateLogin()
        {
            if (LoginData.Password != null && LoginData.UserName != null)
            {
                if (LoginData.Password == "master" && LoginData.UserName == "admin")
                {
                    OnLoginSuccess();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Username and Password Invalid. Please Try Again");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("User Name And Password Field Can not be Empty!!!");
            }
            
        }


        public Login LoginData
        {
            get { return _logindata; }
            set
            {
                SetProperty(ref _logindata, value);
            }
        }


        Status UserNameStatus, PasswordStatus;
        private void OnStatusChange(RequiedFields requiedFields, Status obj)
        {
            switch (requiedFields)
            {
                case RequiedFields.UserName:
                    UserNameStatus = obj;
                    break;
                case RequiedFields.Password:
                    PasswordStatus = obj;
                    break;

            }

            if (UserNameStatus == Status.Enable && PasswordStatus == Status.Enable)
                LoginButtonStatus = Status.Enable;
            else
                LoginButtonStatus = Status.Disable;
        }

        public Status LoginButtonStatus
        {
            get { return _LoginButtonStatus; }
            set { SetProperty(ref _LoginButtonStatus, value); }
        }
    }
}
