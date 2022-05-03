using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Accounting
{
    /// <summary>
    /// Class for updating records in the base
    /// </summary>
    public partial class DoSmthWithEntity : Window
    {
        AccountingEntity variableEntity = new AccountingEntity();
        public bool IsInsert { get; set; }

        public DoSmthWithEntity(AccountingEntity accountingEntity, bool isInsert)
        {
            InitializeComponent();
            IsInsert = isInsert;
            if (IsInsert)
            {
                this.Title = "Inserting";
                Button_DoSmthWithEntity.Content = "Insert";
            }
            else
            {
                this.Title = "Updating";
                Button_DoSmthWithEntity.Content = "Update";
                variableEntity = accountingEntity;
                TextBox_Name.Text = accountingEntity.Name;
                TextBox_Type.Text = accountingEntity.Type;
                TextBox_Status.Text = accountingEntity.Status;
                TexBox_Progress.Text = accountingEntity.Progress;
            }
        }

        private void Button_DoSmthWithEntity_Click(object sender, RoutedEventArgs e)
        {
            var context = new AccountingDBContext();

            if (IsInsert)
            {
                InsertEntity(context);
            }
            else
            {
                UpdateEntity(context);
            }
        }

        private void UpdateEntity(AccountingDBContext context)
        {
            if (MessageBox.Show("Are you sure, that you want to change this record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var accEntity = context.AccountingEntities
                    .Where(x => x.Id == variableEntity.Id)
                    .FirstOrDefault();

                string newLogRecord = $"{DateTime.Now} [UPDATE] [WAS] Record: ID = {accEntity.Id}, Name = {accEntity.Name}, Status = {accEntity.Status}, Progress = {accEntity.Progress}";
                FileWork.SaveLog(newLogRecord);

                accEntity.Name = TextBox_Name.Text;
                accEntity.Type = TextBox_Type.Text;
                accEntity.Status = TextBox_Status.Text;
                accEntity.Progress = TexBox_Progress.Text;

                context.SaveChanges();
                MessageBox.Show("A record has been successfully changed.", "Updating", MessageBoxButton.OK, MessageBoxImage.Information);
                newLogRecord = $"{DateTime.Now} [UPDATE] [NOW] Record: ID = {accEntity.Id}, Name = {accEntity.Name}, Status = {accEntity.Status}, Progress = {accEntity.Progress}";
                FileWork.SaveLog(newLogRecord);
                Close();
            }
        }

        private void InsertEntity(AccountingDBContext context)
        {
            variableEntity.Name = TextBox_Name.Text;
            variableEntity.Type = TextBox_Type.Text;
            variableEntity.Status = TextBox_Status.Text;
            variableEntity.Progress = TexBox_Progress.Text;
            context.Add(variableEntity);
            context.SaveChanges();
            MessageBox.Show("A new record has been successfully added.", "Inserting", MessageBoxButton.OK, MessageBoxImage.Information);
            string newLogRecord = $"{DateTime.Now} [INSERT] Record: ID = {variableEntity.Id}, Name = {variableEntity.Name}, Status = {variableEntity.Status}, Progress = {variableEntity.Progress}";
            FileWork.SaveLog(newLogRecord);
            Close();
        }
    }
}
