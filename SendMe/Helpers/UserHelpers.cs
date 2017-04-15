using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SendMe.Helpers
{
    public class UserHelpers
    {
        internal static string CreateUserName(string email)
        {
            int idxAT = email.IndexOf("@");
            int idxPrd = email.IndexOf(".");
            string un1 = email.Substring(0, idxAT);
            un1 = un1.Remove(idxPrd, 1);

            string un2 = email.Substring(idxAT);
            idxPrd = un2.IndexOf(".");
            un2 = un2.Remove(idxPrd);
            idxAT = un2.IndexOf("@");
            un2 = un2.Remove(idxAT, 1);

            return (un1 + un2);
        }
    }
}