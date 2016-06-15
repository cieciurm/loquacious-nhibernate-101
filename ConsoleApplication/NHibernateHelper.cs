using NHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Data;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using ConsoleApplication.Entities;

namespace ConsoleApplication
{
    public class NHibernateHelper
    {
        public const string ConnectionString = @"Data Source=.\;Initial Catalog=nhibernate-test;Integrated Security=true";

        public static Configuration ConfigureNHibernate()
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;

                db.ConnectionString = ConnectionString;
                db.Timeout = 10;

                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                //db.AutoCommentSql = true;
            });

            var mapping = GetAllMappingsFromAssembly();
            cfg.AddDeserializedMapping(mapping, null);
            SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

            return cfg;
        }

        private static HbmMapping GetMappingsLameWay()
        {
            var mapper = new ModelMapper();

            mapper.AddMapping<FundProductMap>();

            var mapping = mapper.CompileMappingFor(new[] { typeof(FundProduct) });

            return mapping;
        }

        private static HbmMapping GetAllMappingsFromAssembly()
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            return mapping;
        }

        public static void CreateSchema(Configuration cfg)
        {
            var schemaExport = new SchemaExport(cfg);

            schemaExport.Create(false, true);
        }

        public static void Seed(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {
                var person = new Person { Name = "Marek", Age = 16 };

                var transporter1 = new Transporter { Description = "transporterek na lato" };
                var transporter2 = new Transporter { Description = "transporterek na zimę" };

                var pet1 = new Pet { Name = "Pieseł #1" };
                var pet2 = new Pet { Name = "Pieseł #2" };
                var pet3 = new Pet { Name = "Koteł #2" };

                pet1.Transporters.Add(transporter1);
                pet2.Transporters.Add(transporter2);
                pet3.Transporters.Add(transporter1);
                pet3.Transporters.Add(transporter2);

                person.AddPet(pet1);
                person.AddPet(pet2);
                person.AddPet(pet3);

                session.Save(person);

                tx.Commit();
            }
        }
    }
}

