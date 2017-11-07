using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
namespace KennUTicket.Models
{
    public class User : IdentityUser, IUser
    {

        public User() : base()
        {

        }

        public override string ToString()
        {
            return base.UserName;
        }
    }
}