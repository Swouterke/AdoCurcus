using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {
        public ObservableCollection<Brouwer> brouwersOb = new ObservableCollection<Brouwer>();

        private CollectionViewSource brouwerViewSource;
        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
            textBoxZoeken.Focus();
        }
        private void buttonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
        }
        private void textBoxZoeken_KeyUp(object sender, KeyEventArgs e)
        {

                VulDeGrid();
        }
        private void goToFirstButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToFirst();
            goUpdate();
        }
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToPrevious();
            goUpdate();
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToNext();
            goUpdate();
        }
        private void goToLastButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToLast();
            goUpdate();
        }
        private void goUpdate()
        {
            previousButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            goToFirstButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            nextButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);
            goToLastButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);
            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)
                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                    listBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }
            textBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
            
        }
        private void brouwerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goUpdate();
        }


        //methods
        private void VulDeGrid()
        {
            int totalRowsCount;
            brouwerViewSource = ((CollectionViewSource)(this.FindResource("brouwerViewSource")));
            var manager = new BrouwerManager();          
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            totalRowsCount = brouwersOb.Count();
            labelTotalRowCount.Content = totalRowsCount.ToString();
            brouwerViewSource.Source = brouwersOb;
            goUpdate();
            var nummers = (from b in brouwersOb orderby b.Postcode select b.Postcode.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            comboBoxPostcode.ItemsSource = nummers;
            comboBoxPostcode.SelectedIndex = 0;
        }

        private void comboBoxPostcode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPostcode.SelectedIndex == 0)
                brouwerDataGrid.Items.Filter = null;
            else
                brouwerDataGrid.Items.Filter = new Predicate<object>(PostCodeFilter);
            goUpdate();
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
        }
        public bool PostCodeFilter(object br)
        {
            Brouwer b = br as Brouwer;
            bool result = (b.Postcode == Convert.ToInt16(comboBoxPostcode.SelectedValue));
            return result;
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            int position;
            int.TryParse(textBoxGo.Text, out position);
            if (position > 0 && position <= brouwerDataGrid.Items.Count)
                brouwerViewSource.View.MoveCurrentToPosition(position - 1);
            else
                MessageBox.Show("The input index is not valid");
        }

        private void checkBoxPostcode0_Checked(object sender, RoutedEventArgs e)
        {
            Binding binding1 = BindingOperations.GetBinding(textBoxPostcode, TextBox.TextProperty);

            binding1.ValidationRules.Clear();

            var binding2 = (postcodeColumn as DataGridBoundColumn).Binding as Binding;
            binding2.ValidationRules.Clear();

            brouwerDataGrid.RowValidationRules.Clear();
            switch (checkBoxPostcode0.IsChecked)
            {
                case true:
                    binding1.ValidationRules.Add(new PostcodeRangeRule0());
                    binding2.ValidationRules.Add(new PostcodeRangeRule0());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule0());
                    break;
                case false:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;
                default:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;
            }
        }
    }
}
