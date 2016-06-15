using System;
using ConsoleApplication.Entities;
using NUnit.Framework;

namespace UnitTests
{
    public class GetLoadTests : NHibernateBaseTest
    {
        [Test]
        public void GetFirstLevelCache()
        {
            var result = _session.Get<Person>(1);
            // SELECT ...
            var result2 = _session.Get<Person>(1);
            // noop

            Assert.NotNull(result);
            Assert.NotNull(result2);
        }

        [Test]
        public void GetWhenEntityDoesNotExist()
        {
            const int unexistingId = 999;

            var result = _session.Get<Person>(unexistingId);
        }


        [Test]
        public void LoadWhenEntityDoesNotExist()
        {
            const int unexistingId = 999;

            var result = _session.Load<Person>(unexistingId);

            Assert.Throws<ArgumentException>(delegate { throw new ArgumentException(); });
        }


    }
}
