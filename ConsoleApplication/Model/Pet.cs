using Iesi.Collections;
using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Model
{
    public class Pet
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Person Owner { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual ISet<Transporter> Transporters { get; set; }

        public Pet()
        {
            Transporters = new HashedSet<Transporter>();
        }
    }

    public class PetClassMapping : ClassMapping<Pet>
    {
        public PetClassMapping()
        {
            Id(x => x.Id);
            Property(x => x.Name);
            ManyToOne(x => x.Owner);
            Set(x => x.Transporters, c => c.Cascade(Cascade.All), c => c.OneToMany());
        }
    }

}