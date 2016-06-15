using ConsoleApplication.Entities;
using NHibernate.Criterion;
using NUnit.Framework;

namespace UnitTests
{
    public class EagerJoinTests : NHibernateBaseTest
    {
        [Test]
        public void test1()
        {
            Pet petAlias = null;

            var result = Session.QueryOver(() => petAlias)
                .WithSubquery.WhereExists(
                    QueryOver.Of<Transporter>()
                    .Where(x => x.Pet.Id == petAlias.Id)
                    .Select(x => x.Id)
                ).List();
        }

        [Test]
        public void test2()
        {
            Transporter transporterAlias = null;

            var res = Session.QueryOver<Pet>()
                .JoinAlias(x => x.Transporters, () => transporterAlias)
                .WhereRestrictionOn(() => transporterAlias.Description).IsLike("zimę", MatchMode.Anywhere)
                .SingleOrDefault();
        }

        [Test]
        public void test3()
        {
            Transporter transporterAlias = null;

            var res = Session.QueryOver<Pet>()
                .JoinAlias(x => x.Transporters, () => transporterAlias)
                .Select(x=>x.Transporters)
                .List<Transporter>();
        }
    }
}