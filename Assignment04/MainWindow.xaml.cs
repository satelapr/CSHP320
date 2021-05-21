using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtZip_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool enableButtonFlag = false;
            string input = (sender as TextBox).Text; //1234567
            if (!string.IsNullOrWhiteSpace(txtZip.Text))
            {
                // US Zip Code Validation
                if (Regex.IsMatch(input, @"^(\d{5}|\d{5}-\d{4})$"))
                {
                    enableButtonFlag = true; 
                }
                // Canadian Postal code
                else if (Regex.IsMatch(input, @"^([A-Z][0-9][A-Z][0-9][A-Z][0-9])$"))
                {
                    enableButtonFlag = true;
                }
            }
            btnSubmit.IsEnabled = enableButtonFlag;
        }
    }
}
