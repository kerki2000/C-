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
    /// Interaction logic for UpdateUserAccount.xaml
    /// </summary>
    public partial class UpdateUserAccount : Window
    {
        private BankAppViewModel repozitory = new BankAppViewModel();
        private AdminPanel _adminPanel;
        public UpdateUserAccount(AdminPanel adminPanel)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this._adminPanel = adminPanel;
            LoadForm();
        }

        private void LoadForm()
        {
            User user = _adminPanel.dgUsers.SelectedItem as User;
            txtbId.Text = user.ID.ToString();
            txtbAccountNumber.Text = user.UserAccount.AccountNumber.ToString();
        }
        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewName.Text) || string.IsNullOrEmpty(txtNewPassword.Text))
            {
                MessageBox.Show("New Name or New Password Fields Cannot Be Empty");
            }
            else {

                User user = _adminPanel.dgUsers.SelectedItem as User;
                
                user.Name = txtNewName.Text;
                user.Password = txtNewPassword.Text;

                repozitory.UpdateUser(user);
                this._adminPanel.LoadData();
            }
          

           
            this.Close();
        }
    }
}
