﻿using JsMiracle.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace JsMiracle.Dal.Concrete
{
    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userID, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userID, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
