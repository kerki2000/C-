using BankApp.Models;
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

namespace BankApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public MainWindow _mainWindow;


        private BankAppViewModel repozitory = new BankAppViewModel();
        private User user;
        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this._mainWindow = mainWindow;
            user = new User();



    }
        public void LoginUser()
        {
            int id;
            if (int.TryParse(txtId.Text.ToString(),out id))
                { 
                user = repozitory.Login(id, txtPassword.Password.ToString());

                if ((user == null))
                {
                    MessageBox.Show("Invalid ID or Password");
                    

                }
                else if ((user.UserAccount.AccountNumber == 19) && (user.Password == "admin"))
                {
                    MessageBox.Show("Welcome " + user.Name);
                    this.DialogResult = false;
                    this.Close();
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.ShowDialog();
                    
                   
                }
                else if ((user != null))
                {
                    MessageBox.Show("Welcome " + user.Name);
                    _mainWindow.btnDeposit.IsEnabled = true;
                    _mainWindow.btnWithDraw.IsEnabled = true;
                  
                    _mainWindow.txtAmount.IsEnabled = true;
                    _mainWindow.txtbName.Text = user.Name;
                    _mainWindow.txtbId.Text = user.ID.ToString();
                    _mainWindow.txtBalance.Text = user.UserAccount.Balance.ToString("c2");
                    _mainWindow.btnLogin.IsEnabled = false;
                    _mainWindow.btnLogin.Visibility = Visibility.Hidden;
                    _mainWindow.btnLogOut.IsEnabled = true;
                    _mainWindow.btnLogOut.Visibility = Visibility.Visible;


                    this.DialogResult = true;
                    this.Close();

                   
                }
                else
                    return;

            }
            else
            {
                MessageBox.Show("ID can  only be a number");
            }


        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtPassword.Password.ToString()))
            {
                MessageBox.Show("ID or Password Fields Cannot Be Empty");

            }
            else
            {
                LoginUser();
                   
            }
           
         
        }
    }
}
