using Politeh.BLL;
using Politeh.BLL.Model;
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

namespace Politeh.WpfApp
{
    namespace Syste.WPF
    {

    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        ServiceClient service;
        private readonly string path = @"C:\Temp2\Politeh.db";
        public AuthWindow()
        {
            service = new ServiceClient(path);
            InitializeComponent();
        }
        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            ClientDTO client = new ClientDTO();
            client.Email = tbxEmail.Text;
            client.Password = pwdPassword.Password;
            service.RegisterDelegate(ShowMessage);
            client = service.AuthorizationClient(client);
            
            MainWindow mw = new MainWindow(client);
            mw.Show();

            this.Close();
        }
        // public delegate void DelegateEx(bool IsError, string ErrorMessage);

        public void ShowMessage(bool IsError, string ErrorMessage)
        {
            if (IsError)
            {
                MessageBox.Show(ErrorMessage, "IsError", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
