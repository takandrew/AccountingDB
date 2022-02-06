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
using System.Windows.Shapes;

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для Updating.xaml
    /// </summary>
    public partial class Updating : Window
    {
        AccountingEntity updatingEntity = new AccountingEntity();

        public Updating(AccountingEntity accountingEntity)
        {
            InitializeComponent();
            updatingEntity = accountingEntity;
            TextBox_Name.Text = accountingEntity.Name;
            TextBox_Type.Text = accountingEntity.Type;
            TextBox_Status.Text = accountingEntity.Status;
            TexBox_Progress.Text = accountingEntity.Progress;
        }

        private void Button_UpdateEntity_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure, that you want to change this record?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AccountingDBContext context = new AccountingDBContext();

                var accEntity = context.AccountingEntities
                    .Where(x => x.Id == updatingEntity.Id)
                    .FirstOrDefault();

                accEntity.Name = TextBox_Name.Text;
                accEntity.Type = TextBox_Type.Text;
                accEntity.Status = TextBox_Status.Text;
                accEntity.Progress = TexBox_Progress.Text;

                context.SaveChanges();
                MessageBox.Show("A record has been successfully changed.", "Updating", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
    }
}
