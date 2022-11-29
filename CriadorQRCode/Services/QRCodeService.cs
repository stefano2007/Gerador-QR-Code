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
        public DadoPIXViewModel GerarChavePix(DadoPIXViewModel data)
        {
            GerarPix gerarPix = new GerarPix(data);
            string strChavePix = gerarPix.StrChavePix();

            data.StrQrCode = strChavePix;
            data.QrCode = conversor.GerarQrPixCode(strChavePix, false);
            data.Processado = true;

            return data;
        }

        public DadoWifiViewModel GerarWifi(DadoWifiViewModel data)
        {            
            data.QrCode = conversor.GerarQrWifiCode(data, false);
            data.Processado = true;

            return data;
        }
    }
}
