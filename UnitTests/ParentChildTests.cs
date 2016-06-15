using ConsoleApplication.Entities;
using NUnit.Framework;

namespace UnitTests
{
    public class ParentChildTests : NHibernateBaseTest
    {
        [Test]
        public void test1()
        {
            var person = new Person
            {
                Age = 22,
                Name = "Mateusz"
            };

            var pet = new Pet
            {
                Id = 6,
                Name = "ęęęę"
            };

            person.AddPet(pet);

            using (var tx = Session.BeginTransaction())
            {
                Session.Save(person);
                Session.Save(pet);

                tx.Commit();
            }
        }

    }
}
