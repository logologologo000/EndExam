using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trackingapi.Models;

namespace trackingapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalController : ControllerBase
    {
        private readonly ExamDbContext _context;

        public CalController(ExamDbContext context)
        {

            _context = context;

        }

        [HttpGet]
        public async Task<IEnumerable<Cal>> Get() => await _context.Cals.ToListAsync();



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var cals = await _context.Cals.FindAsync(id);
            return cals == null ? NotFound() : Ok(cals);
        }



        [Route("save")]
        [HttpPost]
        [ProducesResponseType(typeof(Cal), StatusCodes.Status201Created)]
        public async Task<IActionResult> Save(Cal cals)
        {
            await _context.Cals.AddAsync(cals);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = cals.Id }, cals);
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(Cal cals)
        {

            

            if (cals.marks == "+")
            {
                cals.result = cals.num1 + cals.num2;
            }
            else if (cals.marks == "-")
            {
                cals.result = cals.num1 - cals.num2;
            }
            else if (cals.marks == "*")
            {
                cals.result = cals.num1 * cals.num2;
            }
            else if (cals.marks == "/")
            {
                cals.result = cals.num1 / cals.num2;
            }
            else
            {
                return NoContent();
            }

            await Save(cals);


            return  Ok(cals.result);

        }

        
    }
}
