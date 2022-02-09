using System;
using System.Windows;

namespace Accounting
{
    /// <summary>
    /// Class for working with files
    /// </summary>
    public class FileWork
    {
        /// <summary>
        /// Changing the backup directory
        /// </summary>
        public static void ChangeBackupDirectory()
        {
            var dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                Properties.Settings settings = new Properties.Settings();
                settings.copyToPath = dialog.FileName;
                settings.Save();
            }
        }

        /// <summary>
        /// Backuping the base in the selected directory
        /// </summary>
        public static void BackupTheBase()
        {
            string path = @".\AccountingDB.db";
            Properties.Settings settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\AccountingDB.db";
            System.IO.FileInfo fileInf = new System.IO.FileInfo(path);
            try
            {
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                    MessageBox.Show("The base has been successfully backed up.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("The base file wasn't found. \nCheck if the «AccountingDB.db» exist in the program directory ", "Backup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Saving all actions with the database to a text file
        /// </summary>
        /// <param name="text">
        /// Text to be saved to a text file
        /// </param>
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

        /// <summary>
        /// Backuping the log in the selected directory
        /// </summary>
        public static void BackupTheLog()
        {
            string path = @".\BaseLog.txt";
            Properties.Settings settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\BaseLog.txt";
            System.IO.FileInfo fileInf = new System.IO.FileInfo(path);

            try
            {
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                    MessageBox.Show("The log has been successfully backed up.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("The log file wasn't found. \nCheck if the «BaseLog.txt» exist in the program directory.", "Backup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
