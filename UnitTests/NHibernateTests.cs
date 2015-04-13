using System.Linq;
using ConsoleApplication;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using ConsoleApplication.Model;

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

    public class ProjectionTests : NHibernateBaseTest
    {
        [Test]
        public void SimpleProjectionTest()
        {
            var result = _session.QueryOver<Person>()
                                 .Select(x => x.Id)
                                 .List<int>()
                                 ;

            Assert.NotNull(result);
        }

        [Test]
        public void SimpleProjectionTestWithJoinAlias()
        {
            Pet petAlias = null;
            var result = _session.QueryOver<Person>()
                                 .JoinAlias(x => x.Pets, () => petAlias)
                                 .Select(Projections.Property(() => petAlias.Id))
                                 .List<int>()
                                 ;

            Assert.NotNull(result);
        }


    }

    public class NHibernateTests : NHibernateBaseTest
    {
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
        public void UsingBothJoinAliasAndJoinQuerOverAchieveTheSame()
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

        [Test]
        public void GrandChildrenEagerJoin()
        {
            var result1 = _session.QueryOver<Person>()
                .Fetch(x => x.Pets).Eager
                .Fetch(x => x.Pets.First().Transporters).Eager
                .List();
            //FROM Person this_ 
            //left outer join Pet pets2_ on this_.Id=pets2_.Owner 
            //left outer join Transporter transporte3_ on pets2_.Id=transporte3_.Pet

            var result2 = _session.QueryOver<Person>()
                .Fetch(x => x.Pets.First().Transporters).Eager
                .List();
            //FROM Person this_

            Pet petAlias = null;
            Transporter transportAlias = null;
            var result3 = _session.QueryOver<Person>()
                .JoinAlias(x => x.Pets, () => petAlias)
                .JoinAlias(() => petAlias.Transporters, () => transportAlias)
                .List();

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
        }


    }
}
