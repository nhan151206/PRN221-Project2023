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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Project2023PRN221.Models;

namespace Project2023PRN221
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private PRN221PROJECTContext context;
        public WindowLogin()
        {
            InitializeComponent();
            context = new PRN221PROJECTContext();
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Equals(String.Empty) || GetPassword().Equals(String.Empty))
            {
                MessageBox.Show("Your username or password cannot be empty", "Please Re Enter it", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var account = context.TblUsers.FirstOrDefault(a => a.Username == txtUserName.Text && a.Pass.ToString() == GetPassword());
                if (account != null)
                {
                    MessageBox.Show("Welcome to our app " + account.Username.ToString(),"Management App", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Your username or password not matched", "Please Re Enter it", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private string GetPassword()
        {
            IntPtr passwordBSTR = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(txtPassword.SecurePassword);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(passwordBSTR);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(passwordBSTR);
            }
        }

    }
}
