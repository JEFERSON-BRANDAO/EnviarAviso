using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace Classes
{
    public class Email
    {
        public string Enviar(string De, string Para, string Copia, string Corpo, string Assunto, string Pallet, string Servidor_Smtp, string quantidade, string dataHora, string Modelo, string TotalDias, string NumContainer, int Sistema)
        {
            #region ENVIAR EMAIL

            MailMessage Mail = new MailMessage();
            StringBuilder msgBody = new StringBuilder();
            string saudacao = string.Empty;
            //
            if (DateTime.Now.Hour >= 00 && DateTime.Now.Hour <= 11)
            {
                saudacao = "Bom dia!";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 17)
            {
                saudacao = "Boa tarde!";
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 23)
            {
                saudacao = "Boa noite!";
            }
            //
            msgBody.Append("<br /><br />");

            if (Sistema == 0)
            {
                Corpo += saudacao +
                       "<br /><br />" +
                       " O Envio dos arquivos via FTP, do PALLET: " + Pallet + " Modelo " + Modelo + " foi realizado com sucesso." +
                       "<br /> " +
                       "Quantidade: " + quantidade +
                       "<br /> " +
                       "Data hora: " + dataHora +
                       "<br /><br />" +
                       "Atenciosamente," +
                       "<br /> <br /> <br /> <br />" +
                       "SISTEMA FTP - [QA]";
            }
            else if (Sistema == 1)
            {
                string textoVencimento = string.Empty;
                if (Assunto.Contains("DEMURRAGE"))
                {
                    if (int.Parse(TotalDias) < 0)
                    {
                        textoVencimento = "Informamos que a Demurrage atingiu seu vencimento há  " + TotalDias.Replace("-", "") + " dia(s).";
                    }
                    else
                    {
                        textoVencimento = int.Parse(TotalDias) > 0 ? "Informamos que falta(m): " + TotalDias + " dia(s) para o vencimento da Demurrage." : "Informamos que hoje a Demurrage atinge seu vencimento. ";
                    }
                }
                else if (Assunto.Contains("PRANCHA"))
                {
                    textoVencimento = "Informamos que o total de diária(s) de prancha atualmente está em: " + TotalDias + " dia(s).";
                }
                //
                Corpo += saudacao +
                       "<br /><br />" +
                       " Prezado(s)," +
                       "<br /> " +
                      textoVencimento +
                       "<br /> " +
                       "Número Container: " + NumContainer +
                       "<br /><br />" +
                       "Atenciosamente," +
                       "<br /> <br /> <br /> <br />" +
                       "IMPORT SYSTEM";
            }
            else if (Sistema == 2)
            {
                Corpo += saudacao +
                       "<br /><br />" +
                       " Presado(as)," +
                       "<br /> " +
                       "O material " + Pallet + " encotra-se em estoque baixo." +
                       "<br /> " +
                       "Minimo: " + quantidade +
                       "<br />" +
                       "Sub Total: " + TotalDias +
                       "<br /><br />" +
                       "Atenciosamente," +
                       "<br /> <br /> <br /> <br />" +
                       "SISTEMA SPAREPARTS";
            }
            else if (Sistema == 3)
            {
                string _sCorpoConteudo = Corpo;
                Corpo = saudacao +
                     "<br /><br />" +
                     " O Envio dos arquivos Modelo " + Modelo + " foi realizado com sucesso." +
                     "<br /> " + _sCorpoConteudo +
                      "<br /><br />" +
                       "Atenciosamente," +
                       "<br /> <br /> <br /> <br />" +
                       "SISTEMA SHIPPING SFTP";

                //Corpo += saudacao +
                //       "<br /><br />" +
                //       " O Envio dos arquivos via SFTP, do PALLET: " + Pallet + " Modelo " + Modelo + " foi realizado com sucesso." +
                //       "<br /> " +
                //       "Quantidade: " + quantidade +
                //       "<br /> " +
                //       "Data hora: " + dataHora +
                //       "<br /><br />" +
                //       "Atenciosamente," +
                //       "<br /> <br /> <br /> <br />" +
                //       "SISTEMA SHIPPING SFTP";
            }
            //
            string sCorpoEmail = Corpo;
            sCorpoEmail = sCorpoEmail + msgBody.ToString();
            //
            Mail.To.Add(Para);
            if (!string.IsNullOrEmpty(Copia))
                Mail.CC.Add(Copia);
            Mail.From = new MailAddress(De, "AVISO");
            Mail.Subject = Assunto;
            Mail.SubjectEncoding = System.Text.Encoding.UTF8;
            Mail.Body = sCorpoEmail;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Mail.IsBodyHtml = true;
            Mail.Priority = MailPriority.High;
            //
            try
            {
                //Servidor_Smtp = "10.19.214.33";
                SmtpClient smtp = new SmtpClient(Servidor_Smtp);
                smtp.Port = 25;
                //smtp.Host = "Br1vmweb02";
                //smtp.UseDefaultCredentials = false;                
                smtp.Send(Mail);

                return "true";
            }
            catch (Exception erro)
            {
                string message = erro.Message;
                //
                if (erro.InnerException != null)
                {
                    message += " - " + erro.InnerException.Message;
                }

                return message;
            }

            #endregion
        }

        public string Destino()
        {
            #region EMAIL DESTINO

            string destino = string.Empty;
            //
            try
            {

                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\EMAIL\DESTINO.txt";
                string linha;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        destino += linha;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //
            return destino.ToLower().Trim();

            #endregion
        }

        public string Remetente()
        {
            #region EMAIL REMETENTE

            string remetente = string.Empty;
            //
            try
            {

                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\EMAIL\REMETENTE.txt";
                string linha;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        remetente += linha;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //
            return remetente.ToLower().Trim();

            #endregion
        }

        public string Smtp()
        {
            #region SERVIDOR SMTP

            string servidor = string.Empty;
            //
            try
            {

                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\EMAIL\SERVIDOR_SMTP.txt";
                string linha;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        servidor += linha;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //
            return servidor.Trim();

            #endregion
        }

        public string Para()
        {
            #region EMAIL DESTINO

            string permisao = string.Empty;
            //
            try
            {

                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\EMAIL\PARA.txt";
                string linha;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        permisao += linha;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //
            return permisao.Trim();

            #endregion
        }

        public string Copia()
        {
            #region EMAIL CÓPIA

            string permisao = string.Empty;
            //
            try
            {

                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\EMAIL\COPIA.txt";
                string linha;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        permisao += linha;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //
            return permisao.Trim();

            #endregion
        }
    }
}