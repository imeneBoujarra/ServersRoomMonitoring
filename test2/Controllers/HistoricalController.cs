using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Models;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalController : ControllerBase
    {
        private readonly AppDb_Context _db;

        public HistoricalController(AppDb_Context db)
        {
            _db = db;
        }

        // GET: api/Historical
        [HttpGet]
        public async Task<IActionResult> GetHistoricals()
        {
            var historicals = await _db.Historical.Include(h => h.User).Include(h => h.Checklist).ToListAsync();
            return Ok(historicals);
        }

        // GET: api/Historical/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistorical(int id)
        {
            var historical = await _db.Historical.Include(h => h.User).Include(h => h.Checklist).FirstOrDefaultAsync(h => h.Id == id);

            if (historical == null)
            {
                return NotFound();
            }

            return Ok(historical);
        }

        // POST: api/Historical
        // POST: api/Historical
        [HttpPost]
        public async Task<IActionResult> CreateHistorical([FromBody] HistoricalDto historicalDto)
        {
            if (historicalDto == null)
            {
                return BadRequest("Invalid historical data.");
            }

          

            // Get the user by ID
            var user = await _db.User.FindAsync(historicalDto.Id_user);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Get the checklist by ID
            var checklist = await _db.Checkliste.FindAsync(historicalDto.ChecklistId);
            if (checklist == null)
            {
                return NotFound("Checklist not found.");
            }

            // Map HistoricalDto to Historical entity
            var historical = new Historical
            {
                DateTime = DateTime.Now,
                Id_user = user.Id_user ?? 0, // Use null-coalescing operator to provide a default value
                ChecklistId = checklist.IdChecklist,
                PicturesFolderPath = historicalDto.PicturesFolderPath
            };

            await _db.Historical.AddAsync(historical);
            await _db.SaveChangesAsync();

            return Ok(historical);
        }

        // PUT: api/Historical/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateHistorical(int id, [FromBody] Historical historical)
        {
            if (id != historical.Id)
            {
                return BadRequest("Historical ID mismatch.");
            }

            var existingHistorical = await _db.Historical.FindAsync(id);
            if (existingHistorical == null)
            {
                return NotFound();
            }

            existingHistorical.PicturesFolderPath = historical.PicturesFolderPath;
            // Update other fields as needed

            _db.Historical.Update(existingHistorical);
            await _db.SaveChangesAsync();

            return Ok(existingHistorical);
        }

        // DELETE: api/Historical/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHistorical(int id)
        {
            var historical = await _db.Historical.FindAsync(id);
            if (historical == null)
            {
                return NotFound();
            }

            _db.Historical.Remove(historical);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }


    public class HistoricalDto
    {
        
        public int Id { get; set; }  // Added primary key

        public DateTime DateTime { get; set; }  // Combined date and time
   
        public int Id_user { get; set; }
    
        public int ChecklistId { get; set; }

        public string PicturesFolderPath { get; set; }
    }


}
