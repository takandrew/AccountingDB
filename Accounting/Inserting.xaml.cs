using System;
using System.Windows;

namespace Accounting
{
    /// <summary>
    /// Class for inserting records in the base
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
