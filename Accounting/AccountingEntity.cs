using System;
using System.Collections.Generic;

namespace Accounting
{
    public partial class AccountingEntity
    {
        public long Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
