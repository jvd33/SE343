using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KennUTicket.DAL;
using KennUTicket.Models;

namespace KennUTicket.Extensions
{
    public class UserExtensions
    {
       public static User SystemUser()
        {
            using (var db = new TicketContext())
            {
                User sys = (User)db.Users.FirstOrDefault(c => c.Id == "1");
                return sys;
            }
        }
    }
}