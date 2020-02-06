using Service.Models;

namespace Service.Interfaces
{
    public interface IQRCodeGenerator
    {
        void GenerateQRCode(Information model);
    }
}
