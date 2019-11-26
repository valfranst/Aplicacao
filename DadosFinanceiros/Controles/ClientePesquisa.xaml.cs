using System;
using System.Collections.Generic;
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
    /// Interação lógica para ClientePesquisa.xam
    /// </summary>
    public partial class ClientePesquisa : UserControl
    {
        MainWindow mw;
        string pesquisa = "";
        public ClientePesquisa(MainWindow mw, string pesquisa)
        {
            InitializeComponent();
            this.mw = mw;
            this.pesquisa = pesquisa;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) 
        {
            //
        }

        private void clienteDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item = clienteDataGrid.SelectedItem;
            string id = "";
            int idemprestimo = 0;
            try
            {
                id = (clienteDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                idemprestimo = Convert.ToInt32(id);
            }
            catch (ArgumentOutOfRangeException outOfRange)
            {
                MessageBox.Show("Error: Não foi possivél iniciar os detalhes desse empréstimo!\n\n\n" + outOfRange);
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Error: Não foi possivél iniciar os detalhes desse empréstimo!\n\n\n" + formatException);
            }
            catch (OverflowException overflowException)
            {
                MessageBox.Show("Error: Não foi possivél iniciar os detalhes desse empréstimo!\n\n\n" + overflowException);
            }

            //chamar ClienteView
            Configuracoes conf = new Configuracoes();
            mw.GridMain.Children.Clear();
            mw.GridMain.Children.Add(conf);

        }

    } //FIM CLASS
}
