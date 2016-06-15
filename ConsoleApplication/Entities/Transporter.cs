using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Entities
{
    public class Transporter : Entity
    {
        public virtual string Description { get; set; }

        public virtual Pet Pet { get; set; }
    }

    public class TransporterMapping : ClassMapping<Transporter>
    {
        public TransporterMapping()
        {
            Id(x => x.Id, c => c.Generator(new IdentityGeneratorDef()));
            Property(x => x.Description);
            ManyToOne(x => x.Pet);
        }
    }
}
