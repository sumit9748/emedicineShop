using System.ComponentModel.DataAnnotations;

namespace Emedicine.DAL.model
{
    public class Medicine
    {
        [Key]
        public int Id { set; get; }
        [MaxLength(100)]
        [Required]
        public string? Name { set; get; }

        [MaxLength(100)]
        [Required]
        public string? Manufacturer { set; get; }

        [Required]
        public double UnitPrice { set; get; }
        [Required]
        public double Discount { set; get; }
        [Required]
        public DateTime ExpDate { set; get; }
        public string? ImgUrl { set; get; }
        [Required]
        public bool Status { set; get; }
        [Required]
        public string? Type { set; get; }

    }
}
