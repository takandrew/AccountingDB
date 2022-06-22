using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_Classes;

namespace Accounting.ViewModel
{
    public class AboutWindowViewModel : ViewModelBase
    {
        #region Variables

        private string _aboutProgram = String.Empty;
        private string _aboutAuthor = String.Empty;
        private string _aboutGithub = String.Empty;

        #endregion

        #region Properties

        public string AboutProgram
        {
            get => _aboutProgram;
            set
            {
                _aboutProgram = value;
                OnPropertyChanged();
            }
        }
        public string AboutAuthor
        {
            get => _aboutAuthor;
            set
            {
                _aboutAuthor = value;
                OnPropertyChanged();
            }
        }
        public string AboutGithub
        {
            get => _aboutGithub;
            set
            {
                _aboutGithub = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public AboutWindowViewModel()
        {
            AboutProgram = "It's an app that allows you to edit and store your reading/watching lists in database.";
            AboutAuthor = $"Author: Takandrew <andreikaril@mail.ru>";
            AboutGithub = $"This project on GitHub:\nhttps://github.com/takandrew/AccountingDB";
        }

        #region Commands



        #endregion
    }
}
