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
    /// "About" window
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            AboutProgram.Text = "It's an app that allows you to edit and store your reading/watching lists in database.";
            AboutAuthor.Text = $"Author: Takandrew <andreikaril@mail.ru>";
            AboutGithub.Text = $"This project on GitHub:\nhttps://github.com/takandrew/AccountingDB";
        }
    }
}
