

using ProjetWebAPI.Models.DTO;

namespace ProjetWebAPI.Services.Interfaces
{
    public interface ICustomersService
    {
        //CRUD
        //Create
        void Create(Customer input);
        //Update
        void Update(Customer input);
        //Delete
        void Delete(int id);
        //Read
        Customer ReadOne(int id);
        List<Customer> ReadAll();
    }
}
