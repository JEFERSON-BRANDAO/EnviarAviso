using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes;
using System.Data;

namespace Classes
{
    class Estacao
    {
        public List<Pallet> Consultar_Pallet(string data)
        {
            #region BUSCA POR PALLETS QUE ESTÃO NA ESTAÇÃO FTP

            OleDbConnect Objconn = new OleDbConnect();
            List<Pallet> ListaPallet = new List<Pallet>();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"Select DISTINCT B.PARENTBUNDLENO, TO_CHAR( A.COMPLETEDATE, 'DD/MM/YYYY HH24:MI:SS') as COMPLETEDATE, B.SKUNO as MODELO 
                                      From  mfworkstatus A, sfcshippack B 
                                      Where A.Location=B.PackNo " +
                                      "and A.CURRENTEVENT='FTP'" +
                                      "and TO_CHAR(A.COMPLETEDATE,'YYYY-MM-DD') ='" + data + "' " +
                                      "and A.LASTEDITBY='SHIPPINGFTP' order by COMPLETEDATE";//Retorna somete se (usuario LASTEDITBY=SHIPPINGFTP) gravado no programa SHIPPING FTP ao passar na estação FTP
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
                            Pallet item = new Pallet();
                            //
                            if (!string.IsNullOrEmpty(linha["PARENTBUNDLENO"].ToString()))
                            {
                                item.TOTAL_PECAS = TOTAL_PECAS(linha["PARENTBUNDLENO"].ToString());
                                item.PALLET_NO = linha["PARENTBUNDLENO"].ToString();
                                item.COMPLETEDATE = linha["COMPLETEDATE"].ToString();
                                item.MODELO = linha["MODELO"].ToString();
                                item.ROWSCOUNT = quantidade;
                                //
                                ListaPallet.Add(item);
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
                objLog.Gravar("Consultar_Pallet", erro.Message, 0);
            }

            return ListaPallet;

            #endregion
        }

        public List<Pallet> Consultar_Pallet_SFTP(string data)
        {
            #region BUSCA POR PALLETS QUE ESTÃO NA ESTAÇÃO SHIPPING

            OleDbConnect Objconn = new OleDbConnect();
            List<Pallet> ListaPallet = new List<Pallet>();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"Select DISTINCT B.PARENTBUNDLENO, TO_CHAR( A.COMPLETEDATE, 'DD/MM/YYYY HH24:MI:SS') as COMPLETEDATE, B.SKUNO as MODELO 
                                      From  mfworkstatus A, sfcshippack B 
                                      Where A.Location=B.PackNo " +
                                      "and A.CURRENTEVENT='SHIPPING' " +
                                      //"and B.SKUNO ='RU9026000643' " +
                                      "and TO_CHAR(A.COMPLETEDATE,'YYYY-MM-DD') ='" + data + "' " +
                                      "and A.LASTEDITBY='SHIPPINGFTP' order by COMPLETEDATE";//Retorna somete se (usuario LASTEDITBY=SHIPPINGFTP) gravado no programa SHIPPING FTP ao passar na estação FTP
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
                            Pallet item = new Pallet();
                            //
                            if (!string.IsNullOrEmpty(linha["PARENTBUNDLENO"].ToString()))
                            {
                                item.TOTAL_PECAS = TOTAL_PECAS(linha["PARENTBUNDLENO"].ToString());
                                item.PALLET_NO = linha["PARENTBUNDLENO"].ToString();
                                item.COMPLETEDATE = linha["COMPLETEDATE"].ToString();
                                item.MODELO = linha["MODELO"].ToString();
                                item.ROWSCOUNT = quantidade;
                                //
                                ListaPallet.Add(item);
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
                objLog.Gravar("Consultar_Pallet", erro.Message, 0);
            }

            return ListaPallet;

            #endregion
        }

        public int TOTAL_PECAS(string Pallet)
        {
            #region PEGA TOTAL DE PEÇAS DO PALLET

            OleDbConnect Objconn = new OleDbConnect();
            int quantidade = 0;
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();

                    //
                    string sql = @" select  count(*) QUANTIDADE from  MFWORKSTATUS WHERE SYSSERIALNO IN (Select  A.SYSSERIALNO From  mfworkstatus A,sfcshippack B 
                                    Where A.Location=B.PackNo 
                                    and parentbundleno = '" + Pallet + "')";
                    //in ('" + Pallet.Replace(", ", "','") + "') )";

                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        quantidade = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["QUANTIDADE"].ToString()) ? 0 : int.Parse(Objconn.Tabela.Rows[0]["QUANTIDADE"].ToString());
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
                objLog.Gravar("QUANTIDADE()", erro.Message, 0);
            }

            return quantidade;

            #endregion
        }

    }
}
