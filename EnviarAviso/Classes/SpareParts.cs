using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Classes
{
    class SpareParts
    {
        public string IDMATERIAL { get; set; }
        public string MATERIAL { get; set; }
        public string MINIMO { get; set; }
        public string SUBTOTAL { get; set; }
        public string AVISO { get; set; }

        public List<SpareParts> Consultar_SpareParts()
        {
            #region BUSCA POR MATERIAL

            MySQLDbConnect Objconn = new MySQLDbConnect();
            List<SpareParts> ListaSpareParts = new List<SpareParts>();
            //
            try
            {
                try
                {
                    Objconn.String_Connection();//string de conexao
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string data = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    string sql = @"SELECT IDMATERIAL, NOME AS MATERIAL, MINIMO, SUBTOTAL, AVISO FROM spare_parts.material WHERE AVISO='" + data + "' AND SUBTOTAL<= MINIMO";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    int quantidade = Objconn.Tabela.Rows.Count;                    
                    if (quantidade > 0)
                    {
                        foreach (DataRow linha in Objconn.Tabela.Rows)
                        {
                            SpareParts item = new SpareParts();
                            //
                            item.IDMATERIAL = linha["IDMATERIAL"].ToString();
                            item.MATERIAL = linha["MATERIAL"].ToString();
                            item.MINIMO = linha["MINIMO"].ToString();
                            item.SUBTOTAL = linha["SUBTOTAL"].ToString();
                            item.AVISO = linha["AVISO"].ToString();
                            //
                            ListaSpareParts.Add(item);
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
                objLog.Gravar("Consultar_ListaSpareParts", erro.Message, 2);
            }
            //
            return ListaSpareParts;

            #endregion
        }

        public bool Atualizar(string IdMaterial)
        {
            #region ATUALIZA ENVIO DE EMAIL

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
                    string nextDate = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
                    string sql = @"UPDATE spare_parts.material  SET AVISO='" + nextDate + "' WHERE IDMATERIAL =" + IdMaterial;

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
                objLog.Gravar("ATUALIZAR()", erro.Message, 2);
            }

            return status;

            #endregion
        }

    }
}
