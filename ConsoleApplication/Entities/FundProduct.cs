using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Entities
{
    public class FundProduct : Entity
    {
        public virtual string Name { get; set; }
        public virtual ISet<FundProduct> ExcludedFromConversion { get; set; }

        public FundProduct()
        {
            ExcludedFromConversion = new HashedSet<FundProduct>();
        }
    }

    public class FundProductMap : ClassMapping<FundProduct>
    {
        public FundProductMap()
        {
            Table("FundProducts"); // custom table name

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Native); // auto Id incremenatation
                m.Column("FundProductId"); // custom Primary Key name
            });

            Property(x => x.Name);

            Set(x => x.ExcludedFromConversion, m =>
            {
                m.Table("ExcludedFund");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("FundProdId"));
            }, map => map.ManyToMany(p => p.Column("ExcludedFundProdId")));

        }
    }
}