﻿using System;
using System.Linq;
using System.Windows;

namespace Accounting
{
    /// <summary>
    /// Class for updating and deleting records from the base
    /// </summary>
    public static class UpdateDelete
    {
        private static long TakeIDFromAccTable(string errorMSG, string selectedRecord)
        {
            try
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
            catch (NullReferenceException)
            {
                MessageBox.Show(errorMSG, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }

        public static void Updating(string selectedRecord)
        {
            long selectedId = TakeIDFromAccTable("You should select record before updating it.", selectedRecord);
            if (selectedId != -1)
            {
                AccountingEntity accountingEntity = new AccountingEntity();
                AccountingDBContext dBContext = new AccountingDBContext();
                accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();

                Updating updating = new Updating(accountingEntity);
                updating.ShowDialog();
            }
        }

        public static void Deleting(string selectedRecord)
        {
            long selectedId = TakeIDFromAccTable("You should select record before deleting it.", selectedRecord);
            if (selectedId != -1)
            {
                if (MessageBox.Show($"Are you sure, that you want to delete record with ID = {selectedId}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AccountingEntity accountingEntity = new AccountingEntity();
                    AccountingDBContext dBContext = new AccountingDBContext();
                    accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();
                    string newLogRecord = $"{DateTime.Now} [DELETE] Record: ID = {accountingEntity.Id}, Name = {accountingEntity.Name}, Status = {accountingEntity.Status}, Progress = {accountingEntity.Progress}";
                    FileWork.SaveLog(newLogRecord);
                    dBContext.AccountingEntities.Remove(accountingEntity);
                    dBContext.SaveChanges();
                }
            }
        }
    }
}