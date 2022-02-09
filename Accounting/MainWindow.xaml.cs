using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Accounting
{
    public partial class MainWindow : Window
    {
        

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
            AccTable_Update(true, true);
            ComboBox_Type.SelectedItem = "All";
            ComboBox_Status.SelectedItem = "All";
        }

        private void AccTable_Update(bool allType, bool allStatus)
        {
            AccountingDBContext dbContext = new AccountingDBContext();
            if (allType && allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            select new { AccEntity.Id, AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (allType && !allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Status == ComboBox_Status.SelectedItem.ToString()
            select new { AccEntity.Id, AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (!allType && allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Type == ComboBox_Type.SelectedItem.ToString()
            select new { AccEntity.Id, AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (!allType && !allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Type == ComboBox_Type.SelectedItem.ToString() && AccEntity.Status == ComboBox_Status.SelectedItem.ToString()
            select new { AccEntity.Id, AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            AccTable.Columns[0].Width = DataGridLength.Auto;
            AccTable.Columns[2].Width = DataGridLength.Auto;
            AccTable.Columns[3].Width = DataGridLength.Auto;
            AccTable.Columns[4].Width = DataGridLength.Auto;
            UpdateComboboxes();
        }

        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            bool allType = false, allStatus = false;
            if (ComboBox_Type.SelectedItem.ToString() == "All")
                allType = true;
            if (ComboBox_Status.SelectedItem.ToString() == "All")
                allStatus = true;
            AccTable_Update(allType, allStatus);
        }

        private void Button_Insert_Click(object sender, RoutedEventArgs e)
        {
            Inserting inserting = new Inserting();
            inserting.ShowDialog();
            AccTable_Update(true, true);
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateDelete.Updating(AccTable.SelectedItem.ToString());
            AccTable_Update(true, true);
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            UpdateDelete.Deleting(AccTable.SelectedItem.ToString());
            AccTable_Update(true, true);
        }

        private void Button_BackupBase_Click(object sender, RoutedEventArgs e)
        {
            FileWork.BackupTheBase();
        }

        private void Button_BackupLog_Click(object sender, RoutedEventArgs e)
        {
            FileWork.BackupTheLog();
        }

        private void Button_ChangeBackupDirection_Click(object sender, RoutedEventArgs e)
        {
            FileWork.ChangeBackupDirectory();
        }
    }
}
