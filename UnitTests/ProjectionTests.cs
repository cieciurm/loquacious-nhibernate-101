using ConsoleApplication.Entities;
using NHibernate.Criterion;
using NUnit.Framework;

namespace UnitTests
{
    public class ProjectionTests : NHibernateBaseTest
    {
        [Test]
        public void SimpleProjectionTest()
        {
            var result = Session.QueryOver<Person>()
                .Select(x => x.Id)
                .List<int>()
                ;

            Assert.NotNull(result);
        }

        [Test]
        public void SimpleProjectionTestWithJoinAlias()
        {
            Pet petAlias = null;
            var result = Session.QueryOver<Person>()
                .JoinAlias(x => x.Pets, () => petAlias)
                .Select(Projections.Property(() => petAlias.Id))
                .List<int>()
                ;

            Assert.NotNull(result);
        }


    }
}