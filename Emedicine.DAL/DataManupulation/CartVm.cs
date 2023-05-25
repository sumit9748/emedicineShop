

using System.ComponentModel.DataAnnotations;

namespace Emedicine.DAL.DataManupulation
{
    public class CartVm
    {
        public int UserId { set; get; }
        public List<int> Medicines { get; set; }
        public int MedicalShopId { set; get; }
        public int Quantity { set; get; }

    }
    public class CartVM2
    {
        public int UserId { set; get; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public int MedicineId { get; set; }

        public int MedicalShopId { get; set; }
    }
}
