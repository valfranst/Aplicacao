using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DadosFinanceiros
{     
    class MyConfig
    {

        private Configuration configApp;
        private string _conexao;
        private string _local;
        private string _localImagem;
        private string _localImagemAplicativo;
        private string _localBanco;
        private string _localExcel;
        private string _localBancoBackup;
        private string _localExcelBackup;

        private MyConfig()
        {
            this.configApp = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this._conexao = configApp.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString;
            this._local = configApp.AppSettings.Settings["local"].Value;
            this._localImagem = configApp.AppSettings.Settings["localImagem"].Value;
            this._localBanco = configApp.AppSettings.Settings["localBanco"].Value;
            this._localExcel = configApp.AppSettings.Settings["localExcel"].Value;
            this._localBancoBackup = configApp.AppSettings.Settings["localBancoBackup"].Value;
            this._localExcelBackup = configApp.AppSettings.Settings["localExcelBackup"].Value;
            this._localImagemAplicativo = configApp.AppSettings.Settings["localImagemAplicativo"].Value;
        }

        static MyConfig _instancia; 
        public static MyConfig Instancia
        {
            get { return _instancia ?? (_instancia = new MyConfig()); }
        }
        
        #region GET and SET

        public string Conexao
        {
            get => _conexao;
            set
            {
                configApp.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + value + @"\Represa02.mdf;Integrated Security=True;Connect Timeout=30";
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("DefaultConnection");
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

        public string Local
        {
            get => _local;
            set
            {
                _local = value;
                configApp.AppSettings.Settings["local"].Value = _local;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalImagem
        {
            get => _localImagem;
            set
            {
                _localImagem = value;
                configApp.AppSettings.Settings["localImagem"].Value = _localImagem;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalBanco
        {
            get => _localBanco;
            set
            {
                _localBanco = value;
                configApp.AppSettings.Settings["localBanco"].Value = _localBanco;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalExcel
        {
            get => _localExcel;
            set
            {
                _localExcel = value;
                configApp.AppSettings.Settings["localExcel"].Value = _localExcel;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalImagemAplicativo
        {
            get => _localImagemAplicativo;
            set
            {
                _localImagemAplicativo = value;
                configApp.AppSettings.Settings["localImagemAplicativo"].Value = _localImagemAplicativo;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalBancoBackup
        {
            get => _localBancoBackup;
            set
            {
                _localBancoBackup = value;
                configApp.AppSettings.Settings["localBancoBackup"].Value = _localBancoBackup;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public string LocalExcelBackup
        {
            get => _localExcelBackup;
            set
            {
                _localExcelBackup = value;
                configApp.AppSettings.Settings["localExcelBackup"].Value = _localExcelBackup;
                configApp.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        #endregion FIM GET and SET


        #region VERIFIAR DIRETORIOS

        public void reiniciarEstrutura()
        {
            Local = "";
            bool teste = verificarDiretorio();
        }

        public bool verificarDiretorio()
        {
            //bool result = false;            
            //result = Directory.Exists(_local);
            if (!Directory.Exists(LocalBanco))
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                // Set validate names and check file exists to false otherwise windows will
                // not let you select "Folder Selection."
                folderBrowser.ValidateNames = false;
                folderBrowser.CheckFileExists = false;
                folderBrowser.CheckPathExists = true;
                // Always default to Folder Selection.
                folderBrowser.FileName = "Selecione a pasta";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                    Local = folderPath;

                    if (Directory.Exists(Local + @"\Banco"))
                        LocalBanco = folderPath + @"\Banco";
                    if (Directory.Exists(Local + @"\Imagens"))
                        LocalImagem = folderPath + @"\Imagens";
                    if (Directory.Exists(Local + @"\Excel"))
                        LocalExcel = folderPath + @"\Excel";

                    if (Directory.Exists(Local + @"\Imagens_Aplicativo"))
                        LocalImagemAplicativo = folderPath + @"\Imagens_Aplicativo";
                    if (Directory.Exists(Local + @"\Backup_Banco"))
                        LocalBancoBackup = folderPath + @"\Backup_Banco";
                    if (Directory.Exists(Local + @"\Backup_Excel"))
                        LocalExcelBackup = folderPath + @"\Backup_Excel";

                    if (File.Exists(LocalBanco + @"\Represa02.mdf"))
                    {
                        string antiga = configApp.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString;
                        Conexao = LocalBanco;
                        //MessageBox.Show("Original: "+antiga+"\n\n\nAlterada: " + configApp.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString);                          
                        return true;


                    }
                    else
                    {
                        string conexao = Conexao;
                        MessageBox.Show("A ARQUIVO do Banco de Dados não Existe: por favor verofique a EXISTÊNCIA da estrutura do SISTEMA! \n\n" +
                            "Banco Atual: " + conexao);

                        configApp.AppSettings.Settings["local"].Value = null;
                        configApp.AppSettings.Settings["localBanco"].Value = null;
                        configApp.AppSettings.Settings["localExcel"].Value = null;
                        configApp.AppSettings.Settings["localImagem"].Value = null;
                        configApp.AppSettings.Settings["localBancoBackup"].Value = null;
                        configApp.AppSettings.Settings["localExcelBackup"].Value = null;
                        configApp.AppSettings.Settings["localImagemAplicativo"].Value = null;
                        configApp.Save(ConfigurationSaveMode.Modified, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("DefaultConnection");
                        ConfigurationManager.RefreshSection("connectionStrings");
                        return false;  
                    }
                }
            }

            return false;

        }

        #endregion FIM VERIFIAR DIRETORIOS



    }//Fim Class
}
