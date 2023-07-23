using Microsoft.EntityFrameworkCore;
using Project2023PRN221.Models;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Project2023PRN221
{
    /// <summary>
    /// Interaction logic for WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        private PRN221PROJECTContext context;
        public WindowOrder()
        {
            context = new PRN221PROJECTContext();
            InitializeComponent();
            cbProductName.ItemsSource = context.TblMatHangs.Where(a => a.Active == true).Select(a => a.TenHang).ToList();
            btnOrder.IsEnabled = false;
            btnUpdateOrder.IsEnabled = false;
            btnRemoveOrder.IsEnabled = false;
            LoadData();
        }

        private void LoadData()
        {
            if (txtOrderId.Text != String.Empty)
            {
                var data2 = context.TblChiTietHds.Select(a => new
                {
                    MaHd = a.MaHd,
                    MaKh = a.MaHdNavigation.MaKh,
                    TenHang = a.MaHangNavigation.TenHang,
                    Gia = a.MaHangNavigation.Gia,
                    Soluong = a.Soluong.ToString()
                }).Where(a => a.MaHd == decimal.Parse(txtOrderId.Text)).
                    ToList();
                listOrder.ItemsSource = data2;
            }
            else
            {
                var data = context.TblChiTietHds.Select(a => new
                {
                    MaHd = a.MaHdNavigation.MaHd,
                    MaKh = a.MaHdNavigation.MaKh,
                    TenHang = a.MaHangNavigation.TenHang,
                    Gia = a.MaHangNavigation.Gia,
                    Soluong = a.Soluong.ToString()
                }).ToList();

                listOrder.ItemsSource = data;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerId.Text = String.Empty;
            txtCustomerName.Text = String.Empty;
            txtCustomerAddress.Text = String.Empty;
            txtOrderId.Text = String.Empty;
            txtOrderDate.Text = String.Empty;
            cbProductName.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtQuantity.Text = String.Empty;

            LoadData();
            btnUpdateOrder.IsEnabled = false;
            btnRemoveOrder.IsEnabled = false;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblHoadon p = new TblHoadon
                {
                    MaKh = txtCustomerId.Text,
                    NgayHd = DateTime.Today,
                };
                context.TblHoadons.Add(p);
                if (context.SaveChanges() > 0)
                {
                    btnUpdateOrder.IsEnabled = true;
                    btnRemoveOrder.IsEnabled = true;
                    btnOrder.IsEnabled = false;
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            TblChiTietHd data = context.TblChiTietHds.FirstOrDefault(a => a.MaHd == decimal.Parse(txtOrderId.Text)
         && a.MaHangNavigation.TenHang.Equals(cbProductName.Text));
            if (data != null)
            {
                data.Soluong = Int32.Parse(txtQuantity.Text);
                if (context.SaveChanges() > 0)
                {
                    LoadData();
                }
            }
            else
            {
                try
                {
                    TblChiTietHd p = new TblChiTietHd
                    {
                        MaHd = decimal.Parse(txtOrderId.Text),
                        MaHang = txtProductId.Text.ToString(),
                        Soluong = Int32.Parse(txtQuantity.Text)
                    };
                    context.TblChiTietHds.Add(p);
                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRemoveOrder_Click(object sender, RoutedEventArgs e)
        {
            TblChiTietHd data = context.TblChiTietHds.OrderBy(a => a.MaChiTietHd).LastOrDefault(a => a.MaHd == decimal.Parse(txtOrderId.Text));
            if (data != null)
            {
                context.TblChiTietHds.Remove(data);
                if (context.SaveChanges() > 0)
                {
                    LoadData();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                txtQuantity.Text = item.GetType().GetProperty("Soluong").GetValue(item, null).ToString();
                txtOrderId.Text = item.GetType().GetProperty("MaHd").GetValue(item, null).ToString();
                txtCustomerId.Text = item.GetType().GetProperty("MaKh").GetValue(item, null).ToString();
                cbProductName.Text = item.GetType().GetProperty("TenHang").GetValue(item, null).ToString();
            }
        }

        private void txtCustomerId_TextChanged(object sender, TextChangedEventArgs e)
        {
            var customer = context.TblKhachHangs.FirstOrDefault(a => a.MakH.Equals(txtCustomerId.Text) && a.Active == true);
            if (customer != null)
            {
                txtCustomerName.Text = customer.TenKh.ToString();
                txtCustomerAddress.Text = customer.Diachi.ToString();
            }
        }

        private void cbProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbProductName.Text != String.Empty)
            {
                var product = context.TblMatHangs.FirstOrDefault(a => a.TenHang.Equals(cbProductName.SelectedItem.ToString()) && a.Active == true);
                if (product != null)
                {
                    txtPrice.Text = product.Gia.ToString();
                    txtProductId.Text = product.MaHang.ToString();
                }
            } 
        }

        private void txtOrderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtOrderId.Text != String.Empty)
            {
                var order = context.TblHoadons.FirstOrDefault(a => a.MaHd == decimal.Parse(txtOrderId.Text));
                if (order != null)
                {
                    txtOrderDate.Text = order.NgayHd.ToString();
                    txtCustomerId.Text = order.MaKh.ToString();
                    var data = context.TblKhachHangs.FirstOrDefault(a => a.MakH.Equals(txtCustomerId.Text));
                    if (data != null)
                    {
                        txtCustomerName.Text = data.TenKh.ToString();
                        txtCustomerAddress.Text = data.Diachi.ToString();
                    }

                    btnUpdateOrder.IsEnabled = true;
                    btnRemoveOrder.IsEnabled = true;
                    btnOrder.IsEnabled = false;

                    LoadData();
                }
                else
                {
                    btnOrder.IsEnabled = true;
                    btnUpdateOrder.IsEnabled = false;
                    btnRemoveOrder.IsEnabled = false;
                }
            }
        }
    }
}
