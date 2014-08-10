using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConsoleApplication.Model
{
    public class FundProductExcludedFromConversion
    {
        public virtual int Id { get; set; }
        public virtual FundProduct FundProd { get; set; }
        public virtual FundProduct ExcludedFundProd { get; set; }
    }

    public class FundProductExcludedFromConversionMap : ClassMapping<FundProductExcludedFromConversion>
    {
        public FundProductExcludedFromConversionMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Native));
            ManyToOne<FundProduct>(x => x.FundProd, m => m.NotNullable(true));
            ManyToOne<FundProduct>(x => x.ExcludedFundProd, m => m.NotNullable(true));
        }
    }
}
