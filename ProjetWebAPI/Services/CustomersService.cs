using AutoMapper;
using ProjetWebAPI.DAL;
using ProjetWebAPI.Models.DTO;
using ProjetWebAPI.Models.Inputs;
using ProjetWebAPI.Services.Interfaces;

namespace ProjetWebAPI.Services
{
    public class CustomersService : ICustomersService
    {
        private DbFactoryContext _db;

        public CustomersService(DbFactoryContext db)
        {
            _db = db;
        }

        ~CustomersService()
        {
            _db.Dispose();
        }

        public void Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            var customer = ReadOne(id);
            if (customer == null)
                throw new Exception($"L'utilisateur d'id {id} n'existe pas.");
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }

        public List<Customer> ReadAll()
        {
            return _db.Customers.ToList();
        }

        public Customer? ReadOne(int id)
        {
            var customer = _db.Customers.
                 FirstOrDefault(c => c.Id == id);
            if (customer == null)
                throw new Exception("L'id spécifié n'existe pas");
            return customer;
        }
    }
}
