using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Classes
{
    public class MySQLDbConnect
    {
        #region Attributes

        private bool _isvalid;
        private string _message;
        private string _stringConnection;
        private MySqlConnection _connection;
        private DataTable _tabela;
        private IList _parametros = new ArrayList();
        private MySqlTransaction _transaction;
        private MySqlCommand _command;

        private string _ip_servidor;
        private string _usuario;
        private string _senha;
        private string _data_base;

        #endregion
        //
        #region Properties

        public string StringConnection
        {
            get { return _stringConnection; }
            set { _stringConnection = value; }
        }

        public MySqlConnection Connection
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

        public MySqlTransaction Transaction
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

        public string IP
        {
            get { return _ip_servidor; }
            set { _ip_servidor = value; }
        }

        public string USUARIO
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string SENHA
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public string DATA_BASE
        {
            get { return _data_base; }
            set { _data_base = value; }
        }

        #endregion
        //
        #region Methods

        public void String_Connection()
        {
            try
            {
                #region IP DO SERVIDOR
                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\MYSQL\CONEXAO.txt";
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
                                    if (indice > 2)
                                    {
                                        _ip_servidor += linha[indice];
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

                //Criptografia objDataBase = new Criptografia();
                //_ip_servidor = objDataBase.Descriptografar(_ip_servidor);

                #endregion
                //
                #region NOME USUARIO
                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\MYSQL\CONEXAO.txt";
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
                                        _usuario += linha[indice];
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

                Criptografia objNomeUsuario = new Criptografia();
                _usuario = objNomeUsuario.Descriptografar(_usuario);

                #endregion
                //
                #region SENHA BASE DE DADOS
                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\MYSQL\CONEXAO.txt";
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

                Criptografia objSenha = new Criptografia();
                _senha = objSenha.Descriptografar(_senha);

                #endregion
                //
                #region DATA BASE
                //
                try
                {
                    string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\MYSQL\CONEXAO.txt";
                    string linha;
                    int row = 0;
                    //
                    if (System.IO.File.Exists(caminho))
                    {
                        System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                        //
                        while ((linha = arqTXT.ReadLine()) != null)
                        {
                            if (row == 3)//terceira linha do .txt
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

                Criptografia objdata_base = new Criptografia();
                _data_base = objdata_base.Descriptografar(_data_base);

                #endregion

            }
            catch (Exception)
            {
                //
            }
        }

        public bool Conectar()
        {
            try
            {
                Criptografia cript = new Criptografia();
                string _connectionString = "server=" + _ip_servidor + ";uid=" + _usuario + ";pwd=" + _senha + ";database=" + _data_base;

                _connection = new MySqlConnection(_connectionString);

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
            MySqlParameter parametro = new MySqlParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Input;
            parametro.Value = valor;

            _parametros.Add(parametro);
        }

        public void AdicionarParametroSaida(string nome, SqlDbType tipo)
        {
            MySqlParameter parametro = new MySqlParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Output;

            _parametros.Add(parametro);
        }

        public void SetarSQL(string SQL)
        {
            _command = new MySqlCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = SQL;
            _command.Connection = _connection;
        }

        public void SetarSP(string nomeSP)
        {
            _command = new MySqlCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = nomeSP;
            _command.Connection = _connection;
        }

        public bool Executar()
        {

            try
            {
                //_command.Parameters.Clear();

                foreach (MySqlParameter parametro in _parametros)
                {
                    _command.Parameters.Add(parametro);

                    //_command.Parameters.AddWithValue(parametro.ToString(), "");
                }

                //_parametros = new ArrayList();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(_command);
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
