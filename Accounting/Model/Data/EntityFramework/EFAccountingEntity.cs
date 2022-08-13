using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.Model.Data.Abstract;

namespace Accounting.Model.Data.EntityFramework
{
    public class EFAccountingEntity : IRepository<AccountingEntity>
    {
        private readonly AccountingDBContext _context;

        public EFAccountingEntity(AccountingDBContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountingEntity> GetAll()
        {
            return _context.AccountingEntities.ToList();
        }

        public IEnumerable<AccountingEntity> GetFiltered(int selectedStatus, int selectedType, string selectedName)
        {
            // TODO: I need to come up with logic of this status/list thing
            //if (selectedStatus == "All")
            //    selectedStatus = "";
            //if (selectedType == "All")
            //    selectedType = "";
            return _context.AccountingEntities.Where(x => x.StatusId == selectedStatus
                                                          && x.TypeId == selectedType
                                                          && x.Name.ToLower().Contains(selectedName.ToLower())).ToList();
        }

        public AccountingEntity GetByID(int id)
        {
            return _context.AccountingEntities.First(x => x.Id == id);
        }

        public void Save(AccountingEntity obj)
        {
            if (obj.Id == 0)
                _context.AccountingEntities.Add(obj);
            else
            {
                var dbEntry = _context.AccountingEntities.FirstOrDefault(m => m.Id == obj.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = obj.Name;
                    dbEntry.TypeId = obj.TypeId;
                    dbEntry.StatusId = obj.StatusId;
                    dbEntry.ProgressCur = obj.ProgressCur;
                    dbEntry.ProgressSum = obj.ProgressSum;
                    dbEntry.ReleaseYear = obj.ReleaseYear;
                    dbEntry.Season = obj.Season;
                    dbEntry.NumberOfRe = obj.NumberOfRe;
                    dbEntry.DateAdded = obj.DateAdded;
                    dbEntry.DateChanged = obj.DateChanged;
                }
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var value = _context.AccountingEntities.Find(id);
            if (value != null)
                _context.AccountingEntities.Remove(value);
            _context.SaveChanges();
        }
    }
}
