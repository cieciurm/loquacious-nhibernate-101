using System.Security.Cryptography.X509Certificates;
using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Model
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual ISet<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new HashedSet<Pet>();
        }
    }

    public class PersonClassMapping : ClassMapping<Person>
    {
        public PersonClassMapping()
        {
            Id(
                x => x.Id,
                c => c.Generator(new IdentityGeneratorDef())
            );

            Property(x => x.Name);

            Property(x => x.Age);

            Set(x => x.Pets, m => m.Cascade(Cascade.All), c => c.OneToMany());
        }

    }
}