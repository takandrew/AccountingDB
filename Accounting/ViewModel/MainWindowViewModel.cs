using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Accounting.Model;
using Accounting.Model.Data.Abstract;
using WPF_MVVM_Classes;

namespace Accounting.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Variables

        private readonly IRepository<AccountingEntity> _entityRepository;
        private IEnumerable<AccountingEntity> _allAccountingEntities;
        private AccountingEntity _accountingEntity;
        private IEnumerable<string> _allStatus;
        private IEnumerable<string> _allType;
        private int _selectedStatus;
        private int _selectedType;
        private string _selectedName = String.Empty;
        private AccountingEntity _selectedEntity;
        private bool _isFirstLaunch = true;

        #endregion

        #region Properties

        public IEnumerable<AccountingEntity> AllAccountingEntities
        {
            get => _allAccountingEntities;
            set
            {
                _allAccountingEntities = value;
                OnPropertyChanged();
            }
        }
        public AccountingEntity AccountingEntity
        {
            get => _accountingEntity;
            set
            {
                _accountingEntity = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<string> AllStatus
        {
            get => _allStatus;
            set
            {
                _allStatus = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<string> AllType
        {
            get => _allType;
            set
            {
                _allType = value;
                OnPropertyChanged();
            }
        }
        public int SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
                if (!_isFirstLaunch)
                    FilterTable();
            }
        }
        public int SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
                if (!_isFirstLaunch)
                    FilterTable();
            }
        }
        public string SelectedName
        {
            get => _selectedName;
            set
            {
                _selectedName = value;
                OnPropertyChanged();
                if (!_isFirstLaunch)
                    FilterTable();
            }
        }
        public AccountingEntity SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                _selectedEntity = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Functions

        public static bool Contains(List<string> source, string toCheck, StringComparison comp)
        {
            foreach (var item in source)
            {
                if (toCheck != null && item?.IndexOf(toCheck, comp) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateTable()
        {
            AllAccountingEntities = _entityRepository.GetAll();

            //UpdateComboBoxes(out var statusList, out var typeList);
            //AllStatus = statusList;
            //AllType = typeList;

            _isFirstLaunch = false;
        }

        private void FilterTable()
        {
            AllAccountingEntities = _entityRepository.GetFiltered(SelectedStatus, SelectedType, SelectedName);
        }

        private void UpdateComboBoxes(/*out List<string> statusList, out List<string> typeList*/)
        {
            //TODO: New logic of status/types lists
            //statusList = new List<string>();
            //typeList = new List<string>();
            //statusList.Clear(); typeList.Clear();
            //statusList.Add("All"); typeList.Add("All");
            //SelectedStatus = statusList[0]; SelectedType = typeList[0];
            //foreach (var item in AllAccountingEntities)
            //{
            //    if (!Contains(statusList, item.Status, StringComparison.OrdinalIgnoreCase))
            //        statusList.Add(item.Status);
            //    if (!Contains(typeList, item.Type, StringComparison.OrdinalIgnoreCase))
            //        typeList.Add(item.Type);
            //}
        }

        #endregion

        public MainWindowViewModel(IRepository<AccountingEntity> entityRepository)
        {
            _entityRepository = entityRepository;
            UpdateTable();
        }

        #region Commands

        public RelayCommand AboutCommand
        {
            get => new RelayCommand(x =>
            {
                var vm = new AboutWindowViewModel();
                RegisterWindow(new AboutWindow(), vm, "About");
                var win = ChildWindows[vm];
                win.Show();
            });
        }

        public RelayCommand InsertCommand
        {
            get => new RelayCommand(x =>
            {
                var vm = new InsertUpdateWindowViewModel(null, _entityRepository, this);
                RegisterWindow(new InsertUpdateWindow(), vm, "Inserting");
                var win = ChildWindows[vm];
                win.Show();
            });
        }

        public RelayCommand UpdateCommand
        {
            get => new RelayCommand(x =>
            {
                if (SelectedEntity != null)
                {
                    var vm = new InsertUpdateWindowViewModel(SelectedEntity, _entityRepository, this);
                    RegisterWindow(new InsertUpdateWindow(), vm, "Updating");
                    var win = ChildWindows[vm];
                    win.Show();
                }
                else
                {
                    MessageBox.Show("You must select an entry before updating it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public RelayCommand DeleteCommand
        {
            get => new RelayCommand(x =>
            {
                if (SelectedEntity != null)
                {
                    if (MessageBox.Show($"Are you sure, that you want to delete record with ID = {SelectedEntity.Id}?",
                            "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        //TODO: New logic of deleting
                        //_entityRepository.Delete(SelectedEntity.Id);
                        //string newLogRecord = $"{DateTime.Now} [DELETE] Record: ID = {SelectedEntity.Id}, Type = {SelectedEntity.Type}, Name = {SelectedEntity.Name}, Status = {SelectedEntity.Status}, Progress = {SelectedEntity.Progress}";
                        //FileWork.SaveLog(newLogRecord);
                    }
                }
                else
                    MessageBox.Show("You must select an entry before deleting it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdateTable();
            });
        }

        public RelayCommand OpenTheLogCommand
        {
            get => new RelayCommand(x =>
            {
                FileWork.OpenLog();
            });
        }

        public RelayCommand ChangeBackupDirectoryCommand
        {
            get => new RelayCommand(x =>
            {
                FileWork.ChangeBackupDirectory();
            });
        }

        public RelayCommand GetDBLogBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to get the database and log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.ApplyTheBaseBackup();
                    FileWork.ApplyTheLogBackup();
                    UpdateTable();
                }
            });
        }

        public RelayCommand GetDBBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to get the database backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.ApplyTheBaseBackup();
                    UpdateTable();
                }
            });
        }

        public RelayCommand GetLogBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to get the log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.ApplyTheLogBackup();
                }
            });
        }

        public RelayCommand PostDBLogBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to post the database and log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.BackupTheBase();
                    FileWork.BackupTheLog();
                }
            });
        }

        public RelayCommand PostDBBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to post the database backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.BackupTheBase();
                }
            });
        }

        public RelayCommand PostLogBackup
        {
            get => new RelayCommand(x =>
            {
                if (MessageBox.Show($"Are you sure, that you want to post the log backup?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FileWork.BackupTheLog();
                }
            });
        }

        #endregion
    }
}
