using QRGenerator.Services.Models;
using System;


namespace QRGenerator.Shared
{
    public abstract class BaseViewModel : BaseNotifable, IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposed)
        {
        }

        public virtual void OnUpdateCompleted(object model)
        {

        }
        public virtual void Confirm()
        {
        }

        

        
    }
}
