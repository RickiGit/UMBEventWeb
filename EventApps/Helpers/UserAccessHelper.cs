using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class UserAccessHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static List<UserAccess> GetAllUserAccess()
        {
            return entities.UserAccesses.ToList();
        }

        public static UserAccess GetDetailUser(string nip)
        {
            var itemUser = entities.UserAccesses.Where(x => x.NIP == nip).SingleOrDefault();

            if (itemUser.Password.StartsWith("xKWG5"))
            {
                itemUser.Password = EncryptionHelper.Decrypt(itemUser.Password);
            };

            return itemUser;
        }

        public static bool SaveUser(UserAccess userAccess)
        {
            try
            {
                var items = new UserAccess
                {
                    NIP = userAccess.NIP,
                    Name = userAccess.Name,
                    Username = userAccess.Username,
                    Password = EncryptionHelper.Encrypt(userAccess.Password),
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.UserAccesses.Add(items);
                entities.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteEvent(string nip)
        {
            var item = entities.UserAccesses.Where(x => x.NIP == nip).SingleOrDefault();
            if (item != null)
            {
                entities.UserAccesses.Remove(item);
                entities.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool EditUser(UserAccess userAccess)
        {
            try
            {
                var item = entities.UserAccesses.Where(x => x.NIP == userAccess.NIP).SingleOrDefault();
                if (item != null)
                {
                    item.Name = userAccess.Name;
                    item.Username = userAccess.Username;
                    item.Password = EncryptionHelper.Encrypt(userAccess.Password);
                    item.Modified = DateTime.Now;
                    item.ModifiedBy = "System";

                    entities.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}