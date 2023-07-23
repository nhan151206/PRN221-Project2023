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

namespace Project2023PRN221
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

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            WindowCustomer windowCustomer = new WindowCustomer();
            windowCustomer.Show();
            this.Hide();
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            WindowProduct windowProduct = new WindowProduct();
            windowProduct.Show();
            this.Hide();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            WindowOrder windowOrder = new WindowOrder();
            windowOrder.Show();
            this.Hide();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WindowLogin windowLogin = new WindowLogin();
            windowLogin.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            WindowReport windowReport = new WindowReport();
            windowReport.Show();
            this.Hide();
        }
    }
}
