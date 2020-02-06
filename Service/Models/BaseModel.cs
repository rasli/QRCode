using System;
using System.ComponentModel.DataAnnotations;

namespace QRGenerator.Services.Models
{
    public abstract class BaseModel : BaseNotifable
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                SetProperty(ref id, value);
            }
        }

        public void ValidateProperty<T>(T value, string name)
        {
            //try
            //{
            //    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            //    {
            //        MemberName = name
            //    });
            //}
            //catch (Exception e)
            //{


            //}

            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });

        }

    }
}