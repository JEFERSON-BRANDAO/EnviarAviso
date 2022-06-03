using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace Classes
{
    public class OleDbConnect
    {
        #region Attributes

        private bool _isvalid;
        private string _message;
        private string _stringConnection;
        private OleDbConnection _connection;
        private DataTable _tabela;
        private IList _parametros = new ArrayList();
        private OleDbTransaction _transaction;
        private OleDbCommand _command;

        private string _nome_usuario;
        private string _data_base;
        private string _senha;

        #endregion
        //
        #region Properties

        public string StringConnection
        {
            get { return _stringConnection; }
            set { _stringConnection = value; }
        }

        public OleDbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public DataTable Tabela
        {
            get { return _tabela; }
            set { _tabela = value; }
        }

        public IList Parametros
        {
            get { return _parametros; }
            set { _parametros = value; }
        }

        public OleDbTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public bool Isvalid
        {
            get { return _isvalid; }
            set { _isvalid = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        public string USUARIO
        {
            get { return _nome_usuario; }
            set { _nome_usuario = value; }
        }

        public string DATA_BASE
        {
            get { return _data_base; }
            set { _data_base = value; }
        }

        public string SENHA
        {
            get { return _senha; }
            set { _senha = value; }
        }


        #endregion
        //
        #region Methods

        public void String_Connection() 
        {
            try
            {
                #region NOME DA BASE DE DADOS

                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\ORACLE\CONEXAO.txt";
                    string linha;
                    int row = 0;
                    //
                    if (System.IO.File.Exists(caminho))
                    {
                        System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                        //
                        while ((linha = arqTXT.ReadLine()) != null)
                        {
                            if (row == 0)//primeira linha do .txt
                            {
                                for (int indice = 0; indice < linha.Length; indice++)
                                {
                                    if (indice > 8)
                                    {
                                        _data_base += linha[indice];
                                    }
                                }
                            }
                            //
                            row++;
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

                Criptografia objDescDataBase = new Criptografia();
                _data_base = objDescDataBase.Descriptografar(_data_base);

                #endregion
                //
                #region NOME USUARIO

                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\ORACLE\CONEXAO.txt";
                    string linha;
                    int row = 0;
                    //
                    if (System.IO.File.Exists(caminho))
                    {
                        System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                        //
                        while ((linha = arqTXT.ReadLine()) != null)
                        {
                            if (row == 1)//segunda linha do .txt
                            {
                                for (int indice = 0; indice < linha.Length; indice++)
                                {
                                    if (indice > 4)
                                    {
                                        _nome_usuario += linha[indice];
                                    }
                                }
                            }
                            //
                            row++;
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

                Criptografia objDescNomeUsuario = new Criptografia();
                _nome_usuario = objDescNomeUsuario.Descriptografar(_nome_usuario);

                #endregion
                //
                #region SENHA BASE DE DADOS

                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\ORACLE\CONEXAO.txt";
                    string linha;
                    int row = 0;
                    //
                    if (System.IO.File.Exists(caminho))
                    {
                        System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                        //
                        while ((linha = arqTXT.ReadLine()) != null)
                        {
                            if (row == 2)//terceira linha do .txt
                            {
                                for (int indice = 0; indice < linha.Length; indice++)
                                {
                                    if (indice > 3)
                                    {
                                        _senha += linha[indice];
                                    }
                                }
                            }
                            //
                            row++;
                        }
                        //
                        arqTXT.Close();
                    }

                }
                catch
                {
                    //
                }

                Criptografia objDescSenha = new Criptografia();
                _senha = objDescSenha.Descriptografar(_senha);

                #endregion

            }
            catch (Exception)
            {

            }
        }      

        public bool Conectar()
        {
            try
            {               
                string _connectionString = "Provider=OraOLEDB.Oracle;Data Source=" + _data_base + ";User Id=" + _nome_usuario + ";Password=" + _senha + ";";

                _connection = new OleDbConnection(_connectionString);

                _connection.Open();
                return true;
            }
            catch (Exception erro)
            {
                Message = erro.Message;
                return false;
            }
        }

        public void Desconectar()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception) { }
        }

        public void AdicionarParametro(string nome, object valor, SqlDbType tipo)
        {
            OleDbParameter parametro = new OleDbParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Input;
            parametro.Value = valor;

            _parametros.Add(parametro);
        }

        public void AdicionarParametroSaida(string nome, SqlDbType tipo)
        {
            OleDbParameter parametro = new OleDbParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Output;

            _parametros.Add(parametro);
        }

        public void SetarSQL(string SQL)
        {
            _command = new OleDbCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = SQL;
            _command.Connection = _connection;
        }

        public void SetarSP(string nomeSP)
        {
            _command = new OleDbCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = nomeSP;
            _command.Connection = _connection;
        }

        public bool Executar()
        {

            try
            {
                //_command.Parameters.Clear();

                foreach (OleDbParameter parametro in _parametros)
                {
                    _command.Parameters.Add(parametro);

                    //_command.Parameters.AddWithValue(parametro.ToString(), "");
                }

                //_parametros = new ArrayList();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(_command);
                Tabela = new DataTable();
                dataAdapter.Fill(Tabela);

                _isvalid = true;
                _message = "";

                return true;
            }
            catch (Exception erro)
            {
                _isvalid = false;
                _message = erro.Message;

                return false;
            }
        }


        #endregion
    }
}
