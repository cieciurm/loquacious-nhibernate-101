using System;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Entities
{
    public class NameChange : Entity
    {
        public virtual FundProduct FundProduct { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ValidSince { get; set; }
    }

    public class NameChangeMap : ClassMapping<NameChange>
    {
        public NameChangeMap()
        {
            Id(x => x.Id);
            ManyToOne(x => x.FundProduct, m => m.Column("FundProductId"));
            Property(x => x.Name);
            Property(x => x.ValidSince);
        }
    }
}
