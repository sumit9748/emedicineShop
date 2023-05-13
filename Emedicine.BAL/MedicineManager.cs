using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;

namespace Emedicine.BAL
{
    public class MedicineManager : IMedicineManager
    {
        private readonly IDataAccess _da;
        public MedicineManager(IDataAccess da)
        {
            _da = da;
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _da.user.GetAllAsync();
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicine()
        {
            return await _da.medicine.GetAllAsync();
        }

        public async Task<IEnumerable<Medicalshop>> GetAllGetAllMedicalShop()
        {
            return await _da.medicalShop.GetAllAsync();
        }
        public async Task<IEnumerable<User>> GetOnlineUser()
        {
            return await _da.user.GetAllListAsync(x=>x.Status == "online");
        }
        public async Task<User> GetUser(int id)
        {
            return await _da.user.GetFirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<bool> AddUser(User user)
        {

            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                IEnumerable<User> users = await _da.user.GetAllAsync();
                if(users.Any(users=>users.Id == user.Id|| users.Email == user.Email))
                {
                    return await Task.FromResult(false);
                }
                _da.user.AddAsync(user);
                _da.save();
                return await Task.FromResult(true);
            }
        }
        public void UpdateUser(User user)
        {
              _da.user.UpdateExisting(user);
              _da.save();     
        }
        public void DeleteUser(User user)
        {
            _da.user.Remove(user);
            _da.save();
        }
    }
}