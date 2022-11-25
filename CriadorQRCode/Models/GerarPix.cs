using CriadorQRCode.Models.Utils;
using System.Text;

namespace CriadorQRCode.Models
{
    public class GerarPix
    {
        /*
         * validador https://www.gerarpix.com.br/
         * 
        exemplo com valor
        000201
        2636
          0014BR.GOV.BCB.PIX
          0114+5513988728776
        52040000
        5303986
        54071523.00
        5802BR
        5907Stefano
        6012Praia Grande
        62070503***
        630471D4
                 */


        public GerarPix(DadosPIXView data)
        {
            ChavePix = data.ChavePix;
            NomeRazao = data.NomeRazao;
            Municipio = data.Municipio;
            Valor = data.Valor;
        }

        public GerarPix(string urlChavePix)
        {
            //seta valores
        }
        public TipoPIX Tipo { get; set; } = TipoPIX.Telefone;
        public string ChavePix { get; set; }
        public string NomeRazao { get; set; }
        public string Municipio { get; set; }        
        public double Valor { get; set; }

        public string StrChavePix()
        {
            return getCamposTipo()+ calcModuloCRC16();
        }
       
        string calcModuloCRC16()
        {
            CalculadorCRC16 crc16 = new CalculadorCRC16();
            var result = crc16.ComputeChecksum(
                                        Encoding.ASCII.GetBytes(getCamposTipo())
                                        )
                            .ToString("X4");

            return result;//"71D4";
        }
        string getCamposTipo()
        {
            return Tipo00 + Tipo26 + Tipo52 + Tipo53 + Tipo54 +
                Tipo58 + Tipo59 + Tipo60 + Tipo62 + Tipo63;
        }

        #region Variaveis Tipos
        string Tipo00
        {
            get
            {
                return "000201";
            }
        }

        string Tipo2600
        {
            get
            {
                return "0014BR.GOV.BCB.PIX";
            }
        }
        string Tipo2601
        {
            get
            {
                string chave = ChavePix;

                if (Tipo == TipoPIX.Telefone && !chave.StartsWith("+55"))
                {
                    chave = "+55" + chave;
                }

                return "0114"+ chave;
            }
        }
        string Tipo26
        {
            get
            {
                string text = Tipo2600 + Tipo2601;
                return "26" +formatString(text.Length) + text;
            }
        }
        string Tipo52
        {
            get
            {
                return "52040000";
            }
        }
        string Tipo53
        {
            get
            {
                return "5303986";
            }
        }
        string Tipo54
        {
            get
            {
                string valor = Valor.ToString("f").Replace(",",".");
                string text = Valor > 0 ? "54" +formatString(valor.Length) + valor : ""; 

                return text;
            }
        }
        string Tipo58
        {
            get
            {
                return "5802BR";
            }
        }
        string Tipo59
        {
            get
            {
                return "59" + formatString(NomeRazao.Length) + NomeRazao;
            }
        }
        string Tipo60
        {
            get
            {
                return "60" + formatString(Municipio.Length) + Municipio;
            }
        }
        string Tipo6205
        {
            get
            {
                return "0503***";
            }
        }
        string Tipo62
        {
            get
            {
                return "62"+ formatString(Tipo6205.Length) + Tipo6205;
            }
        }
        string Tipo63
        {
            get
            {
                return "6304";
            }
        }
        #endregion
        string formatString(int valor)
        {
            //formatar com 2 casas decimais
            return valor.ToString("00");
        }
    }
}
