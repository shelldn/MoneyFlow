using System;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;
using Ninject;
using Ninject.Activation;

namespace MoneyFlow.Identity
{
    public class NinjectUserManagerFactory
    {
        public static UserManager<Account, int> Create(IContext context)
        {
            var us = context.Kernel
                .Get<IUserStore<Account, Int32>>();

            var mgr = new UserManager<Account, int>(us);

            mgr.UserValidator = new UserValidator<Account, Int32>(mgr)
            {
                // allow spaces & co. in user names
                AllowOnlyAlphanumericUserNames = false
            };

            return mgr;
        }
    }
}