using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NhibernateDemo.Models;
using System;
using ISession = NHibernate.ISession;

namespace NhibernateDemo
{
    public interface INHibernateSessionFactory
    {
        ISession OpenSession();
    }

    public class NHibernateSessionFactory : INHibernateSessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.Configure(Path.Combine(Directory.GetCurrentDirectory(),"hibernate.cfg.xml"));
            configuration.AddAssembly(typeof(Product).Assembly);
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
