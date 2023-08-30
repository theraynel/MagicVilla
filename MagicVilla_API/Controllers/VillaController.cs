using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas() 
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id")]
        public ActionResult<VillaDto> GetVilla(int id) 
        {
            if (id == 0) return BadRequest("El id debe ser mayor de Cero");

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if (villa == null) return NotFound("No existe Datos con ese Id");

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla( VillaDto villa) 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (villa == null) return BadRequest(ModelState);

            if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
                return Conflict("El nombre que intenta Inserta ya existe");


            if (villa.Id > 0) return StatusCode(500);

            villa.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;

            VillaStore.villaList.Add(villa);

            return CreatedAtAction("GetVilla", new { id = villa.Id }, villa);
    
        }
    }
}
