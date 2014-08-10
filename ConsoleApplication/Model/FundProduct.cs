using Iesi.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Model
{
    public class FundProduct
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<FundProductExcludedFromConversion> ExcludedFromConversion { get; set; }

        public FundProduct()
        {
            ExcludedFromConversion = new HashedSet<FundProductExcludedFromConversion>();
        }
    }

    public class FundProductMap : ClassMapping<FundProduct>
    {
        public FundProductMap()
        {
            Table("FundProducts"); // custom table name

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Native); // auto Id incremenatation
                m.Column("FundProductId"); // custom Primary Key name
            });
            Property<string>(x => x.Name);
            Set(x => x.ExcludedFromConversion, 
                cp => { }, 
                cr => cr.OneToMany(x => x.Class(typeof(FundProductExcludedFromConversion)))
            );
        }
    }
}