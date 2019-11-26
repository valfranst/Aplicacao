using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DadosFinanceiros.Controles
{
    /// <summary>
    /// Interação lógica para Configuracoes.xam
    /// </summary>
    public partial class Configuracoes : UserControl
    {
        MyConfig myConfig;
        public Configuracoes()
        {
            InitializeComponent();
            myConfig = MyConfig.Instancia;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkDb())
            {
                btTesteBanco.Background = Brushes.Green;
                statusDB.Badge = "Conectado";
            }

            localTextBox.Text = myConfig.Local;
            bancoTextBox.Text = myConfig.LocalBanco;
            excelTextBox.Text = myConfig.LocalExcel;
            imagensTextBox.Text = myConfig.LocalImagem;
            bancoBackupTextBox.Text = myConfig.LocalBancoBackup;
            excelBackupTextBox.Text = myConfig.LocalExcelBackup;
            imagensAplicativoTextBox.Text = myConfig.LocalImagemAplicativo;

            if (File.Exists(myConfig.LocalBanco + "Represa02.mdf"))
            {
                bancoDadosTextBox.Text = myConfig.LocalBanco + "Represa02.mdf";
            }
            if (Directory.Exists(myConfig.LocalImagem))
            {
                contImagensTextBox.Text = "Total de Imagens de Clientes " + Directory.GetFiles(myConfig.LocalImagem, "*", SearchOption.TopDirectoryOnly).Length;
            }
            if (Directory.Exists(myConfig.LocalExcel))
            {
                contPlanilhasTextBox.Text = "Total de Planilhas do Excel " + Directory.GetFiles(myConfig.LocalExcel, "*", SearchOption.TopDirectoryOnly).Length;
            }
        }

        
        public bool checkDb()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar Banco de Dados! \n\n\n" + ex);
                return false; // any error is considered as db connection error for now
            }
        }

        private void btAlterarpastas_Click(object sender, RoutedEventArgs e)
        {
            myConfig.reiniciarEstrutura();
            Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }

        private void btRecarregar_Click(object sender, RoutedEventArgs e)
        {
            bool teste = myConfig.verificarDiretorio();
            Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            //App.Current.MainWindow.Close();
            Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
        }
    }  //fim class
}