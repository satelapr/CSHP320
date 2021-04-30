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
using System.Globalization;

namespace HelloWorld
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

        public void OnSubmit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format("Entered Credentials are - UserName : {0} and Password {1}", uxName.Text, uxPassword.Text), "Results");
        }
    }

    public class CredentialMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string userName = values[0].ToString();
            string passWord = values[1].ToString();
            bool buttonEnable = ! (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(passWord));
            return buttonEnable;
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }

}
