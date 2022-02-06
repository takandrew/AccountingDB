using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ComboBox_Type.SelectedItem = "All";
            ComboBox_Status.SelectedItem = "All";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AccTable_Update(true, true);
        }

        private void AccTable_Update(bool allType, bool allStatus)
        {
            AccountingDBContext dbContext = new AccountingDBContext();
            UpdateComboboxes();
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
            try
            {
                string selected = AccTable.SelectedItem.ToString();
                var tempInd1 = selected.IndexOf("=");
                var tempInd2 = selected.IndexOf(",");
                string tempStr = "";
                for (int i = tempInd1 + 2; i < tempInd2; i++)
                {
                    tempStr += selected[i];
                }
                long selectedId = int.Parse(tempStr);

                AccountingEntity accountingEntity = new AccountingEntity();
                AccountingDBContext dBContext = new AccountingDBContext();
                accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();

                Updating updating = new Updating(accountingEntity);
                updating.ShowDialog();
                AccTable_Update(true, true);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You should select record before updating it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selected = AccTable.SelectedItem.ToString();
                var tempInd1 = selected.IndexOf("=");
                var tempInd2 = selected.IndexOf(",");
                string tempStr = "";
                for (int i = tempInd1 + 2; i < tempInd2; i++)
                {
                    tempStr += selected[i];
                }
                long selectedId = int.Parse(tempStr);

                if (MessageBox.Show($"Are you sure, that you want to delete record with ID = {selectedId}?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AccountingEntity accountingEntity = new AccountingEntity();
                    AccountingDBContext dBContext = new AccountingDBContext();
                    accountingEntity = dBContext.AccountingEntities.Where(x => x.Id == selectedId).FirstOrDefault();
                    dBContext.AccountingEntities.Remove(accountingEntity);
                    dBContext.SaveChanges();
                    AccTable_Update(true, true);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You should select record before deleting it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
