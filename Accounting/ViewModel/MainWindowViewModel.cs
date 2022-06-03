using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.Model.Data.Abstract;
using Accounting.Service;
using ReactiveUI;
using WPF_MVVM_Classes;
using ViewModelBase = Accounting.Service.ViewModelBase;

namespace Accounting.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        #region Variables

        private readonly IRepository<AccountingEntity> _entityRepository;
        private IEnumerable<AccountingEntity> _allAccountingEntities;
        private AccountingEntity _accountingEntity;
        private List<string> _allStatus = new List<string>();
        private List<string> _allType = new List<string>();
        private string _selectedStatus = String.Empty;
        private string _selectedType = String.Empty;

        #endregion

        #region Properties

        public IEnumerable<AccountingEntity> AllAccountingEntities
        {
            get => _allAccountingEntities;
            set => this.RaiseAndSetIfChanged(ref _allAccountingEntities, value);
        }
        public AccountingEntity AccountingEntity
        {
            get => _accountingEntity;
            set => this.RaiseAndSetIfChanged(ref _accountingEntity, value);
        }
        public List<string> AllStatus
        {
            get => _allStatus;
            set => this.RaiseAndSetIfChanged(ref _allStatus, value);
        }
        public List<string> AllType
        {
            get => _allType;
            set => this.RaiseAndSetIfChanged(ref _allType, value);
        }
        public string SelectedStatus
        {
            get => _selectedStatus;
            set => this.RaiseAndSetIfChanged(ref _selectedStatus, value);
        }
        public string SelectedType
        {
            get => _selectedType;
            set => this.RaiseAndSetIfChanged(ref _selectedType, value);
        }

        #endregion

        #region Functions

        public static bool Contains(List<string> source, string toCheck, StringComparison comp)
        {
            bool result = false;
            foreach (var item in source)
            {
                if (item?.IndexOf(toCheck, comp) >= 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        #endregion

        public MainWindowViewModel(IRepository<AccountingEntity> entityRepository)
        {
            _entityRepository = entityRepository;
            _allAccountingEntities = _entityRepository.GetAll();

            AllStatus.Add("All"); AllType.Add("All");
            SelectedStatus = AllStatus[0]; SelectedType = AllType[0];
            foreach (var item in AllAccountingEntities)
            {
                if (!Contains(AllStatus, item.Status, StringComparison.OrdinalIgnoreCase)) 
                    AllStatus.Add(item.Status);
                if (!Contains(AllType, item.Type, StringComparison.OrdinalIgnoreCase))
                    AllType.Add(item.Type);
            }
        }

        #region Commands



        #endregion
    }
}
