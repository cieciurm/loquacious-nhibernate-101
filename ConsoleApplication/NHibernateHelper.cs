using System;
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
        public static Configuration ConfigureNHibernate()
        {
            var cfg = new Configuration();
            cfg.SessionFactoryName("BuildIt");

            cfg.DataBaseIntegration(db =>
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

            var mapping = GetAllMappingsFromAssembly();
            cfg.AddDeserializedMapping(mapping, "nhibernate-test");
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
            //new SchemaExport(cfg).Drop(false, true);
            new SchemaExport(cfg).Create(false, true);
        }

        public static void Seed(ISession session)
        {

        }
    }
}

