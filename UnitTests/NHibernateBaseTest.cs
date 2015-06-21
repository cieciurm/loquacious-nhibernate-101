using ConsoleApplication;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class NHibernateBaseTest
    {
        protected ISession _session;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            //NHibernateHelper.CreateSchema(cfg);
            var sessionFactory = cfg.BuildSessionFactory();

            _session = sessionFactory.OpenSession();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _session.Dispose();
        }

    }
}