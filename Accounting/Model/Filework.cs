using System;
using System.Windows;

namespace Accounting.Model
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
            if (Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialog.IsPlatformSupported)
            {
                if (dialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
                {
                    var settings = new Properties.Settings();
                    settings.copyToPath = dialog.FileName;
                    settings.Save();
                }
            }
            else
            {
                MessageBox.Show("Your system doesn't support this feature", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Backuping the base in the selected directory
        /// </summary>
        public static void BackupTheBase()
        {
            string path = @".\AccountingDB.db";
            var settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\AccountingDB.db";
            var fileInf = new System.IO.FileInfo(path);
            try
            {
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                    MessageBox.Show("The database has been successfully backed up.", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("The database file wasn't found. \nCheck if the «AccountingDB.db» exist in the program directory ", "Backup", MessageBoxButton.OK, MessageBoxImage.Error);
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
                using (var sw = new System.IO.StreamWriter(writePath, true, System.Text.Encoding.Default))
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
        /// Opening the log file
        /// </summary>
        public static void OpenLog()
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var file = System.IO.Path.Combine(directory, "BaseLog.txt");
            var fileInfo = new System.IO.FileInfo(file);
            try
            {
                if (fileInfo.Exists)
                {
                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(file);
                    p.StartInfo.UseShellExecute = true;
                    p.Start();
                }
                else
                    MessageBox.Show("The log file wasn't found. \nCheck if the «BaseLog.txt» exist in the program directory.", "Opening the log", MessageBoxButton.OK, MessageBoxImage.Error);
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
            var settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\BaseLog.txt";
            var fileInf = new System.IO.FileInfo(path);

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

        /// <summary>
        /// Applying the base backup
        /// </summary>
        public static void ApplyTheBaseBackup()
        {
            string path = @".\AccountingDB.db";
            var settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\AccountingDB.db";
            var fileInf = new System.IO.FileInfo(newPath);
            try
            {
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(path, true);
                    MessageBox.Show("The database backup has been successfully applied.", "Applying the backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("The database backup file wasn't found. \nCheck if the backup directory is correct", "Applying the backup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Applying the log backup
        /// </summary>
        public static void ApplyTheLogBackup()
        {
            string path = @".\BaseLog.txt";
            var settings = new Properties.Settings();
            string newPath = settings.copyToPath + @"\BaseLog.txt";
            var fileInf = new System.IO.FileInfo(newPath);
            try
            {
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(path, true);
                    MessageBox.Show("The log backup has been successfully applied.", "Applying the backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("The log backup file wasn't found. \nCheck if the backup directory is correct", "Applying the backup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}