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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BankAppViewModel repozitory = new BankAppViewModel();
        User user = new User();
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
           this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           
           
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
           
            loginWindow.ShowDialog();

        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            btnDeposit.IsEnabled = false;
            btnWithDraw.IsEnabled = false;
            txtBalance.IsEnabled = false;
            txtbName.Text = string.Empty;
            txtbId.Text = string.Empty;
            txtBalance.Text = 0.ToString("c2");
            txtAmount.Text = 0.ToString("c2");
            txtAmount.IsEnabled = false;
            btnLogin.IsEnabled = true;
            btnLogin.Visibility = Visibility.Visible;
            btnLogOut.IsEnabled = false;
            btnLogOut.Visibility = Visibility.Hidden;
        }
        public void CheckUserAmount()
        {
            double amount;
            if (double.TryParse(txtAmount.Text.ToString(), out amount))
            {
                user = repozitory.BalanceById(int.Parse(txtbId.Text));

              
            }
            else
            {
                MessageBox.Show("Amount must be a number");
                return;
            }


        }
        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            if (string.IsNullOrEmpty(txtAmount.Text) || (double.TryParse(txtAmount.Text.ToString(), out amount))==false)
            {
                MessageBox.Show("Amount is Empty or Invalid.Enter Valid Amount ");

            }
            else
            {
                CheckUserAmount();
                repozitory.DepositBalance(user, double.Parse(txtAmount.Text));
                txtBalance.Text = repozitory.GetBalance(user).ToString("c2");
                repozitory.DepositTransactionToLogFile(user, "../../../log.txt", double.Parse(txtAmount.Text));


            }
        }

        private void BtnWithDraw_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            if (string.IsNullOrEmpty(txtAmount.Text) || (double.TryParse(txtAmount.Text.ToString(), out amount)) == false)
            {
                MessageBox.Show("Amount is Empty or Invalid.Enter Valid Amount ");

            }
            else
            {
                CheckUserAmount();
                repozitory.WithDrawBalance(user, double.Parse(txtAmount.Text));
                txtBalance.Text = repozitory.GetBalance(user).ToString("c2");
                repozitory.WithDrawTransactionToLogFile(user, "../../../log.txt", double.Parse(txtAmount.Text));


            }
        }
    }
}
