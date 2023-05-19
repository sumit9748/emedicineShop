
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.model;


namespace Emedicine.BAL.MedcineBased
{
    public class MedicineMain : IMedicineMain
    {
        private readonly IDataAccess _da;
        public MedicineMain(IDataAccess da) 
        {
            _da = da;
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicine()
        {
            return await _da.medicine.GetAllAsync();
        }

        

        public async Task<IEnumerable<Medicine>> GetTypeOfMedicine(string type)
        {
            return await _da.medicine.GetAllListAsync(x => x.Type == type);
        }

        public async Task<Medicine> GetMedicineById(int id)
        {
            return await _da.medicine.GetFirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> AddMedicine(Medicine medicine)
        {
            if (medicine == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                IEnumerable<Medicine> medicines = await _da.medicine.GetAllAsync();
                if (medicines.Any(med => med.Id == medicine.Id || med.Name == medicine.Name))
                {
                    return await Task.FromResult(false);
                }
                _da.medicine.AddAsync(medicine);
                _da.save();
                return await Task.FromResult(true);
            }

        }
        public void UpdateMedicine(Medicine medicine)
        {
            _da.medicine.UpdateExisting(medicine);
            _da.save();
        }
        public void DeleteMedicine(Medicine medicine)
        {
            _da.medicine.Remove(medicine);
            _da.save();
        }
        public async Task<IEnumerable<Medicine>> GetMedicalShopItem(int id)
        {
            return await _da.medicine.GetMedicalShopItems(id);
        }
       
    }
}
