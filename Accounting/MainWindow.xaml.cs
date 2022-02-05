﻿using System;
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
            if (allType && allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            select new { AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (allType && !allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Status == ComboBox_Status.SelectedItem.ToString() 
            select new { AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (!allType && allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Type == ComboBox_Type.SelectedItem.ToString()
            select new { AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
            else if (!allType && !allStatus)
            {
                var query =
            from AccEntity in dbContext.AccountingEntities
            where AccEntity.Type == ComboBox_Type.SelectedItem.ToString() && AccEntity.Status == ComboBox_Status.SelectedItem.ToString()
            select new { AccEntity.Name, AccEntity.Type, AccEntity.Status, AccEntity.Progress };
                AccTable.ItemsSource = query.ToList();
            }
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

        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
