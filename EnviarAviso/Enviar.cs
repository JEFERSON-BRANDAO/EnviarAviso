using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Classes;
using System.Globalization;

// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 09/27/2018
// DESCRIPTION  : Sistema para enviar email dos pallets modelo E965 que passaram pela estação FTP
// SPECIAL NOTES: Disparar email informando embarque dos pallets modelo NEMO da ROKU
// ===============================
// Change History: version 1.0.0.6 
//                                  
// Date:24/11/20
//==================================

namespace SendFTP
{
    public partial class Send : Form
    {
        int statusConexao = 0;
        int segundos = 0;
        int tempo = 0;
        int sistema = 0;
        const int DevolucaoIdeal = 10;//qdo faltarem 10 dias para vencimento, começa avisar

        public Send()
        {
            InitializeComponent();

            #region RODAPÉ

            int anoCriacao = 2018;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            //
            lbRodape.Text = "Copyright © " + anoCriacao + texto;

            #endregion
            //
            txtData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            tempo = GetTempo();
            //
            CriaColunas();
            //
            Ligar();

        }

        private void Ligar()
        {
            lbAviso.Text = "LIGADO";
            lbAviso.Visible = true;
            lbAviso.ForeColor = System.Drawing.Color.Blue;
            timerCount.Start();
            statusConexao = 1;
        }

        public int GetTempo()
        {
            #region SEGUNDOS txt

            string arquivo = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\TEMPO.txt";
            string linha;
            int tempo = 0;
            string aux = string.Empty;
            //
            if (System.IO.File.Exists(arquivo))
            {
                System.IO.StreamReader arqTXT = new System.IO.StreamReader(arquivo);
                //
                while ((linha = arqTXT.ReadLine()) != null)
                {
                    for (int index = 0; index < linha.Length; index++)
                    {
                        aux += linha[index];
                    }
                }
                //
                arqTXT.Close();
            }

            tempo = string.IsNullOrEmpty(aux) ? tempo : int.Parse(aux.Trim());
            //
            return tempo;

            #endregion
        }

        private void CriaColunas()
        {
            gridShipped.Columns.Clear();
            gridShipped.Rows.Clear();

            //grid sistema FTP
            if (rdbFTP.Checked)
            {
                sistema = 0;

                if (gridShipped.ColumnCount == 0)//Para refresh não duplicar coluna
                {
                    gridShipped.Columns.Add("", "PALLET_NO");//CRIA COLUNA  
                    gridShipped.Columns.Add("", "MODELO");//CRIA COLUNA
                    gridShipped.Columns.Add("", "COMPLETEDATE");//CRIA COLUNA
                    gridShipped.Columns.Add("", "QUANTIDADE");//CRIA COLUNA
                }
                //
                CarregaDadosFTP();
            }
            //grid sistema de importação
            if (rdbDemurrage.Checked)
            {
                sistema = 1;

                if (gridShipped.ColumnCount == 0)//Para refresh não duplicar coluna
                {
                    gridShipped.Columns.Add("", "IDDEMURRAGE");//CRIA COLUNA  
                    gridShipped.Columns.Add("", "NR_CONTAINER");//CRIA COLUNA  
                    gridShipped.Columns.Add("", "CONTAGEM_DIAS");//CRIA COLUNA
                    gridShipped.Columns.Add("", "ETA_MAO");//CRIA COLUNA
                    gridShipped.Columns.Add("", "DATA_IDEAL_DEV");//CRIA COLUNA
                    gridShipped.Columns.Add("", "VENCIMENTO");//CRIA COLUNA
                    gridShipped.Columns.Add("", "ETA_FOXCONN");//CRIA COLUNA
                }
                //
                CarregaDadosDemurrage();
            }
            //grid sistema spareParts
            if (rdbSpareParts.Checked)
            {
                sistema = 2;

                if (gridShipped.ColumnCount == 0)//Para refresh não duplicar coluna
                {
                    gridShipped.Columns.Add("", "IDMATERIAL");//CRIA COLUNA  
                    gridShipped.Columns.Add("", "MATERIAL");//CRIA COLUNA 
                    gridShipped.Columns.Add("", "MINIMO");//CRIA COLUNA 
                    gridShipped.Columns.Add("", "SUBTOTAL");//CRIA COLUNA 
                }
                //
                CarregaDadosSpareParts();
            }

            //grid sistema SFTP
            if (rdbSFTP.Checked)
            {
                sistema = 3;

                if (gridShipped.ColumnCount == 0)//Para refresh não duplicar coluna
                {
                    gridShipped.Columns.Add("", "PALLET_NO");//CRIA COLUNA  
                    gridShipped.Columns.Add("", "MODELO");//CRIA COLUNA
                    gridShipped.Columns.Add("", "COMPLETEDATE");//CRIA COLUNA
                    gridShipped.Columns.Add("", "QUANTIDADE");//CRIA COLUNA
                }
                //
                CarregaDadosSFTP();
            }
        }

        private void CarregaDadosFTP()
        {
            Estacao ftp = new Estacao();
            string date = txtData.Text;
            //
            DateTimeFormatInfo ukDtfi = new CultureInfo("pt-BR", false).DateTimeFormat;
            DateTime SqlDataInicio = new DateTime();
            DateTime.TryParse(date, ukDtfi, DateTimeStyles.None, out SqlDataInicio);
            //
            if (Convert.ToString(SqlDataInicio) == "1/1/0001 00:00:00")
            {
                gridShipped.Rows.Clear();
                segundos = 0;
            }
            //
            List<Pallet> listaPallet = new List<Pallet>();
            listaPallet = ftp.Consultar_Pallet(date);
            int rowCount = listaPallet.Count;
            //
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    #region PREENCHE O GRID

                    string PALLET_NO = listaPallet[index].PALLET_NO;
                    string COMPLETEDATE = listaPallet[index].COMPLETEDATE;
                    string MODELO = listaPallet[index].MODELO;
                    int QUANTIDADE = listaPallet[index].TOTAL_PECAS;
                    //
                    gridShipped.Rows.Add();//adiciona os registros no grid
                    //
                    gridShipped.Rows[index].Cells[0].Value = PALLET_NO;
                    gridShipped.Rows[index].Cells[1].Value = MODELO;
                    gridShipped.Rows[index].Cells[2].Value = COMPLETEDATE;
                    gridShipped.Rows[index].Cells[3].Value = QUANTIDADE;
                    //
                    gridShipped.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT do GRID
                    gridShipped.ClearSelection();

                    #endregion
                }

                int RowCountGrid = gridShipped.Rows.Count;
                //
                if (RowCountGrid > 0)
                {
                    for (int index = 0; index < RowCountGrid; index++)
                    {
                        string PALLET_NO = gridShipped.Rows[index].Cells[0].Value.ToString();
                        string MODELO = gridShipped.Rows[index].Cells[1].Value.ToString();
                        string COMPLETEDATE = gridShipped.Rows[index].Cells[2].Value.ToString();
                        string QUANTIDADE = gridShipped.Rows[index].Cells[3].Value.ToString();
                        //
                        #region ENVIA EMAIL

                        Email objEmail = new Email();
                        string remetente = objEmail.Remetente();
                        string para = objEmail.Para();
                        string copia = objEmail.Copia();
                        string corpo = "";
                        string assunto = "ENVIO DE ARQUIVO FTP Modelo [" + MODELO + "] - " + PALLET_NO;
                        string Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, PALLET_NO, objEmail.Smtp(), QUANTIDADE, COMPLETEDATE, MODELO, string.Empty, string.Empty, sistema);

                        if (Enviado == "true")
                        {
                            #region REGISTRA O ENVIO DE EMAIL

                            //RegistraEnvio(PALLET_NO);
                            Pallet pallet = new Pallet();
                            pallet.RegistraEnvio(PALLET_NO);

                            #endregion
                            //
                            lbAviso.Visible = true;
                            lbAviso.Text = "Email enviado com sucesso";
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            //
                            Log objLog = new Log();
                            objLog.Gravar(string.Empty, "PALLET: " + PALLET_NO + " MODELO: " + MODELO + " QUANTIDADE: " + QUANTIDADE + " Email enviado com sucesso", 0);
                        }
                        else
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = "Erro: " + Enviado;
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            //
                            Log objLog = new Log();
                            objLog.Gravar("CarregaDadosFTP()", "Erro: " + Enviado, 0);
                        }

                        #endregion
                    }
                }
                else
                {
                    gridShipped.Columns.Clear();
                    gridShipped.Rows.Clear();
                    gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA                  
                }

            }
            else
            {
                gridShipped.Columns.Clear();
                gridShipped.Rows.Clear();
                gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA             
            }
            //
            txtData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data
        }

        private void CarregaDadosDemurrage()
        {
            Demurrage demurrage = new Demurrage();
            //
            List<Demurrage> listaDemurrage = new List<Demurrage>();
            listaDemurrage = demurrage.Consultar_Demurrage();
            int rowCount = listaDemurrage.Count;
            //
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    #region PREENCHE O GRID

                    string IDDEMURRAGE = listaDemurrage[index].IDDEMURRAGE;
                    string NR_CONTAINER = listaDemurrage[index].NR_CONTAINER;
                    string CONTAGEM_DIAS = listaDemurrage[index].CONTAGEM_DIAS;
                    string ETA_MAO = listaDemurrage[index].ETA_MAO;
                    string DATA_IDEAL_DEV = listaDemurrage[index].DATA_IDEAL_DEV;
                    string VENCIMENTO = listaDemurrage[index].VENCIMENTO;
                    string ETA_FOXCONN = listaDemurrage[index].ETA_FOXCONN;
                    //
                    gridShipped.Rows.Add();//adiciona os registros no grid
                    //
                    gridShipped.Rows[index].Cells[0].Value = IDDEMURRAGE;
                    gridShipped.Columns[0].Visible = false;
                    //
                    gridShipped.Rows[index].Cells[1].Value = NR_CONTAINER;
                    gridShipped.Rows[index].Cells[2].Value = CONTAGEM_DIAS;
                    gridShipped.Rows[index].Cells[3].Value = ETA_MAO;
                    gridShipped.Rows[index].Cells[4].Value = DATA_IDEAL_DEV;
                    gridShipped.Rows[index].Cells[5].Value = VENCIMENTO;
                    gridShipped.Rows[index].Cells[6].Value = ETA_FOXCONN;
                    //
                    gridShipped.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT do GRID
                    gridShipped.ClearSelection();

                    #endregion
                }

                int RowCountGrid = gridShipped.Rows.Count;
                //
                if (RowCountGrid > 0)
                {
                    for (int index = 0; index < RowCountGrid; index++)
                    {
                        string IDDEMURRAGE = gridShipped.Rows[index].Cells[0].Value.ToString();
                        string NR_CONTAINER = gridShipped.Rows[index].Cells[1].Value.ToString();
                        string CONTAGEM_DIAS = gridShipped.Rows[index].Cells[2].Value.ToString();
                        string ETA_MAO = gridShipped.Rows[index].Cells[3].Value.ToString();
                        string DATA_IDEAL_DEV = gridShipped.Rows[index].Cells[4].Value.ToString();
                        string VENCIMENTO = gridShipped.Rows[index].Cells[5].Value.ToString();
                        string ETA_FOXCONN = gridShipped.Rows[index].Cells[6].Value.ToString();
                        //
                        if (!string.IsNullOrEmpty(VENCIMENTO))
                        {
                            TimeSpan DIF = DateTime.Parse(VENCIMENTO).Subtract(DateTime.Now.Date);
                            int totalDias = int.Parse(DIF.TotalDays.ToString());
                            if ((totalDias >= 0) && (totalDias <= DevolucaoIdeal))//verifica se ainda está no prazo e se prazo de vencimento já está restando <=10 dias para vencimento
                            {
                                if (int.Parse(CONTAGEM_DIAS) >= totalDias)//verifica contagem para não repetir o envio do email
                                {
                                    #region ENVIA EMAIL

                                    Email objEmail = new Email();
                                    string remetente = objEmail.Remetente();
                                    string para = demurrage.Emails();
                                    string copia = objEmail.Copia();
                                    string corpo = "";
                                    string assunto = "VENCIMENTO DEMURRAGE [N.CONTAINER: " + NR_CONTAINER + "]";
                                    string smtp = objEmail.Smtp();
                                    string Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, string.Empty, smtp, string.Empty, string.Empty, string.Empty, totalDias.ToString(), NR_CONTAINER, sistema);

                                    if (Enviado == "true")
                                    {
                                        #region REGISTRA O ENVIO DE EMAIL

                                        demurrage.Atualizar((totalDias - 1), IDDEMURRAGE);//ATUALIZA CONTAGEM PARA CONTROLAR ENVIO DO EMAIL E NÃO EMVIAR O MESMO EMAIL DIVERSAS VEZES AO DIA

                                        #endregion
                                        //
                                        lbAviso.Visible = true;
                                        lbAviso.Text = "Email enviado com sucesso";
                                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                                        //
                                        Log objLog = new Log();
                                        objLog.Gravar(string.Empty, "NÚMERO CONTAINER: " + NR_CONTAINER + ", ETA_MAO: " + DateTime.Parse(ETA_MAO).ToString("dd/MM/yyyy") + ", VENCIMENTO: " + DateTime.Parse(VENCIMENTO).ToString("dd/MM/yyyy") + ", Email enviado com sucesso", 1);
                                    }
                                    else
                                    {
                                        lbAviso.Visible = true;
                                        lbAviso.Text = "Erro: " + Enviado;
                                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                                        //
                                        Log objLog = new Log();
                                        objLog.Gravar("CarregaDados()", "Erro: " + Enviado, 1);
                                    }

                                    #endregion

                                    if (!string.IsNullOrEmpty(ETA_FOXCONN))
                                    {
                                        DIF = DateTime.Now.Date.Subtract(DateTime.Parse(ETA_FOXCONN).AddDays(1));
                                        totalDias = int.Parse(DIF.TotalDays.ToString());
                                        //
                                        #region ENVIA EMAIL

                                        //Email objEmail = new Email();
                                        // string remetente = objEmail.Remetente();
                                        //string para = demurrage.Emails();
                                        //string copia = objEmail.Copia();
                                        //string corpo = "";
                                        assunto = "DIÁRIA DE PRANCHA [N.CONTAINER: " + NR_CONTAINER + "]";
                                        //string smtp = objEmail.Smtp();
                                        Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, string.Empty, smtp, string.Empty, string.Empty, string.Empty, totalDias.ToString(), NR_CONTAINER, sistema);

                                        if (Enviado == "true")
                                        {
                                            lbAviso.Visible = true;
                                            lbAviso.Text = "Email enviado com sucesso";
                                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                                            //
                                            Log objLog = new Log();
                                            objLog.Gravar(string.Empty, "NÚMERO CONTAINER: " + NR_CONTAINER + ", ETA_MAO: " + DateTime.Parse(ETA_MAO).ToString("dd/MM/yyyy") + ", DIÁRIA DE PRANCHA: " + totalDias.ToString() + " DIA(S)" + ", Email enviado com sucesso", 1);
                                        }
                                        else
                                        {
                                            lbAviso.Visible = true;
                                            lbAviso.Text = "Erro: " + Enviado;
                                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                                            //
                                            Log objLog = new Log();
                                            objLog.Gravar("CarregaDados()", "Erro: " + Enviado, 1);
                                        }

                                        #endregion
                                    }
                                }
                            }
                            else//atrasado 
                            {
                                if (!string.IsNullOrEmpty(VENCIMENTO))
                                {
                                    DIF = DateTime.Parse(VENCIMENTO).Subtract(DateTime.Now.Date);
                                    totalDias = int.Parse(DIF.TotalDays.ToString());
                                    if (totalDias < 0)
                                    {
                                        string diasVencidos = totalDias.ToString();
                                        if (int.Parse(diasVencidos) < int.Parse(CONTAGEM_DIAS))
                                        {
                                            CONTAGEM_DIAS = diasVencidos;

                                            #region ENVIA EMAIL

                                            Email objEmail = new Email();
                                            string remetente = objEmail.Remetente();
                                            string para = demurrage.Emails();
                                            string copia = objEmail.Copia();
                                            string corpo = "";
                                            string assunto = "DEMURRAGE VENCIDA [N.CONTAINER: " + NR_CONTAINER + "]";
                                            string smtp = objEmail.Smtp();
                                            string Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, string.Empty, smtp, string.Empty, string.Empty, string.Empty, totalDias.ToString(), NR_CONTAINER, sistema);

                                            if (Enviado == "true")
                                            {
                                                #region REGISTRA O ENVIO DE EMAIL

                                                demurrage.Atualizar(int.Parse(CONTAGEM_DIAS), IDDEMURRAGE);//ATUALIZA CONTAGEM PARA CONTROLAR ENVIO DO EMAIL E NÃO EMVIAR O MESMO EMAIL DIVERSAS VEZES AO DIA

                                                #endregion
                                                //
                                                lbAviso.Visible = true;
                                                lbAviso.Text = "Email enviado com sucesso";
                                                lbAviso.ForeColor = System.Drawing.Color.Blue;
                                                //
                                                Log objLog = new Log();
                                                objLog.Gravar(string.Empty, "NÚMERO CONTAINER: " + NR_CONTAINER + ", ETA_MAO: " + DateTime.Parse(ETA_MAO).ToString("dd/MM/yyyy") + ", VENCIMENTO: " + DateTime.Parse(VENCIMENTO).ToString("dd/MM/yyyy") + ", Email enviado com sucesso", 1);
                                            }
                                            else
                                            {
                                                lbAviso.Visible = true;
                                                lbAviso.Text = "Erro: " + Enviado;
                                                lbAviso.ForeColor = System.Drawing.Color.Blue;
                                                //
                                                Log objLog = new Log();
                                                objLog.Gravar("CarregaDados()", "Erro: " + Enviado, 1);
                                            }

                                            #endregion
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    gridShipped.Columns.Clear();
                    gridShipped.Rows.Clear();
                    gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA                 

                }

            }
            else
            {
                gridShipped.Columns.Clear();
                gridShipped.Rows.Clear();
                gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA              

            }
            //
            txtData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data
        }

        private void CarregaDadosSpareParts()
        {
            SpareParts spareParts = new SpareParts();
            string date = txtData.Text;
            //
            DateTimeFormatInfo ukDtfi = new CultureInfo("pt-BR", false).DateTimeFormat;
            DateTime SqlDataInicio = new DateTime();
            DateTime.TryParse(date, ukDtfi, DateTimeStyles.None, out SqlDataInicio);
            //
            if (Convert.ToString(SqlDataInicio) == "1/1/0001 00:00:00")
            {
                gridShipped.Rows.Clear();
                segundos = 0;
            }
            //
            List<SpareParts> listaSpareParts = new List<SpareParts>();
            listaSpareParts = spareParts.Consultar_SpareParts();
            int rowCount = listaSpareParts.Count;
            //
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    #region PREENCHE O GRID

                    string IDMATERIAL = listaSpareParts[index].IDMATERIAL;
                    string MATERIAL = listaSpareParts[index].MATERIAL;
                    string MINIMO = listaSpareParts[index].MINIMO;
                    string SUBTOTAL = listaSpareParts[index].SUBTOTAL;
                    //
                    gridShipped.Rows.Add();//adiciona os registros no grid
                    //
                    gridShipped.Rows[index].Cells[0].Value = IDMATERIAL;
                    gridShipped.Rows[index].Cells[1].Value = MATERIAL;
                    gridShipped.Rows[index].Cells[2].Value = MINIMO;
                    gridShipped.Rows[index].Cells[3].Value = SUBTOTAL;
                    //
                    gridShipped.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT do GRID
                    gridShipped.ClearSelection();

                    #endregion
                }

                int RowCountGrid = gridShipped.Rows.Count;
                //
                if (RowCountGrid > 0)
                {
                    for (int index = 0; index < RowCountGrid; index++)
                    {
                        string IDMATERIAL = gridShipped.Rows[index].Cells[0].Value.ToString();
                        string MATERIAL = gridShipped.Rows[index].Cells[1].Value.ToString();
                        string MINIMO = gridShipped.Rows[index].Cells[2].Value.ToString();
                        string SUBTOTAL = gridShipped.Rows[index].Cells[3].Value.ToString();
                        //
                        #region ENVIA EMAIL

                        Email objEmail = new Email();
                        string remetente = objEmail.Remetente();
                        string para = objEmail.Copia();//objEmail.Para();//SOMENTE WH
                        string copia = string.Empty;//objEmail.Copia();
                        string corpo = "";
                        string assunto = "ESTOQUE BAIXO DE MATERIAL [" + MATERIAL + "]";
                        string Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, MATERIAL, objEmail.Smtp(), MINIMO, string.Empty, string.Empty, SUBTOTAL, string.Empty, sistema);

                        if (Enviado == "true")
                        {
                            #region REGISTRA O ENVIO DE EMAIL

                            spareParts.Atualizar(IDMATERIAL);

                            #endregion
                            //
                            lbAviso.Visible = true;
                            lbAviso.Text = "Email enviado com sucesso";
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            //
                            Log objLog = new Log();
                            objLog.Gravar(string.Empty, "MATERIAL: " + MATERIAL + " MINIMO: " + MINIMO + " SUBTOTAL: " + SUBTOTAL + " Email enviado com sucesso", 2);
                        }
                        else
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = "Erro: " + Enviado;
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            //
                            Log objLog = new Log();
                            objLog.Gravar("CarregaDadosSpareParts()", "Erro: " + Enviado, 2);
                        }

                        #endregion
                    }
                }
                else
                {
                    gridShipped.Columns.Clear();
                    gridShipped.Rows.Clear();
                    gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA                  
                }

            }
            else
            {
                gridShipped.Columns.Clear();
                gridShipped.Rows.Clear();
                gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA             
            }
            //
            txtData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data
        }

        private void CarregaDadosSFTP()
        {
            Estacao ftp = new Estacao();
            string date = txtData.Text;
            //
            DateTimeFormatInfo ukDtfi = new CultureInfo("pt-BR", false).DateTimeFormat;
            DateTime SqlDataInicio = new DateTime();
            DateTime.TryParse(date, ukDtfi, DateTimeStyles.None, out SqlDataInicio);
            //
            if (Convert.ToString(SqlDataInicio) == "1/1/0001 00:00:00")
            {
                gridShipped.Rows.Clear();
                segundos = 0;
            }
            //
            List<Pallet> listaPallet = new List<Pallet>();
            listaPallet = ftp.Consultar_Pallet_SFTP(date);
            int rowCount = listaPallet.Count;
            //
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    #region PREENCHE O GRID

                    string PALLET_NO = listaPallet[index].PALLET_NO;
                    string COMPLETEDATE = listaPallet[index].COMPLETEDATE;
                    string MODELO = listaPallet[index].MODELO;
                    int QUANTIDADE = listaPallet[index].TOTAL_PECAS;
                    //
                    gridShipped.Rows.Add();//adiciona os registros no grid
                    //
                    gridShipped.Rows[index].Cells[0].Value = PALLET_NO;
                    gridShipped.Rows[index].Cells[1].Value = MODELO;
                    gridShipped.Rows[index].Cells[2].Value = COMPLETEDATE;
                    gridShipped.Rows[index].Cells[3].Value = QUANTIDADE;
                    //
                    gridShipped.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT do GRID
                    gridShipped.ClearSelection();

                    #endregion
                }

                int RowCountGrid = gridShipped.Rows.Count;
                //
                if (RowCountGrid > 0)
                {
                    string PALLET_NO = string.Empty;
                    string MODELO = string.Empty;
                    string COMPLETEDATE = string.Empty;
                    string QUANTIDADE = string.Empty;
                    string INFORMACAO = string.Empty;

                    for (int index = 0; index < RowCountGrid; index++)
                    {
                        PALLET_NO = gridShipped.Rows[index].Cells[0].Value.ToString();
                        MODELO = gridShipped.Rows[index].Cells[1].Value.ToString();
                        COMPLETEDATE = gridShipped.Rows[index].Cells[2].Value.ToString();
                        QUANTIDADE = gridShipped.Rows[index].Cells[3].Value.ToString();
                        //
                        INFORMACAO += "<br />" + PALLET_NO + " QUANTIDADE: " + QUANTIDADE + " DATA/HORA: " + COMPLETEDATE;

                    }

                    #region ENVIA EMAIL

                    Email objEmail = new Email();
                    string remetente = objEmail.Remetente();
                    string para = objEmail.Copia(); //objEmail.Para();
                    string copia = string.Empty;//objEmail.Copia();
                    string corpo = INFORMACAO;
                    string assunto = "ENVIO DE ARQUIVO Modelo [" + MODELO + "]";
                    string Enviado = objEmail.Enviar(remetente, para, copia, corpo, assunto, string.Empty, objEmail.Smtp(), QUANTIDADE, COMPLETEDATE, MODELO, string.Empty, string.Empty, sistema);

                    if (Enviado == "true")
                    {
                        #region REGISTRA O ENVIO DE EMAIL

                        for (int i = 0; i < listaPallet.Count; i++)
                        {
                            Pallet pallet = new Pallet();
                            PALLET_NO = listaPallet[i].PALLET_NO;
                            QUANTIDADE = listaPallet[i].TOTAL_PECAS.ToString();

                            pallet.RegistraEnvio(PALLET_NO);

                            Log objLog = new Log();
                            objLog.Gravar(string.Empty, "PALLET: " + PALLET_NO + " MODELO: " + MODELO + " QUANTIDADE: " + QUANTIDADE + " Email enviado com sucesso", 0);

                        }

                        lbAviso.Visible = true;
                        lbAviso.Text = "Email enviado com sucesso";
                        lbAviso.ForeColor = System.Drawing.Color.Blue;

                        #endregion
                        //
                        //lbAviso.Visible = true;
                        //lbAviso.Text = "Email enviado com sucesso";
                        //lbAviso.ForeColor = System.Drawing.Color.Blue;
                        //
                        //Log objLog = new Log();
                        //objLog.Gravar(string.Empty, "PALLET: " + PALLET_NO + " MODELO: " + MODELO + " QUANTIDADE: " + QUANTIDADE + " Email enviado com sucesso", 0);
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Erro: " + Enviado;
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        //
                        Log objLog = new Log();
                        objLog.Gravar("CarregaDadosSFTP()", "Erro: " + Enviado, 0);
                    }

                    #endregion
                }
                else
                {
                    gridShipped.Columns.Clear();
                    gridShipped.Rows.Clear();
                    gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA                  
                }

            }
            else
            {
                gridShipped.Columns.Clear();
                gridShipped.Rows.Clear();
                gridShipped.Columns.Add("", "NENHUM REGISTRO ENCONTRADO");//CRIA COLUNA             
            }
            //
            txtData.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            timerCount.Stop();
            timerCount.Start();
            //
            statusConexao = 1;
            //
            if (statusConexao == 1)
            {
                lbtime.Text = segundos.ToString() + ":s";
                segundos = segundos + 1;
                //
                if (segundos == tempo)//5 segundos
                {
                    timerCount.Stop();
                    timerCount.Start();
                    //
                    CriaColunas();
                    //
                    if (rdbFTP.Checked)
                    {
                        rdbDemurrage.Checked = true;//OK
                        rdbFTP.Checked = false;
                        rdbSpareParts.Checked = false;

                    }
                    else if (rdbDemurrage.Checked)
                    {
                        rdbSpareParts.Checked = true;//OK                      
                        rdbFTP.Checked = false;
                        rdbDemurrage.Checked = false;
                    }
                    else if (rdbSpareParts.Checked)
                    {
                        //rdbFTP.Checked = true;//OK
                        //rdbSpareParts.Checked = false;
                        //rdbDemurrage.Checked = false;

                        rdbSFTP.Checked = true;//OK
                        rdbSpareParts.Checked = false;
                        rdbDemurrage.Checked = false;
                    }
                    else if (rdbSFTP.Checked)
                    {
                        rdbFTP.Checked = true;//OK
                        rdbSFTP.Checked = false;
                        rdbSFTP.Checked = false;
                    }
                }

                if (segundos == tempo + 30)//limpa registros após 30 segundos
                {
                    lbAviso.Text = "LIGADO";
                    lbAviso.Visible = true;
                    lbAviso.ForeColor = System.Drawing.Color.Blue;
                    //
                    gridShipped.Columns.Clear();
                    gridShipped.Rows.Clear();
                    //
                    segundos = 0;
                }
            }
            else
            {
                lbAviso.Text = "PARADO";
                lbAviso.Visible = true;
                lbAviso.ForeColor = System.Drawing.Color.Red;
                statusConexao = 0;
                segundos = 0;
                lbtime.Text = "0:s";

            }
        }

        private void btParar_Click(object sender, EventArgs e)
        {

            lbAviso.Text = "PARADO";
            lbAviso.Visible = true;
            lbAviso.ForeColor = System.Drawing.Color.Red;
            timerCount.Stop();
            //
            lbtime.Text = "0:s";
            segundos = 0;
            //
            CriaColunas();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Ligar();
        }
    }
}
