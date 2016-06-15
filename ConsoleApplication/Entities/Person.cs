using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Entities
{
    public class Person : Entity
    {
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual ISet<Pet> Pets { get; set; }

        public Person()
        {
            // ReSharper disable VirtualMemberCallInContructor
            Pets = new HashSet<Pet>();
            // ReSharper restore VirtualMemberCallInContructor
        }

        public virtual void AddPet(Pet pet)
        {
            Pets.Add(pet);
        }
    }

    public class PersonClassMapping : ClassMapping<Person>
    {
        public PersonClassMapping()
        {
            Id(x => x.Id, c => c.Generator(new IdentityGeneratorDef()));
            Property(x => x.Name);
            Property(x => x.Age);
            //Set(x => x.Pets, m => m.Cascade(Cascade.All), c => c.OneToMany());
            Set(x => x.Pets, m => {}, c => c.OneToMany());
        }

    }
}