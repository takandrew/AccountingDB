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
            InitializeComponent();
        }

        public void UpdateComboboxes()
        {
            ComboBox_Type.ItemsSource = Queries.Query_Type();
            ComboBox_Status.ItemsSource = Queries.Query_Status();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NameFilter = "";
            AllType = true;
            AllStatus = true;
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void AccTable_Update(bool allType, bool allStatus, string nameFilter)
        {
            var dbContext = new AccountingDBContext();

            string selectedType = allType ? "" : ComboBox_Type.SelectedItem.ToString();
            string selectedStatus = allStatus ? "" : ComboBox_Status.SelectedItem.ToString();

            var query =
                from accEntity in dbContext.AccountingEntities
                where accEntity.Type.Contains(selectedType) && accEntity.Status.Contains(selectedStatus) && accEntity.Name.Contains(nameFilter)
                select new { accEntity.Id, accEntity.Name, accEntity.Type, accEntity.Status, accEntity.Progress };
            AccTable.ItemsSource = query.ToList();
            AccTable.Columns[0].Width = DataGridLength.Auto;
            AccTable.Columns[2].Width = DataGridLength.Auto;
            AccTable.Columns[3].Width = DataGridLength.Auto;
            AccTable.Columns[4].Width = DataGridLength.Auto;
            UpdateComboboxes();
        }

        private void Filter()
        {
            AllStatus = false; AllType = false;
            if (ComboBox_Type.SelectedItem.ToString() == "All")
                AllType = true;
            if (ComboBox_Status.SelectedItem.ToString() == "All")
                AllStatus = true;
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            var inserting = new Inserting();
            inserting.ShowDialog();
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDelete.Updating(AccTable.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You should select record before updating it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDelete.Deleting(AccTable.SelectedItem.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You should select record before deleting it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void Button_BackupBase_Click(object sender, RoutedEventArgs e)
        {
            FileWork.BackupTheBase();
        }

        private void Button_BackupLog_Click(object sender, RoutedEventArgs e)
        {
            FileWork.BackupTheLog();
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
            FileWork.ApplyTheBaseBackup();
            AccTable_Update(AllType, AllStatus, NameFilter);
        }

        private void Button_ApplyTheLogBackup_Click(object sender, RoutedEventArgs e)
        {
            FileWork.ApplyTheLogBackup();
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
            AccTable_Update(AllType, AllStatus, NameFilter);
        }
    }
}
