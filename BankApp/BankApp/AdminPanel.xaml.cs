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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private BankAppViewModel repozitory = new BankAppViewModel();

       
        public AdminPanel()
        {
            
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoadData();


        }
       public void LoadData()
        {
            dgUsers.ItemsSource = repozitory.GetAllUsersData();
            dgUsers.Items.Refresh();

        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserAccount1 addUserAccount = new AddUserAccount1(this);
            addUserAccount.ShowDialog();
            
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
                MessageBox.Show("Please selected an User for Update");
            else
            {
                UpdateUserAccount updateUserAccount = new UpdateUserAccount(this);
                updateUserAccount.ShowDialog();
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null && MessageBox.Show("Are you sure you want to delete?\n" +
               "This will delete the Account records as well", "Confirm Delete"
               , MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                try
                {
                    repozitory.DeleteUserRecord((dgUsers.SelectedItem as User).ID);
                    dgUsers.ItemsSource = repozitory.GetAllUsersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       
    }
}
