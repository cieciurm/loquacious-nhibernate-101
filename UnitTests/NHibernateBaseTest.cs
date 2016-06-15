using ConsoleApplication;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class NHibernateBaseTest
    {
        protected ISession Session;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            //NHibernateHelper.CreateSchema(cfg);
            var sessionFactory = cfg.BuildSessionFactory();

            Session = sessionFactory.OpenSession();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Session.Dispose();
        }

    }
}