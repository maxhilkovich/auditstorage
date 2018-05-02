using System;
using System.Collections.Generic;
using System.Linq;
using AuditStorageSystem.Models;
using AuditStorageSystem.Models.Authorization;

namespace AuditStorageSystem.Framework.Authorization
{
    public class AuthorizationUtility
    {
        public static List<User> GetUserFromAuthTable(DatabaseContext dbContext, string userNameWithDomain)
        {
            var actualUserName = GetUserName(userNameWithDomain);
            var actualDomain = GetDomain(userNameWithDomain);

            var results = from users in dbContext.Users
                where users.UserName.Equals(actualUserName) && users.Domain.ToLower().Equals(actualDomain.ToLower())
                select users;

            return results.ToList();
        }

        public static string GetUserName(string userNameWithDomain)
        {
            return userNameWithDomain.Split(new[] { "\\" }, StringSplitOptions.None).Last();
        }

        public static string GetDomain(string userNameWithDomain)
        {
            var list = userNameWithDomain.Split(new [] { "\\"}, StringSplitOptions.None).ToList();
            list.RemoveAt(list.Count - 1);
            return string.Join("\\", list);
        }

        public static Role GetUserRole(DatabaseContext dbContext, User user)
        {
            var results = from role in dbContext.Roles
                          where role.Id == user.RoleId
                          select role;
            return results.First();
        }

        public static Role GetUserRole(DatabaseContext dbContext, string userName)
        {
            var user = GetUserFromAuthTable(dbContext, userName);
            return GetUserRole(dbContext, user.First());
        }

    }
}