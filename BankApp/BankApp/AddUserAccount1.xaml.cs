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
    /// Interaction logic for AddUserAccount1.xaml
    /// </summary>
    public partial class AddUserAccount1 : Window
    {
        private BankAppViewModel repozitory = new BankAppViewModel();
        private AdminPanel _adminPanel;

        public AddUserAccount1(AdminPanel adminPanel)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this._adminPanel = adminPanel;

        }
        private User GenerateNewUser()
        {
            return new User()
            {
                Name = txtName.Text,
                Password = txtPassword.Text,

                UserAccount = new Account()
               
            };
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
             if(string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            
                MessageBox.Show("Name or Password Fields Cannot Be Empty");

            repozitory.AddNewUser(GenerateNewUser());

            this._adminPanel.LoadData();
            this.Close();
        }
        
    }
    
}
