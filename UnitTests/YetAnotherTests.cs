using System.Linq;
using ConsoleApplication.Entities;
using NHibernate.SqlCommand;
using NUnit.Framework;

namespace UnitTests
{
    public class YetAnotherTests : NHibernateBaseTest
    {
        [Test]
        public void YetAnotherSimpleTest()
        {
            Pet petAlias = null;
            Transporter transporterAlias = null;

            var person = Session.QueryOver<Person>()
                //.WhereRestrictionOn(x=>x.Name).IsLike("Marek")
                .JoinAlias(x => x.Pets, () => petAlias, JoinType.LeftOuterJoin)
                .JoinAlias(() => petAlias.Transporters, () => transporterAlias, JoinType.LeftOuterJoin)
                .List();

            var firstPetName = person.First().Pets.First().Name;
            var firstPetTransporterDesc = person.First()
                .Pets.First()
                .Transporters.First()
                .Description;

            Assert.NotNull(person);
        }
    }
}
