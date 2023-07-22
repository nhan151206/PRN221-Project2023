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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

namespace Project2023PRN221
{
    /// <summary>
    /// Interaction logic for WindowCustomer.xaml
    /// </summary>
    public partial class WindowCustomer : Window
    {
        private PRN221PROJECTContext context;
        public WindowCustomer()
        {
            context = new PRN221PROJECTContext();
            InitializeComponent();
            txtSearch.ItemsSource = context.TblKhachHangs.Where(a => a.Active == true).Select(a => a.TenKh).ToList();
            LoadData();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadData()
        {
            listCus.ItemsSource = context.TblKhachHangs.Where(a => a.Active == true).ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerId.Text = String.Empty;
            txtCustomerName.Text = String.Empty;
            txtCustomerAddress.Text = String.Empty;
            txtCustomerDob.Text = String.Empty;
            if(rbFemale.IsChecked == true)
            {
                rbFemale.IsChecked = false;
            }
            else
            {
                rbMale.IsChecked = false;
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var gender = ((TblKhachHang)item).Gt;
             
                    if (gender == true)
                    {
                        rbMale.IsChecked = true;
                    }
                    else
                    {
                        rbFemale.IsChecked = true;
                    }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblKhachHang t = context.TblKhachHangs.OrderBy(a => a.MakH).LastOrDefault();
                int newid = Convert.ToInt32(t.MakH.ToString().Substring(2)) + 1;
                String nid = "KH" + newid;
                var customer = new TblKhachHang
                {
                    MakH = nid,
                    TenKh = txtCustomerName.Text,
                    Gt = rbMale.IsChecked == true?true:false,
                    Diachi = txtCustomerAddress.Text,
                    NgaySinh = DateTime.Parse(txtCustomerDob.Text),
                    Active = true
                };
                if(customer != null)
                {
                    context.TblKhachHangs.Add(customer);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Add customer successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            bool gender = true;
            if (rbFemale.IsChecked == true)
            {
                gender = false;
            }
            try
            {
                TblKhachHang c = context.TblKhachHangs.FirstOrDefault(item => item.MakH.Equals(txtCustomerId.Text));
                if (c != null)
                {
                    c.TenKh = txtCustomerName.Text;
                    c.Gt = gender;
                    c.Diachi = txtCustomerAddress.Text;
                    c.NgaySinh = DateTime.Parse(txtCustomerDob.Text);

                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                        MessageBox.Show("Update customer successfully");
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
                TblKhachHang c = context.TblKhachHangs.FirstOrDefault(a => a.MakH.Equals(txtCustomerId.Text));
                if (c != null)
                {
                    c.Active = false;

                    if (context.SaveChanges() > 0)
                    {
                        LoadData();
                        MessageBox.Show("Delete customer successfully");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var customer = context.TblKhachHangs.FirstOrDefault(a => a.TenKh.Contains(txtSearch.Text) || a.MakH.Contains(txtSearch.Text) || a.Diachi.Contains(txtSearch.Text));
            if(customer != null)
            {
                txtCustomerName.Text = customer.TenKh;
                txtCustomerId.Text = customer.MakH.ToString();
                txtCustomerAddress.Text = customer.Diachi;
                txtCustomerDob.Text = customer.NgaySinh.ToString();
                if(customer.Gt == true)
                {
                    rbMale.IsChecked = true;
                }
                else
                {
                    rbFemale.IsChecked = true;
                }
            }
            else
            {
                MessageBox.Show("The customer doesn't exist");
            }
        }
    }
}
