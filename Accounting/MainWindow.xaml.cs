using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Accounting
{
    public partial class MainWindow : Window
    {
        public bool AllStatus { get; set; }
        public bool AllType { get; set; }
        private string NameFilter { get; set; }

        public MainWindow()
        {
            NameFilter = ""; 
            AllType = true;
            AllStatus = true;
            InitializeComponent();
        }

        public void ComboBox_Update()
        {
            ComboBox_Type.ItemsSource = Queries.Query_Type();
            ComboBox_Status.ItemsSource = Queries.Query_Status();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AccTable_Update();
        }

        private void AccTable_Update()
        {
            var dbContext = new AccountingDBContext();

            string selectedType = AllType ? "" : ComboBox_Type.SelectedItem.ToString();
            string selectedStatus = AllStatus ? "" : ComboBox_Status.SelectedItem.ToString();

            var query =
                from accEntity in dbContext.AccountingEntities
                where accEntity.Type.Contains(selectedType) && accEntity.Status.Contains(selectedStatus) 
                                                            && (accEntity.Name.ToLower().Contains(NameFilter.ToLower()))
                select new { accEntity.Id, accEntity.Name, accEntity.Type, accEntity.Status, accEntity.Progress };
            AccTable.ItemsSource = query.ToList();
            AccTable.Columns[0].Width = DataGridLength.Auto;
            AccTable.Columns[2].Width = DataGridLength.Auto;
            AccTable.Columns[3].Width = DataGridLength.Auto;
            AccTable.Columns[4].Width = DataGridLength.Auto;
            ComboBox_Update();
        }

        private void Filter()
        {
            AllStatus = false; AllType = false;
            if (ComboBox_Type.SelectedItem.ToString() == "All")
                AllType = true;
            if (ComboBox_Status.SelectedItem.ToString() == "All")
                AllStatus = true;
            AccTable_Update();
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            var inserting = new DoSmthWithEntity(null, true);
            inserting.ShowDialog();
            AccTable_Update();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDelete.Updating(AccTable.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must select an entry before updating it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AccTable_Update();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDelete.Deleting(AccTable.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must select an entry before deleting it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AccTable_Update();
        }

        private void Button_BackupBase_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to backup the base?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.BackupTheBase();
            }
        }

        private void Button_BackupLog_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to backup the log?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.BackupTheLog();
            }
        }

        private void Button_ChangeBackupDirectory_Click(object sender, RoutedEventArgs e)
        {
            FileWork.ChangeBackupDirectory();
        }

        private void Button_OpenTheLog_Click(object sender, RoutedEventArgs e)
        {
            FileWork.OpenLog();
        }

        private void Button_ApplyTheBaseBackup_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to apply the base backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.ApplyTheBaseBackup();
                AccTable_Update();
            }
        }

        private void Button_ApplyTheLogBackup_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to apply the log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.ApplyTheLogBackup();
            }
        }

        private void ComboBox_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Type.SelectedItem != null && ComboBox_Status.SelectedItem != null)
                Filter();
        }

        private void ComboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Type.SelectedItem != null && ComboBox_Status.SelectedItem != null) 
                Filter();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameFilter = SearchTextBox.Text;
            AccTable_Update();
        }

        private void GetBackup_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to apply the base and log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.ApplyTheBaseBackup();
                FileWork.ApplyTheLogBackup();
                AccTable_Update();
            }
        }

        private void PostBackup_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure, that you want to backup the base and log?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileWork.BackupTheBase();
                FileWork.BackupTheLog();
                AccTable_Update();
            }
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }
    }
}
