using Emedicine.DAL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.BAL
{
    public interface IMedicineManager
    {
        public Task<IEnumerable<User>> GetAllUser();
        public Task<IEnumerable<Medicalshop>> GetAllGetAllMedicalShop();
        public Task<IEnumerable<Medicine>> GetAllMedicine();
        public Task<IEnumerable<User>> GetOnlineUser();
        public Task<User> GetUser(int id);
        public Task<bool> AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(User user);

    }
}
