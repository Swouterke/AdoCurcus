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
using System.Windows.Shapes;
using AdoLibrary;

namespace AdoCurcus
{
    /// <summary>
    /// Interaction logic for SaldoRekeningRaadplegen.xaml
    /// </summary>
    public partial class SaldoRekeningRaadplegen : Window
    {
        public SaldoRekeningRaadplegen()
        {
            InitializeComponent();
        }

        private void saldo_Click(object sender, RoutedEventArgs e)
        {
            var manager = new RekeningenManager();
            try
            {
                waarde.Content = "Uw saldo: " + manager.SaldoRekeningRaadplegen(rekeningnr.Text).ToString("N");
            }
            catch (Exception ex)
            {
                waarde.Content = ex.Message;
            }
        }
    }
}
