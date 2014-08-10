using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Model;
using NHibernate.Linq;
using NHibernate.Mapping;
using Iesi.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NHibernateHelper.Setup();
            NHibernateHelper.CreateSchema();
            NHibernateHelper.Seed();

            using (var session = NHibernateHelper.SessionFactory.OpenSession())
            {
                var fp1 = session.Query<FundProduct>().Single(x => x.Id == 1);
                var fp2 = session.Query<FundProduct>().Single(x => x.Id == 2);

                var ex = new FundProductExcludedFromConversion {FundProd = fp2, ExcludedFundProd = fp1};

                session.SaveOrUpdate(ex);

                session.Flush();
            }

            Console.ReadLine();
        }
    }
}
