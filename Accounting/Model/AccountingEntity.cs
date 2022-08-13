using System;
using System.Collections.Generic;

namespace Accounting
{
    public partial class AccountingEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long TypeId { get; set; }
        public long StatusId { get; set; }
        public long ProgressCur { get; set; }
        public long ProgressSum { get; set; }
        public long? ReleaseYear { get; set; }
        public long? Season { get; set; }
        public long NumberOfRe { get; set; }
        public string? DateAdded { get; set; }
        public string? DateChanged { get; set; }
        public string? Note { get; set; }

        public virtual StatusList Status { get; set; } = null!;
        public virtual TypeList Type { get; set; } = null!;
    }
}
