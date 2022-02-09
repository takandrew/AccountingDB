using System.Collections.Generic;
using System.Linq;

namespace Accounting
{
    /// <summary>
    /// Class for queries
    /// </summary>
    public class Queries
    {
        /// <summary>
        /// Returns the list of types in the base
        /// </summary>
        public static List<string> Query_Type()
        {
            AccountingDBContext dbContext = new AccountingDBContext();
            var query_Type =
            (from accountingEntity in dbContext.AccountingEntities
            select accountingEntity.Type).Distinct();
            List<string> typeList = new List<string>();
            typeList.Add("All");
            foreach (var type in query_Type)
                typeList.Add(type.ToString());
            return typeList;
        }

        /// <summary>
        /// Returns the list of statuses in the base
        /// </summary>
        public static List<string> Query_Status()
        {
            AccountingDBContext dbContext = new AccountingDBContext();
            var query_Status =
            (from accountingEntity in dbContext.AccountingEntities
            select accountingEntity.Status).Distinct();
            List<string> statusList = new List<string>();
            statusList.Add("All");
            foreach (var status in query_Status)
                statusList.Add(status.ToString());
            return statusList;
        }

    }
}
