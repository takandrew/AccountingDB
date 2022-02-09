using System;
using System.Linq;
using System.Windows;

namespace Accounting
{
    /// <summary>
    /// Class for updating records in the base
    /// </summary>
    public partial class Updating : Window
    {
        AccountingEntity updatingEntity = new AccountingEntity();

        public Updating(AccountingEntity accountingEntity)
        {
            InitializeComponent();
            updatingEntity = accountingEntity;
            TextBox_Name.Text = accountingEntity.Name;
            TextBox_Type.Text = accountingEntity.Type;
            TextBox_Status.Text = accountingEntity.Status;
            TexBox_Progress.Text = accountingEntity.Progress;
        }

        private void Button_UpdateEntity_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure, that you want to change this record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AccountingDBContext context = new AccountingDBContext();

                var accEntity = context.AccountingEntities
                    .Where(x => x.Id == updatingEntity.Id)
                    .FirstOrDefault();

                string newLogRecord = $"{DateTime.Now} [UPDATE] [WAS] Record: ID = {accEntity.Id}, Name = {accEntity.Name}, Status = {accEntity.Status}, Progress = {accEntity.Progress}";
                FileWork.SaveLog(newLogRecord);

                accEntity.Name = TextBox_Name.Text;
                accEntity.Type = TextBox_Type.Text;
                accEntity.Status = TextBox_Status.Text;
                accEntity.Progress = TexBox_Progress.Text;

                context.SaveChanges();
                MessageBox.Show("A record has been successfully changed.", "Updating", MessageBoxButton.OK, MessageBoxImage.Information);
                newLogRecord = $"{DateTime.Now} [UPDATE] [BECAME] Record: ID = {accEntity.Id}, Name = {accEntity.Name}, Status = {accEntity.Status}, Progress = {accEntity.Progress}";
                FileWork.SaveLog(newLogRecord);
                Close();
            }
        }
    }
}
