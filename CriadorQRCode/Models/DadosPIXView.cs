using System.ComponentModel;

namespace CriadorQRCode.Models
{
    
    public class DadosPIXView
    {
        public TipoPIX Tipo { get; set; } = TipoPIX.Telefone;
        [DisplayName("Chave PIX")]
        public string ChavePix { get; set; }

        [DisplayName("Nome do beneficiário (até 25 letras)")]
        public string NomeRazao { get; set; }

        [DisplayName("Cidade do beneficiário ou da transação (até 15 letras)")]
        public string Municipio { get; set; }

        [DisplayName("Valor para transferência (opcional)")]
        public double Valor { get; set; } = 0;

        public byte[] QrCode { get; set; }

        public string StrQrCode { get; set; }
        public bool Processado { get; set; } = false;
    }
}
