
using QRCoder;
using QRGenerator.Shared;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QRGenerator.ViewModels
{
    public class QRGeneratorViewModel: BaseViewModel
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        private int _isSuccess1, _isSuccess2, _isSuccess3;

        private Information currentinformation;
        private ObservableCollection<Lecturer> _lecturers = new ObservableCollection<Lecturer>();
        private Status buttonstatus, buttonstatus1;
        Bitmap bmp;

        /// <summary>
        /// Constructor
        /// </summary>
        public QRGeneratorViewModel()
        {
            CurrentInformation = new Information();
            SaveCommand = new RelayCommand(SaveSetting);
            PrintCommand = new RelayCommand(PrintQRCode);
            ResetCommand = new RelayCommand(ResetAll);

            bmp = new Bitmap(200, 200);
            ButtonStatus = Status.Disable;
           GenerateBtnStatus = Status.Disable;
            IsSuccessOne = 1;
            //IsSuccessTwo = 1;
            //IsSuccessThree = 1;
            CurrentInformation.Access_Type = 1;
            CurrentInformation.StartDateTime = DateTime.Now;
            CurrentInformation.OnServiceStatusChange += OnStatusChange;
            #region Commented Out Load image
            //----------------------Bind image on load
            //CurrentInformation.Company = "IQL";
            //BitmapImage bi = new BitmapImage();
            //bi.BeginInit();
            //bi.UriSource = new Uri(@"C:\\Users\\rndsof01\\Desktop\\image.jpeg", UriKind.RelativeOrAbsolute);
            //bi.EndInit();
            //CurrentInformation.QrCode = bi;
            //var tmp = GetAllLecturers();
            //foreach (var item in tmp)
            //{
            //    Lecturers.Add(item);
            //}
            #endregion
        }
        Status CompanyStatus, ContractStatus, ContractorStatus;
        private void OnStatusChange(RequiedFields requiedFields,Status obj)
        {
            switch(requiedFields)
            {
                case RequiedFields.Company:
                    CompanyStatus = obj;
                    break;
                case RequiedFields.ContractNumber:
                    ContractStatus = obj;
                    break;
                case RequiedFields.NoOfContractor:
                    ContractorStatus = obj;
                    break;

            }

            if (CompanyStatus==Status.Enable && ContractStatus== Status.Enable && ContractorStatus== Status.Enable)
                GenerateBtnStatus = Status.Enable;
            else
                GenerateBtnStatus = Status.Disable;
        }

        /// <summary>
        /// Reset All 
        /// </summary>
        private void ResetAll()
        {
            CurrentInformation.Company = "";
            CurrentInformation.ContractNumber = "";
            CurrentInformation.QrCode = null;
            CurrentInformation.NumberOfContractors = "";
            CurrentInformation.StartDateTime = DateTime.Now;
            CurrentInformation.Access_Type = 1;
            ButtonStatus = Status.Disable;            
            IsSuccessOne = 0;
            IsSuccessOne = 1;
            //IsSuccessTwo = 0;
            //IsSuccessThree = 0;
        }

               
        /// <summary>
        /// Print QRCODE
        /// </summary>
        private void PrintQRCode()
        {
            //save to image file
            bmp.Save("C:\\Users\\rndsof01\\Desktop\\image.jpeg", ImageFormat.Jpeg);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            printDialog.Document = doc;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        /// <summary>
        /// create QRCode
        /// </summary>
        private void SaveSetting()
        {
           ButtonStatus = Status.Enable;
            
            CurrentInformation.Access_Type = IsSuccessOne;
            
           

            if (CurrentInformation.Company != null && CurrentInformation.ContractNumber != null && CurrentInformation.NumberOfContractors!=null )
            {
                string _data = CurrentInformation.Company + "^" + CurrentInformation.ContractNumber + "^"+ CurrentInformation.Access_Type.ToString() + "^" + CurrentInformation.NumberOfContractors+ "^" + CurrentInformation.StartDateTime.Date.ToString("dd/MM/yyyy") ;
                QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
                QRCodeData data = qr.CreateQrCode(_data, QRCoder.QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                bmp = code.GetGraphic(5);
                
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi = Bitmap2BitmapImage(bmp);
                //bind to the model to show
                CurrentInformation.QrCode = bi;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Company Name, Work Permit Number and Contractor Number can not be empty!!!");
            }


            //change print button to enable
            if (CurrentInformation.QrCode != null)
            {
                ButtonStatus = Status.Enable;
            }


            
        }


        public int IsSuccessOne
        {
            get { return _isSuccess1; }
            set
            {
                SetProperty(ref _isSuccess1, value);
            }
        }
        public int IsSuccessTwo
        {
            get { return _isSuccess2; }
            set
            {
                SetProperty(ref _isSuccess2, value);
            }
        }
        public int IsSuccessThree
        {
            get { return _isSuccess3; }
            set
            {
                SetProperty(ref _isSuccess3, value);
            }
        }




        /// <summary>
        /// Converting Bitmap to BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public Information CurrentInformation  
        {
            get { return currentinformation; }
            set
            {                
                SetProperty(ref currentinformation, value);
            }
        }

        public Status ButtonStatus
        {
            get { return buttonstatus; }
            set { SetProperty(ref buttonstatus, value); }
        }
        public Status GenerateBtnStatus
        {
            get { return buttonstatus1; }
            set { SetProperty(ref buttonstatus1, value); }
        }

       




        //public ObservableCollection<Lecturer> Lecturers
        //{
        //    get { return _lecturers; }
        //    set { _lecturers = value; }
        //}
        /// <summary>
        /// getting a list
        /// </summary>
        /// <returns></returns>
        //private ObservableCollection<Lecturer> GetAllLecturers()
        //{
        //    ObservableCollection<Lecturer> result = new ObservableCollection<Lecturer>();

        //    for (int i = 0; i < 2; i++)
        //    {
        //        result.Add(
        //            new Lecturer
        //            {
        //                Id_Lecturer = i,
        //                Name = "Name " + i,
        //                Phone_Number = i.ToString(),
        //                Surname = "Surname " + i


        //            });
        //    }

        //    return result;
        //}


    }
    /// <summary>
    /// Model Class
    /// </summary>
    public class Lecturer
    {
        public Lecturer() { }

        public int Id_Lecturer { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone_Number { get; set; }
    }
}
