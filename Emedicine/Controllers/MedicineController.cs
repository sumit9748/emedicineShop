

using Emedicine.BAL.MedcineBased;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineMain md;
        public MedicineController(IMedicineMain _md)
        {
            md = _md;
        }


        [HttpGet("medicalShopItems/{medicalshopid}")]
        public Task<IEnumerable<Medicine>> GetMedicalShopMedicines(int medicalshopid)
        {
            try
            {
                return md.GetMedicalShopItem(medicalshopid);
            }
            catch (Exception ex)
            {
                IEnumerable<Medicine> medicines = new List<Medicine>();
                return (Task<IEnumerable<Medicine>>)medicines;
            }
        }
        
        [HttpPost("Medicine")]
        public async Task<IActionResult> AddMedicine(MedicineVm medicine)
        {
            try
            {
                if (medicine == null)
                    return BadRequest("Object cannot be null");
                if (await md.AddMedicine(medicine))
                {
                    return StatusCode(
                       StatusCodes.Status200OK,
                       "Medicine added successfully");
                }
                else return StatusCode(StatusCodes.Status406NotAcceptable, "Medicine already exists");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }

        }
        

        [HttpGet("{id}")]
        public Task<Medicine> GetMedicne(int id)
        {
             return md.GetMedicineById(id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, [FromBody] Medicine medicine)
        {
            try
            {
                Medicine med = await md.GetMedicineById(id);
                if (med == null) return NotFound();
                med.Manufacturer = medicine.Manufacturer;
                med.ExpDate= medicine.ExpDate;
                med.Status= medicine.Status;
                med.Discount= medicine.Discount;
                med.ImgUrl= medicine.ImgUrl;
                med.Name= medicine.Name;
                med.Type= medicine.Type;
                med.UnitPrice= medicine.UnitPrice;

                md.UpdateMedicine(med);
                return Ok("Medicine updated successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating medicine");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            try
            {
                Medicine med = await md.GetMedicineById(id);
                if (med == null) return NotFound();
                md.DeleteMedicine(med);
                return Ok("Medicine removed successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error deleting medicine");
            }

        }

    }
}