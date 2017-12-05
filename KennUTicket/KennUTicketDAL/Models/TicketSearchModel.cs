using KennUTicket.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KennUTicket.Models
{
    public class TicketSearchModel
    {
        public string Category { get; set; }
        public string ProductID { get; set; }
        public string OrderNumber { get; set; }
        public string StatusName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByID { get; set; }
        public int StatusID;
        public string AssignedToName { get; set; }
        public string AssignedToID { get; set; }
        public string Priority { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Strategy { get; set; }
    }

    public static class TicketSearchExtensions
    {
        public static String GetSearchValue(this TicketSearchModel tsm)
        {
            foreach (PropertyInfo prop in tsm.GetType().GetProperties())
            {
                if (prop.GetValue(tsm, null) != null && prop.Name != "Strategy")
                {
                    return prop.Name;
                }
            }
            return null;
        }
        
        public static TicketSearchModel ResolveRef(this TicketSearchModel tsm)
        {
            var searchVal = tsm.GetSearchValue();
            using (var db = new TicketContext())
            {
                switch (searchVal)
                {
                    case "CreatedByName":
                        tsm.CreatedByID = db.Users.FirstOrDefault(c => c.UserName == tsm.CreatedByName).Id;
                        tsm.CreatedByName = null;
                        break;
                    case "StatusName":
                        var stat = db.TicketStatuses.FirstOrDefault(c => c.StatusName.Contains(tsm.StatusName));
                        if (stat != null)
                        {
                            tsm.StatusID = stat.ID;
                            tsm.StatusName = null;
                        }
                        break;
                    case "AssignedToName":
                        tsm.AssignedToID = db.Users.FirstOrDefault(c => c.UserName == tsm.AssignedToName).Id;
                        tsm.AssignedToName = null;
                        break;
                }
            }
            return tsm;

        }
    }


}
