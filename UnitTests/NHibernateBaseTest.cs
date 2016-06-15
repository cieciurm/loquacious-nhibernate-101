using ConsoleApplication;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class NHibernateBaseTest
    {
        protected ISession Session;

        [OneTimeSetUp]
        public void SetUp()
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            //NHibernateHelper.CreateSchema(cfg);
            var sessionFactory = cfg.BuildSessionFactory();

            Session = sessionFactory.OpenSession();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Session.Dispose();
        }

    }
}