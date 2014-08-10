using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Data;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using ConsoleApplication.Model;

namespace ConsoleApplication
{
    public class NHibernateHelper
    {
        public static Configuration NHConfiguration { get; set; }
        public static ISessionFactory SessionFactory { get; set; }

        public static void Setup()
        {
            NHConfiguration = ConfigureNHibernate();
            //var mapping = GetMappingsLameWay();
            var mapping = GetAllMappingsFromAssembly();
            NHConfiguration.AddDeserializedMapping(mapping, "nhibernate-test");
            SchemaMetadataUpdater.QuoteTableAndColumns(NHConfiguration);
            SessionFactory = NHConfiguration.BuildSessionFactory();
        }

        private static Configuration ConfigureNHibernate()
        {
            var configure = new Configuration();
            configure.SessionFactoryName("BuildIt");

            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;

                db.ConnectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=nhibernate-test;Integrated Security=true";
                db.Timeout = 10;

                // enabled for testing
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });


            return configure;
        }

        private static HbmMapping GetMappingsLameWay()
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<FundProductMap>();
            mapper.AddMapping<FundProductExcludedFromConversionMap>();

            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(FundProduct), typeof(FundProductExcludedFromConversion) });

            return mapping;
        }

        private static HbmMapping GetAllMappingsFromAssembly()
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            return mapping;
        }

        public static void CreateSchema()
        {
			new SchemaExport(NHConfiguration).Drop(false, true);
            new SchemaExport(NHConfiguration).Create(false, true);
        }

        public static void Seed()
        {
            using (var session = NHibernateHelper.SessionFactory.OpenSession())
            {
                var fp1 = new FundProduct {Name = "Fundusz #1"};
                var fp2 = new FundProduct {Name = "Fundusz #2"};
                var fp3 = new FundProduct {Name = "Fundusz #3"};
                var fp4 = new FundProduct {Name = "Fundusz #4"};

                var fundProds = new List<FundProduct>(new[] {fp1, fp2, fp3, fp4});

                foreach (var fp in fundProds)
                    session.SaveOrUpdate(fp);

                var ex1 = new FundProductExcludedFromConversion { FundProd = fp1, ExcludedFundProd = fp2 };
                var ex2 = new FundProductExcludedFromConversion { FundProd = fp1, ExcludedFundProd = fp3 };
                var ex3 = new FundProductExcludedFromConversion { FundProd = fp1, ExcludedFundProd = fp4 };

                fp1.ExcludedFromConversion.Add(ex1);
                fp1.ExcludedFromConversion.Add(ex2);
                fp1.ExcludedFromConversion.Add(ex3);

                foreach (var ex in fp1.ExcludedFromConversion)
                    session.SaveOrUpdate(ex);

                session.Flush();
            }
        }
    }
}
