using System;
using System.Linq;
using System.Windows;

namespace Accounting
{
    /// <summary>
    /// Class for updating and deleting records from the base
    /// </summary>
    public static class UpdateDelete
    {
        private static long TakeIDFromAccTable(string selectedRecord)
        {
            string selected = selectedRecord;
            var tempInd1 = selected.IndexOf("=");
            var tempInd2 = selected.IndexOf(",");
            string tempStr = "";
            for (int i = tempInd1 + 2; i < tempInd2; i++)
            {
                tempStr += selected[i];
            }
            return int.Parse(tempStr);
        }

        public static void Updating(string selectedRecord)
        {
            long selectedId = TakeIDFromAccTable(selectedRecord);
            var accountingEntity = new AccountingEntity();
            var dBContext = new AccountingDBContext();
            accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();

            var updating = new Updating(accountingEntity);
            updating.ShowDialog();
        }

        public static void Deleting(string selectedRecord)
        {
            long selectedId = TakeIDFromAccTable(selectedRecord);
            if (MessageBox.Show($"Are you sure, that you want to delete record with ID = {selectedId}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var accountingEntity = new AccountingEntity();
                var dBContext = new AccountingDBContext();
                accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();
                string newLogRecord = $"{DateTime.Now} [DELETE] Record: ID = {accountingEntity.Id}, Name = {accountingEntity.Name}, Status = {accountingEntity.Status}, Progress = {accountingEntity.Progress}";
                FileWork.SaveLog(newLogRecord);
                dBContext.AccountingEntities.Remove(accountingEntity);
                dBContext.SaveChanges();
            }
        }
    }
}
