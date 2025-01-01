using GIS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricPolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ElectricPolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ElectricPolesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var poles = await _context.ElectricPoles
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    Location = new
                    {
                        Latitude = p.Location.Y,
                        Longitude = p.Location.X
                    }
                })
                .ToListAsync();

            return Ok(poles);
        }

        // GET api/<ElectricPolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ElectricPolesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ElectricPolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ElectricPolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
