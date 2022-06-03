using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Classes
{
    class Demurrage
    {
        public string IDDEMURRAGE { get; set; }
        public string NR_CONTAINER { get; set; }
        public string CONTAGEM_DIAS { get; set; }
        public string ETA_MAO { get; set; }
        public string DATA_IDEAL_DEV { get; set; }
        public string VENCIMENTO { get; set; }
        public string ETA_FOXCONN { get; set; }

        public List<Demurrage> Consultar_Demurrage()
        {
            #region BUSCA POR DEMURRAGE

            MySQLDbConnect Objconn = new MySQLDbConnect();
            List<Demurrage> ListaDemurrage = new List<Demurrage>();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT  a.IDDEMURRAGE, 
                                        a.DATA_IDEAL_DEV,
                                        a.VENCIMENTO,  
                                        a.CONTAGEM_DIAS,
                                        b.ETA_MAO,
                                        b.ETA_FOXCONN, 
                                        b.NR_CONTAINER,
                                        b.DI,                                    
                                        b.LIBERADO
                                     FROM importacao.demurrage a 
                                     INNER JOIN importacao.sea_shipment b ON a.IDDEMURRAGE = b.IDDEMURRAGE 
                                     WHERE b.LIBERADO = 0
                                     AND b.DATA_DEVOLUCAO is null";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    int quantidade = Objconn.Tabela.Rows.Count;
                    //
                    if (quantidade > 0)
                    {
                        foreach (DataRow linha in Objconn.Tabela.Rows)
                        {
                            Demurrage item = new Demurrage();
                            //
                            if (!string.IsNullOrEmpty(linha["NR_CONTAINER"].ToString()))
                            {
                                item.IDDEMURRAGE = linha["IDDEMURRAGE"].ToString();
                                item.NR_CONTAINER = linha["NR_CONTAINER"].ToString();
                                item.CONTAGEM_DIAS = linha["CONTAGEM_DIAS"].ToString();
                                item.ETA_MAO = linha["ETA_MAO"].ToString();
                                item.DATA_IDEAL_DEV = linha["DATA_IDEAL_DEV"].ToString();
                                item.VENCIMENTO = linha["VENCIMENTO"].ToString();
                                item.ETA_FOXCONN = linha["ETA_FOXCONN"].ToString();
                                //
                                ListaDemurrage.Add(item);
                            }
                        }
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                Log objLog = new Log();
                objLog.Gravar("Consultar_Demurrage", erro.Message, 1);
            }

            return ListaDemurrage;

            #endregion
        }

        public bool Atualizar(int dias, string IdDemurrage)
        {
            #region ATUALIZA CONTAGEM DE ENVIO DE EMAIL

            bool status = false;
            //
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"UPDATE importacao.demurrage SET CONTAGEM_DIAS=" + dias +
                                         " WHERE IDDEMURRAGE =" + IdDemurrage;

                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Isvalid)
                    {
                        status = true;
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                Log objLog = new Log();
                objLog.Gravar("ATUALIZAR()", erro.Message, 1);
            }

            return status;

            #endregion

        }

        public string Emails()
        {
            #region LISTA DE EMAILs

            string emails = string.Empty;
            MySQLDbConnect Objconn = new MySQLDbConnect();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT EMAIL FROM  importacao.usuario WHERE STATUS =1 AND EMAIL <>'' ";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    int rowCount = Objconn.Tabela.Rows.Count;
                    if (rowCount > 0)
                    {
                        for (int index = 0; index < rowCount; index++)
                        {
                            emails +=  Objconn.Tabela.Rows[index]["EMAIL"].ToString() + ",";
                        }

                        //
                        emails = emails.Remove(emails.Length - 1);

                    }
                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                Log objLog = new Log();
                objLog.Gravar("ATUALIZAR()", erro.Message, 1);
            }

            return emails;

            #endregion
        }

    }
}
