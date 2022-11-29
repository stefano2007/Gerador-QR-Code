using Microsoft.AspNetCore.Hosting;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static QRCoder.PayloadGenerator.WiFi;

namespace CriadorQRCode.Models.Utils
{
    public class QRCodeConversor
    {
        //Define uma instância de IHostingEnvironment
        IWebHostEnvironment _appEnvironment;
        public QRCodeConversor(IWebHostEnvironment env)
        {
            _appEnvironment = env;
        }
        public byte[] GerarQrPixCode(string chave, bool includeLogo = false)
        {
            var qrGenerator = new QRCodeGenerator();
            //var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var qrCodeData = qrGenerator.CreateQrCode(chave, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeBitmap;

            if (includeLogo)
            {
                using (var logo = (Bitmap)Bitmap.FromFile(@$"{_appEnvironment.WebRootPath}\img\insta.png"))
                {
                    qrCodeBitmap = qrCode.GetGraphic(32, Color.Black, Color.White, logo);
                }
            }
            else
            {
                qrCodeBitmap = qrCode.GetGraphic(32);
            }

            using (qrCodeBitmap)
            {
                return ConverterImagemParaBytes(qrCodeBitmap);
            }
        }

        public byte[] GerarQrWifiCode(DadoWifiViewModel data, bool includeLogo = false)
        {
            var qrGenerator = new QRCodeGenerator();

            var payload = new PayloadGenerator.WiFi(data.ssid, data.password, (Authentication) data.Tipo, data.isHiddenSSID);

            var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeBitmap;

            if (includeLogo)
            {
                using (var logo = (Bitmap)Bitmap.FromFile(@$"{_appEnvironment.WebRootPath}\img\insta.png"))
                {
                    qrCodeBitmap = qrCode.GetGraphic(32, Color.Black, Color.White, logo);
                }
            }
            else
            {
                qrCodeBitmap = qrCode.GetGraphic(32);
            }
            //atualizar valor do qr code gerado
            data.StrQrCode = payload.ToString();

            using (qrCodeBitmap)
            {
                return ConverterImagemParaBytes(qrCodeBitmap);
            }
        }

        private static byte[] ConverterImagemParaBytes(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);

                return stream.ToArray();
            }
        }
    }
}
