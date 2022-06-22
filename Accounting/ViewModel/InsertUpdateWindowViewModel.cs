using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using Accounting.Model;
using Accounting.Model.Data.Abstract;
using WPF_MVVM_Classes;

namespace Accounting.ViewModel;

public class InsertUpdateWindowViewModel : ViewModelBase
{
    #region Variables

    private AccountingEntity _accountingEntity;
    private string _entityName;
    private string _entityType;
    private string _entityStatus;
    private string _entityProgress;
    private readonly IRepository<AccountingEntity> _accountingRepository;
    private readonly MainWindowViewModel _viewModelBase;

    #endregion

    #region Properties

    public string EntityName
    {
        get => _entityName;
        set
        {
            _entityName = value;
            OnPropertyChanged();
        }
    }
    public string EntityType
    {
        get => _entityType;
        set
        {
            _entityType = value;
            OnPropertyChanged();
        }
    }
    public string EntityStatus
    {
        get => _entityStatus;
        set
        {
            _entityStatus = value;
            OnPropertyChanged();
        }
    }
    public string EntityProgress
    {
        get => _entityProgress;
        set
        {
            _entityProgress = value;
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

    #endregion

    #region Functions



    #endregion

    public InsertUpdateWindowViewModel(AccountingEntity accountingEntity, IRepository<AccountingEntity> accountingRepository, MainWindowViewModel viewModelBase)
    {
        _accountingRepository = accountingRepository;
        _viewModelBase = viewModelBase;
        _accountingEntity = accountingEntity;
        if (accountingEntity != null)
        {
            EntityName = accountingEntity.Name;
            EntityType = accountingEntity.Type;
            EntityStatus = accountingEntity.Status;
            EntityProgress = accountingEntity.Progress;

            string newLogRecord = $"{DateTime.Now} [UPDATE] [SELECTED] Record: ID = {accountingEntity.Id}, Type = {EntityType}, Name = {EntityName}, Status = {EntityStatus}, Progress = {EntityProgress}";
            FileWork.SaveLog(newLogRecord);
        }
    }

    #region Commands

    public RelayCommand InsertUpdateButtonCommand
    {
        get => new RelayCommand(x =>
        {
            if (_accountingEntity != null)
            {
                AccountingEntity.Name = EntityName;
                AccountingEntity.Type = EntityType;
                AccountingEntity.Status = EntityStatus;
                AccountingEntity.Progress = EntityProgress;
                _accountingRepository.Save(_accountingEntity);
                MessageBox.Show("A record has been successfully updated.", "Updating", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModelBase.UpdateTable();
                string newLogRecord = $"{DateTime.Now} [UPDATE] [UPDATED] Record: ID = {AccountingEntity.Id}, Type = {AccountingEntity.Type}, Name = {AccountingEntity.Name}, Status = {AccountingEntity.Status}, Progress = {AccountingEntity.Progress}";
                FileWork.SaveLog(newLogRecord);

            }
            else
            {
                AccountingEntity = new AccountingEntity();
                AccountingEntity.Name = EntityName;
                AccountingEntity.Type = EntityType;
                AccountingEntity.Status = EntityStatus;
                AccountingEntity.Progress = EntityProgress;
                _accountingRepository.Save(_accountingEntity);
                MessageBox.Show("A record has been successfully inserted.", "Inserting", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModelBase.UpdateTable();
                string newLogRecord = $"{DateTime.Now} [INSERT] Record: ID = {AccountingEntity.Id}, Type = {AccountingEntity.Type}, Name = {AccountingEntity.Name}, Status = {AccountingEntity.Status}, Progress = {AccountingEntity.Progress}";
                FileWork.SaveLog(newLogRecord);
            }

        });
    }

    #endregion
}