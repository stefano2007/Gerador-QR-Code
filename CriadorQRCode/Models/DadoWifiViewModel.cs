using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CriadorQRCode.Models
{
    
    public class DadoWifiViewModel
    {
        [DisplayName("Nome da Rede")]
        [Required]
        public string ssid { get; set; }
        [DisplayName("Senha da Rede")]
        [Required]
        public string password { get; set; }
        [DisplayName("Tipo de criptografia")]
        [Required]
        public TipoAuthWifi Tipo { get; set; } = TipoAuthWifi.WPA;

        [DisplayName("Rede Escondida?")]
        [Required]
        public bool isHiddenSSID { get; set; }
        public byte[] QrCode { get; set; }
        public string StrQrCode { get; set; }
        public bool Processado { get; set; } = false;
    }
}
