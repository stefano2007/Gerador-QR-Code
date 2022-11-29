using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CriadorQRCode.Models
{
    
    public class DadoPIXViewModel
    {
        public TipoPIX Tipo { get; set; } = TipoPIX.Telefone;
        [DisplayName("Chave PIX")]
        [Required]
        [MaxLength(99)]
        public string ChavePix { get; set; }

        [DisplayName("Nome do beneficiário (até 25 letras)")]
        [Required]
        [MaxLength(25)]
        public string NomeRazao { get; set; }

        [DisplayName("Cidade do beneficiário ou da transação (até 15 letras)")]
        [Required]
        [MaxLength(15)]
        public string Municipio { get; set; }
        [Required]

        [DisplayName("Valor para transferência (opcional)")]
        public double Valor { get; set; } = 0;
        public byte[] QrCode { get; set; }
        public string StrQrCode { get; set; }
        public bool Processado { get; set; } = false;
    }
}
