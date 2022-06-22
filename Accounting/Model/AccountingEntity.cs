namespace Accounting
{
    public partial class AccountingEntity
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Progress { get; set; }
    }
}
