using System;
using System.Collections.Generic;

namespace Accounting
{
    public partial class StatusList
    {
        public StatusList()
        {
            AccountingEntities = new HashSet<AccountingEntity>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AccountingEntity> AccountingEntities { get; set; }
    }
}
