using System;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Model
{
    public class NameChange
    {
        public virtual int Id { get; set; }
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
