using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting
{
    public class FileWork
    {
        public static void BackupTheBase(string newPath)
        {
            string path = @".\AccountingDB.db";
            System.IO.FileInfo fileInf = new System.IO.FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
            }
            MessageBox.Show("The base has been successfully backed up.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void SaveLog(string text)
        {
            string writePath = @".\BaseLog.txt";

            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void BackupTheLog(string newPath)
        {
            string path = @".\BaseLog.txt";
            System.IO.FileInfo fileInf = new System.IO.FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
            }
            MessageBox.Show("The log has been successfully backed up.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
