using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;
using MoneyFlow.Web.ApiControllers;
using Moq;
using NUnit.Framework;

namespace MoneyFlow.Test.ApiControllers
{
    [TestFixture]
    public class AccountsControllerTest
    {
        #region test_maintenance

        private AccountsController m_ctrl;
        private IUserStore<Account, Int32> m_store;

        [SetUp]
        public void Init()
        {
            var store = Mock.Of<IUserStore<Account, Int32>>();
            var mgr = new UserManager<Account, Int32>(store);

            m_store = store;
            m_ctrl = new AccountsController(mgr);

            SignOut();
        }

        #endregion

        private static IPrincipal CreatePrincipal(string name, int id, params string[] roles)
        {
            var identity = new GenericIdentity(name);
            var principal = new GenericPrincipal(identity, roles);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

            return principal;
        }

        private static void SignIn(Account account)
        {
            Thread.CurrentPrincipal = 
                CreatePrincipal(account.UserName, id: account.Id);
        }

        private static void SignOut()
        {
            Thread.CurrentPrincipal = null;
        }

        [Test, Category("GetCurrent()")]
        public void Should_return_the_current_account_associated_with_the_request()
        {
            #region arrange

            const int id = 108;

            var account = 
                new Account(id, "tester@test.com");

            Mock.Get(m_store)
                .Setup(s => s.FindByIdAsync(id))
                .ReturnsAsync(account);

            SignIn(account);

            #endregion

            // act

            var result = m_ctrl.GetCurrent();

            // assert

            Assert.That(result, Is.SameAs(account));
        }
    }
}