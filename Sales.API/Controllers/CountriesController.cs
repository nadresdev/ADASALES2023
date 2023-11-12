
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Sales.API.Data;
using Sales.Shared.Entities; 


namespace Sales.API.Controllers
{

    [ApiController]
    [Route("/api/countries")]
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;


        public CountriesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            _context.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);


        }

        

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            return Ok(await _context.Countries.ToListAsync());

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x=>x.Id == id);
            if(country == null) {

                return NotFound();
            }
            else { return Ok(country); }
            

        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return Ok(country);


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {

                return NotFound();
            }
            else
            {
                _context.Remove(country);
                await _context.SaveChangesAsync();
                return NoContent();
            }


        }



    }
}
