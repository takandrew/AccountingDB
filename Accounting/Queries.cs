using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public class Queries
    {
        //AccountingDBContext dbContext = new AccountingDBContext();

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
