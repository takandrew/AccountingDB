using System.Windows;

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
