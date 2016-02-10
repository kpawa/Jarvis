using AJKM_phase1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJKM_phase1.Models
{
    public class UserAccountVMRepo
    {
        public IEnumerable<UserAccountVM> GetAll()
        {
            SecurityEntities context = new SecurityEntities();

            var query = context.Accounts.Select(account => new UserAccountVM
            {
                AccountId = account.accountID,
                FirstName = account.firstname,
                LastName = account.lastname,
                Email = account.AspNetUser.Email,
                Password = account.AspNetUser.PasswordHash,
                Username = account.AspNetUser.UserName
            }).ToList();

            return query;
        }

        public UserAccountVM GetOne(string accountId)
        {
            SecurityEntities context = new SecurityEntities();

            var query = context.Accounts.Select(account => new UserAccountVM
            {
                AccountId = account.accountID,
                FirstName = account.firstname,
                LastName = account.lastname,
                Email = account.AspNetUser.Email,
                Password = account.AspNetUser.PasswordHash,
                Username = account.AspNetUser.UserName
            }).Where(account => account.AccountId == accountId).FirstOrDefault();

            return query;
        }
        public bool CreateAccount(string firstName, string lastName, string accountId)
        {
            SecurityEntities context = new SecurityEntities();
            try
            {
                Account account = new Account();
                account.accountID = accountId;
                account.firstname = firstName;
                account.lastname = lastName;
                context.Accounts.Add(account);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}