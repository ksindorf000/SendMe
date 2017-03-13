using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendMe.Helpers
{
    public class UserHelpers
    {
        internal static string CreateUserName(string email)
        {
            int idxAT = email.IndexOf("@");
            int idxPeriod = email.IndexOf(".") - 1;

            string un1 = email.Substring(0, idxAT);
            string un2 = email.Substring((idxAT + 1), (idxPeriod - idxAT));

            return (un1 + un2);
        }
    }
}