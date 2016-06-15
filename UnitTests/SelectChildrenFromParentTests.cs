using ConsoleApplication.Entities;
using NHibernate.Exceptions;
using NUnit.Framework;

namespace UnitTests
{
    public class SelectChildrenFromParentTests : NHibernateBaseTest
    {
        [Test]
        public void SelectChildrenFromParent_ThrowsException()
        {
            Pet petAlias = null;

            Assert.Throws<GenericADOException>(() => Session.QueryOver<Person>()
                .JoinAlias(x => x.Pets, () => petAlias)
                .Select(x => x.Pets)
                .List<Pet>());
        }
    }
}
