using DadosFinanceiros.Controles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace DadosFinanceiros
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        MyConfig myConfig;
        public MainWindow()
        {
            InitializeComponent();
            myConfig = MyConfig.Instancia;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkDb())
            {
                btTesteBanco.Background = Brushes.Green; 
                //iniciar clientePesquisa
            }
        }

        
        public bool checkDb()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();
                    connection.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar Banco de Dados! \n\n\n" + ex);
                return false; // any error is considered as db connection error for now
            }
        }



        #region Click Butons
        private void btRefinanciamento_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Recebimento_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btProducao_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btClientePesquisar_Click(object sender, RoutedEventArgs e)
        {

        }
        ////***************************************************************
        private void btTesteBanco_Click(object sender, RoutedEventArgs e)
        {
            if (checkDb())
            {
                btTesteBanco.Background = Brushes.Green;
            }
        }
        //***************************************************************
        private void btExemplo_Click(object sender, RoutedEventArgs e)
        {

        }   
        private void btLerDados_Click(object sender, RoutedEventArgs e)
        {

        }  
        private void btConfiguracao_Click(object sender, RoutedEventArgs e)
        {
            Configuracoes conf = new Configuracoes();
            GridMain.Children.Clear();
            GridMain.Children.Add(conf);
        } 
        private void btAddCliente_Click(object sender, RoutedEventArgs e)
        {

        }
        //***************************************************************
        private void txtPesquisa_TextChanged(object sender, RoutedEventArgs e)
        {

        }           



        #endregion

    }  //FIM CLASS
}
