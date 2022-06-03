using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;
using System.Collections;
using System.IO;
using System.Text;


namespace Classes
{
    class Pallet
    {
        public string PALLET_NO { get; set; }
        public string COMPLETEDATE { get; set; }
        public string MODELO { get; set; }
        public int TOTAL_PECAS { get; set; }
        public int ROWSCOUNT { get; set; }


        public void RegistraEnvio(string Pallet)
        {
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //                  
                    string sql = @"UPDATE MFWORKSTATUS SET LASTEDITBY='ENVIARAVISO'                                        
                                        WHERE SYSSERIALNO IN (Select  A.SYSSERIALNO From  mfworkstatus A,sfcshippack B 
                                        Where A.Location=B.PackNo 
                                        and parentbundleno ='" + Pallet + "')";

                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (!Objconn.Isvalid)
                    {
                        Log objLog = new Log();
                        objLog.Gravar("RegistraEnvio()", Objconn.Message, 0);
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                //
                Log objLog = new Log();
                objLog.Gravar("RegistraEnvio()", erro.Message, 0);
            }
        }
    }

}
