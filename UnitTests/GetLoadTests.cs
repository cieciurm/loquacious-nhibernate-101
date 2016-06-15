using System;
using ConsoleApplication.Entities;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class GetLoadTests : NHibernateBaseTest
    {
        [Test]
        public void GetFirstLevelCache()
        {
            var result = Session.Get<Person>(1);
            // SELECT ...
            var result2 = Session.Get<Person>(1);
            // noop

            Assert.NotNull(result);
            Assert.NotNull(result2);
        }

        [Test]
        public void GetWhenEntityDoesNotExist()
        {
            const int unexistingId = 999;

            var result = Session.Get<Person>(unexistingId);

            Assert.Null(result);
        }


        [Test]
        public void LoadWhenEntityDoesNotExist()
        {
            const int unexistingId = 999;

            var result = Session.Load<Person>(unexistingId);


            Assert.Throws<ObjectNotFoundException>(() =>
            {
                var age = result.Age;
            });
        }
    }
}
