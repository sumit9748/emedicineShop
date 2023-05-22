using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emedicine.DAL.model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
        public int MedicalShopId { get; set; }
        [ForeignKey("MedicalShopId")]
        public virtual Medicalshop Medicicalshop { get; set; }
    }
}
