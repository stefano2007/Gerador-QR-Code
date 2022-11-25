using CriadorQRCode.Models;
using CriadorQRCode.Models.Utils;

namespace CriadorQRCode.Services
{
    public class QRCodeService
    {
        QRCodeConversor conversor;
        public QRCodeService(QRCodeConversor _conversor)
        {
            conversor = _conversor;
        }
        public DadosPIXView GerarChavePix(DadosPIXView data)
        {
            GerarPix gerarPix = new GerarPix(data);
            string strChavePix = gerarPix.StrChavePix();

            data.StrQrCode = strChavePix;
            data.QrCode = conversor.GerarQrCode(strChavePix, false);
            data.Processado = true;

            return data;
        }
    }
}
