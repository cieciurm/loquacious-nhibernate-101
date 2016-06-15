using ConsoleApplication.Entities;
using NUnit.Framework;

namespace UnitTests
{
    public class Class2 : NHibernateBaseTest
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

            using (var tx = _session.BeginTransaction())
            {
                _session.Save(person);
                _session.Save(pet);

                tx.Commit();
            }
        }

    }
}
