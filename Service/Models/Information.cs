using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using QRGenerator.Services.Models;

namespace Service.Models
{
    public class Information : BaseModel, IDataErrorInfo
    {
        public event Action<RequiedFields,Status> OnServiceStatusChange = delegate { };
        private BitmapImage qrcode;
        private string company, contractnumber, _numberofContractor;
        private DateTime dateTime;
        private int type, numberofcontractor;
        private Status _status;

        //[Required(ErrorMessage ="Must not be empty")]
        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage ="Must not start with digits or Contain Special Character")]
        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Must not start with digits or Contain Special Character")]

        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Must be atleast 3 Characters")]
        public string Company
        {
            get { return company; }
            set
            {
                //if (int.TryParse(value, out numberofcontractor))
                //{
                //    if (Int32.Parse(value) >= 0)
                //   {
                //        value = "";
                //    }
                //}
                //if(string.IsNullOrWhiteSpace(value))

                ValidateProperty(value, "Company");
                SetProperty(ref company, value);
               
            }
        }
        //public string Company { get; set; }
       // [Required(ErrorMessage = "Must not be empty")]
       // [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be atleast 3 Characters")]
        public string ContractNumber
        {
            get { return contractnumber; }
            set
            {
                ValidateProperty(value, "ContractNumber");
                SetProperty(ref contractnumber, value);
            }
        }
        public string NumberOfContractors
        {
            get { return _numberofContractor; }
            set
            {
                if (int.TryParse(value, out numberofcontractor))
                {
                    if (Int32.Parse(value) > 50)
                    {
                        value = "";
                    }
                }
                else
                {
                    value = "";
                }
                SetProperty(ref _numberofContractor, value);
            }
        }
        public DateTime StartDateTime
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }

        }
        public DateTime ExpiryDateTime { get; set; }
        //public BitmapImage QrCode { get; set; }
        public BitmapImage QrCode
        {
            get { return qrcode; }
            set { SetProperty(ref qrcode, value); }
        }

        public int Access_Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

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
                string r2 = @"^[0-9a-zA-Z][a-zA-Z0-9@#$%&*+\-_(),+':;?.,![\]\s\\/]*$";
                string r1 = @"^[a-zA-Z][a-zA-Z0-9@#$%&*+\-_(),+':;?.,![\]\s\\/]+$";
                string result = null;
                switch (columnName)
                {
                    case "Company":                        
                        Match res=Regex.Match("a",r1);
                        Match res2 = Regex.Match("a", r2);
                        if (!string.IsNullOrWhiteSpace(Company))
                        {
                            res = Regex.Match(Company, r1);
                            res2 = Regex.Match(Company, r2);
                        }
                        if (string.IsNullOrWhiteSpace(Company))
                        {
                            
                            result = "Must not be empty";

                            OnServiceStatusChange(RequiedFields.Company,Status.Disable);
                        }
                        else if (!res2.Success)
                        {
                            result = "Must not start with Special Character";
                            status = Status.Disable;
                            OnServiceStatusChange(RequiedFields.Company, Status.Disable);
                        }
                        else if (!res.Success)
                        {
                           if (Company.Length < 4)
                                result = "At least 3 Characters";
                            else
                                result = "Must not start with digits";
                            status = Status.Disable;
                            OnServiceStatusChange(RequiedFields.Company, Status.Disable);
                        }
                        else
                        {
                            if (Company.Length >= 3)
                                OnServiceStatusChange(RequiedFields.Company, Status.Enable);
                            else
                            {
                                result = "At least 3 Characters";
                                OnServiceStatusChange(RequiedFields.Company, Status.Disable);
                            }
                        }
                        break;
                    case "ContractNumber":
                        Match res3 = Regex.Match("a", r1);
                        Match res4 = Regex.Match("a", r2);
                        if (!string.IsNullOrWhiteSpace(ContractNumber))
                        {
                            res3 = Regex.Match(ContractNumber, r1);
                            res4 = Regex.Match(ContractNumber, r2);
                        }
                        if (string.IsNullOrWhiteSpace(ContractNumber))
                        {
                            
                            result = "Must not be empty";

                            OnServiceStatusChange(RequiedFields.ContractNumber, Status.Disable);
                        }
                        else if (!res4.Success)
                        {
                            result = "Must not start with Special Character";
                            status = Status.Disable;
                            OnServiceStatusChange(RequiedFields.ContractNumber, Status.Disable);
                        }
                        else if (!res3.Success)
                        {
                            if (ContractNumber.Length < 4)
                                result = "At least 3 Characters";
                            else
                                result = "Must not start with digits";
                            status = Status.Disable;
                            OnServiceStatusChange(RequiedFields.ContractNumber, Status.Disable);
                        }
                        else
                        {
                            if (ContractNumber.Length >= 3)
                                OnServiceStatusChange(RequiedFields.ContractNumber, Status.Enable);
                            else
                            {
                                result = "At least 3 Characters";
                                OnServiceStatusChange(RequiedFields.ContractNumber, Status.Disable);
                            }
                           // status = Status.Enable;
                            //OnServiceStatusChange(RequiedFields.ContractNumber, Status.Enable);
                        }
                        break;
                    case "NumberOfContractors":
                        if (string.IsNullOrWhiteSpace(NumberOfContractors))
                        {
                            result = "Must not be empty";
                            status = Status.Disable;
                            OnServiceStatusChange(RequiedFields.NoOfContractor, Status.Disable);
                        }
                        else
                            OnServiceStatusChange(RequiedFields.NoOfContractor, Status.Enable);
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
