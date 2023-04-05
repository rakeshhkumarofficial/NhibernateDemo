using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhibernateDemo.Models;

namespace NhibernateDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly INHibernateSessionFactory _sessionFactory;

        public ProductsController(INHibernateSessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<Product>().List();
            }
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<Product>(id);
            }
        }

        [HttpPost]
        public void Post([FromBody]Product product)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
               
                session.Save(product);
                transaction.Commit();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var existingProduct = session.Get<Product>(id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    session.Update(existingProduct);
                }
                transaction.Commit();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var product = session.Get<Product>(id);
                if (product != null)
                {
                    session.Delete(product);
                }
                transaction.Commit();
            }
        }
    }
}
