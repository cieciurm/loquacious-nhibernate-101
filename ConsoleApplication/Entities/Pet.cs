using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Entities
{
    public class Pet : Entity
    {
        public virtual string Name { get; set; }
        public virtual Person Owner { get; set; }
        public virtual ISet<Transporter> Transporters { get; set; }

        public Pet()
        {
            // ReSharper disable VirtualMemberCallInContructor
            Transporters = new HashedSet<Transporter>();
            // ReSharper restore VirtualMemberCallInContructor
        }
    }

    public class PetClassMapping : ClassMapping<Pet>
    {
        public PetClassMapping()
        {
            Id(x => x.Id, x => x.Generator(new IdentityGeneratorDef()));
            Property(x => x.Name);
            ManyToOne(x => x.Owner);
            Set(x => x.Transporters, c => c.Cascade(Cascade.All), c => c.OneToMany());
        }
    }

}