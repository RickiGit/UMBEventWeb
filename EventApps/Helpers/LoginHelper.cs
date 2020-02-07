using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class LoginHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static UserAccess CheckUserLogin(string username, string password)
        {
            var passwordEncrypt = EncryptionHelper.Encrypt(password);
            var itemUser = entities.UserAccesses.Where(x => x.Username == username && x.Password == passwordEncrypt).SingleOrDefault();
            return itemUser;
        }

        public UserAccess CheckUserLoginMobile(string username, string password)
        {
            var passwordEncrypt = EncryptionHelper.Encrypt(password);
            var itemUser = entities.UserAccesses.Where(x => x.Username == username && x.Password == passwordEncrypt).SingleOrDefault();
            return itemUser;
        }
    }
}