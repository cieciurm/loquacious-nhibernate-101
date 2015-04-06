using System.Linq;
using ConsoleApplication;
using NHibernate;
using NUnit.Framework;
using ConsoleApplication.Model;

namespace UnitTests
{
    public class NHibernateTests
    {
        private ISession _session;

        [Test]
        public void WhenQueryOverListCalledQueryIsEvaluatedInline()
        {
            var result = _session.QueryOver<Person>().Where(x => x.Age > 10).List();

            Assert.NotNull(result);
        }

        [Test]
        public void SelectNPlus1()
        {
            Pet pet = null;

            var personResult = _session
                .QueryOver<Person>()
                .Future();

            foreach (var person in personResult)
            {
                var personPets = person.Pets.ToList();
            }

            Assert.NotNull(personResult);
        }


        [Test]
        public void JoinAliasInlineTest()
        {
            Pet pet = null;

            var personResult = _session
                .QueryOver<Person>()
                .JoinAlias(x => x.Pets, () => pet)
                .List();
            // performs inner join with pets table

            foreach (var person in personResult)
            {
                var personPets = person.Pets.ToList();
            }

            Assert.NotNull(personResult);
        }

        [Test]
        public void FetchTest()
        {
            var personResult = _session
                .QueryOver<Person>()
                .Fetch(x => x.Pets).Eager
                .Fetch(x => x.Pets.First().Transporters).Eager
                .List();


            Assert.NotNull(personResult.First().Pets);
            Assert.NotNull(personResult);
        }

        [Test]
        public void JoinAliasFutureTest()
        {
            Pet pet = null;

            var personResult = _session
                .QueryOver<Person>()
                .JoinAlias(x => x.Pets, () => pet)
                .Future();
            // performs inner join with pets table

            foreach (var person in personResult)
            {
                var personPets = person.Pets.ToList();
            }

            Assert.NotNull(personResult);
        }

        [Test]
        public void joinqueryover_test()
        {
            Pet petAlias = null;
            Transporter transporterAlias = null;

            var result = _session.QueryOver<Person>()
                //.JoinAlias(x => x.Pets, () => petAlias)
                //.JoinAlias(() => petAlias.Transporters, () => transporterAlias)
                //.JoinQueryOver(x => x.Pets, () => petAlias)
                //.JoinQueryOver(x => x.Transporters, () => transporterAlias)
                .List();

            Assert.NotNull(result);
        }

        [SetUp]
        public void SetUp()
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            //NHibernateHelper.CreateSchema(cfg);
            var sessionFactory = cfg.BuildSessionFactory();

            _session = sessionFactory.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
            _session.Dispose();
        }
    }
}
