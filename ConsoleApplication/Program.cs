using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            NHibernateHelper.CreateSchema(cfg);

            var sessionFactory = cfg.BuildSessionFactory();
            var session = sessionFactory.OpenSession();

            NHibernateHelper.Seed(session);

            Console.ReadLine();
        }
    }
}