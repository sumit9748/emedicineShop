
using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
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
        public async Task<Medicalshop> GetMedicalshopById(int id)
        {
            return await _da.medicalShop.GetFirstOrDefaultAsync(x => x.ID == id);
        }
        public async Task<bool> AddMedicine(MedicineVm medicine)
        {
            if (medicine == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                var medicines = await _da.medicine.GetAllAsync();
                if(medicines.Any(m=>m.Name == medicine.Name))
                {
                    return await Task.FromResult(false);
                }
                var med = new Medicine
                {
                    Name = medicine.Name,
                    Manufacturer = medicine.Manufacturer,
                    UnitPrice = medicine.UnitPrice,
                    Discount = medicine.Discount,
                    ExpDate = medicine.ExpDate,
                    ImgUrl = medicine.ImgUrl,
                    Status = medicine.Status,
                    Type = medicine.Type,

                };
                 _da.medicine.AddAsync(med);
                _da.save();
                
                foreach(var id in medicine.MedicalShops)
                {
                    var medicalshop=await _da.medicalShop.GetFirstOrDefaultAsync(c=>c.ID== id);
                    var mItem = new MedicalShopItem
                    {
                        MedicalShopId = id,
                        MedicineId = med.Id,
                        Medicine=med,
                        MedicalShop= medicalshop,
                        Quantity=10,
                    };
                    _da.medicalShopItem.AddAsync(mItem);
                    _da.save();
                }
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
