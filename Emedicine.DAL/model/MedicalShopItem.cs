using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.model
{
    public class MedicalShopItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine medicine { get; set; }
        [Required]
        public int? MedicalShopId { get; set; }
        [ForeignKey("MedicalShopId")]
        public virtual Medicalshop medicalshop { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
