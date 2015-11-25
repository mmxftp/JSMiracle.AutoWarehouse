using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace JsMiracle.Dal.Abstract.UP
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userID, string password);

        void CreateUser(string userID, string userName,string password, string email);

        bool ChangePassword(string userID , string oldPassword, string newPassword);
    }
}
