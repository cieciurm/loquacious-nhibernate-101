using System;
using ConsoleApplication.Model;
using Iesi.Collections.Generic;
using NHibernate.SqlCommand;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cfg = NHibernateHelper.ConfigureNHibernate();
            //NHibernateHelper.CreateSchema(cfg);
            var sessionFactory = cfg.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var person = new Person {Name = "Marek", Age = 16};

                var transporter1 = new Transporter { Description = "transporterek na lato" };
                var transporter2 = new Transporter { Description = "transporterek na zimę" };

                var pet1 = new Pet {Id = 3, Name = "Pieseł #1"};
                var pet2 = new Pet {Id = 4, Name = "Pieseł #2"};
                var pet3 = new Pet {Id = 5, Name = "Koteł #2"};

                pet1.Transporters.Add(transporter1);
                pet2.Transporters.Add(transporter2);
                pet3.Transporters.Add(transporter1);
                pet3.Transporters.Add(transporter2);

                person.Pets.Add(pet1);
                person.Pets.Add(pet2);
                person.Pets.Add(pet3);

                session.Save(person);

                tx.Commit();
            }

            Console.ReadLine();
        }
    }
}