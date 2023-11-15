using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Enums;

namespace Sales.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IOrdersHelper _ordersHelper;
         private readonly IUserHelper _userHelper;
        private readonly DataContext _context;

        public SalesController(IOrdersHelper ordersHelper, IUserHelper userHelper, DataContext context)
        {   
            _ordersHelper = ordersHelper;
            _userHelper = userHelper;
            _context = context; 
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.User!)
                
                .Include(s => s.SaleDetails!)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }



        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == User.Identity!.Name);
            }

            return Ok(await queryable
                .OrderByDescending(x => x.Date)
               
                .ToListAsync());
        }

        


        [HttpPost]
        public async Task<ActionResult> Post(SaleDTO saleDTO)
        {
            var response = await _ordersHelper.ProcessOrderAsync(User.Identity!.Name!, saleDTO.Remarks);
            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }
    }
}
