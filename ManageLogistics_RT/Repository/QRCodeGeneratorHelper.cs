using ManageLogistics_RT.Helpers;
using QRCoder;

namespace ManageLogistics_RT.Repository
{
    public class QRCodeGeneratorHelper : IQRCodeGenerator
    {
        public byte[] GenerateQRCode(string text)
        {
            byte[] QRCode = new byte[0];
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeGenerator  QRCodeGenerator = new QRCodeGenerator();
                QRCodeData data = QRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode bitmap = new BitmapByteQRCode(data);
                QRCode = bitmap.GetGraphic(20);
            }
            return QRCode;
        }
    }
}
