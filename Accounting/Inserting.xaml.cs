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

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для Inserting.xaml
    /// </summary>
    public partial class Inserting : Window
    {
        public Inserting()
        {
            InitializeComponent();
        }

        private void Button_InsertEntity_Click(object sender, RoutedEventArgs e)
        {
            AccountingEntity accountingEntity = new AccountingEntity();
            accountingEntity.Name = TextBox_Name.Text;
            accountingEntity.Type = TextBox_Type.Text;
            accountingEntity.Status = TextBox_Status.Text;
            accountingEntity.Progress = TexBox_Progress.Text;
            AccountingDBContext context = new AccountingDBContext();
            context.Add(accountingEntity);
            context.SaveChanges();
            MessageBox.Show("A new record has been successfully added.", "Inserting", MessageBoxButton.OK, MessageBoxImage.Information);
            string newLogRecord = $"{DateTime.Now} [INSERT] Record: ID = {accountingEntity.Id}, Name = {accountingEntity.Name}, Status = {accountingEntity.Status}, Progress = {accountingEntity.Progress}";
            FileWork.SaveLog(newLogRecord);
            Close();
        }
    }
}
