using QRGenerator.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Login : BaseModel, IDataErrorInfo
    {
        string username, password;
        public event Action<RequiedFields, Status> OnServiceStatusChange = delegate { };
        private Status _status;


        [Required(ErrorMessage = "Must not be empty")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Must not start with digits")]
        public string UserName
        {
            get { return username; }
            set
            {
                ValidateProperty(value, "UserName");
                SetProperty(ref username, value);
                OnServiceStatusChange(RequiedFields.UserName, Status.Enable);
            }
        }
        [Required(ErrorMessage = "Must not be empty")]
        [StringLength(50,MinimumLength=6,ErrorMessage = "Must be atleast 6 Characters")]
        public string Password
        {
            get { return password; }
            set
            {
                ValidateProperty(value, "Password");
                SetProperty(ref password, value);
                OnServiceStatusChange(RequiedFields.Password, Status.Enable);
            }
        }
        //public string UserName { get; set; }
        //public string Password { get; set; }

        public Status status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }


        public string Error
        {
            get { return null; }
        }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "UserName":
                        if (string.IsNullOrWhiteSpace(UserName))
                            result = "Must not be empty";
                        break;
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Must not be empty";
                        break;
                    
                }
                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChange("ErrorCollection");
                return result;
            }
        }
    }
}
