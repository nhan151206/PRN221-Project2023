using Project2023PRN221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Project2023PRN221
{
    /// <summary>
    /// Interaction logic for WindowProduct.xaml
    /// </summary>
    public partial class WindowProduct : Window
    {
        private PRN221PROJECTContext context;
        public WindowProduct()
        {
            context = new PRN221PROJECTContext();
            InitializeComponent();
            cbProductType.ItemsSource = context.TblMatHangs.Select(a => a.Dvt).ToList();
            txtSearch.ItemsSource = context.TblMatHangs.Where(a => a.Active == true).Select(a => a.TenHang).ToList();
            LoadData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtProductId.Text = String.Empty;
            txtProductName.Text = String.Empty;
            txtProductPrice.Text = String.Empty;
            txtSearch.Text = String.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblMatHang t = context.TblMatHangs.OrderBy(a => a.MaHang).LastOrDefault();
                int newid = Convert.ToInt32(t.MaHang.ToString().Substring(2)) + 1;
                String nid = "TH" + newid;
                var product = new TblMatHang
                {
                    MaHang = nid,
                    TenHang = txtProductName.Text,
                    Dvt = cbProductType.Text,
                    Gia = float.Parse(txtProductPrice.Text),
                    Active = true
                };
                if (product != null)
                {
                    context.TblMatHangs.Add(product);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Add peoduct successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblMatHang c = context.TblMatHangs.FirstOrDefault(item => item.MaHang.Equals(txtProductId.Text));
                if (c != null)
                {
                    c.TenHang = txtProductName.Text;
                    c.TenHang = txtProductName.Text;
                    c.Dvt = cbProductType.Text;
                    c.Gia = float.Parse(txtProductPrice.Text);

                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                        MessageBox.Show("Update product successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblMatHang c = context.TblMatHangs.FirstOrDefault(a => a.MaHang.Equals(txtProductId.Text));
                if (c != null)
                {
                    c.Active = false;

                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                        MessageBox.Show("Delete product successfully");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadData()
        {
            listProduct.ItemsSource = context.TblMatHangs.Where(a => a.Active == true).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var product = context.TblMatHangs.FirstOrDefault(a => a.TenHang.Contains(txtSearch.Text));
            if (product != null)
            {
                txtProductName.Text = product.TenHang;
                txtProductId.Text = product.MaHang.ToString();
                txtProductPrice.Text = product.Gia.ToString();
                cbProductType.Text = product.Dvt;
            }
            else
            {
                MessageBox.Show("The product doesn't exist");
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var type = ((TblMatHang)item).Dvt;

                cbProductType.Text = type;
            }
        }
    }
}
