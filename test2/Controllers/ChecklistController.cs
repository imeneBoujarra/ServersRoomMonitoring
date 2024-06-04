using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Models;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly AppDb_Context _db;

        public ChecklistController(AppDb_Context db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("CreateChecklist")]
        public async Task<IActionResult> CreateChecklist([FromBody] ChecklistDto checklistDto)
        {
            if (checklistDto == null)
            {
                return BadRequest("Invalid checklist data.");
            }

            var serverRoom = await _db.ServersRoom.FindAsync(checklistDto.ServerRoomId);
            if (serverRoom == null)
            {
                return NotFound("Server room not found.");
            }

            var checklist = new Checklist
            {
                ServerRoomId = checklistDto.ServerRoomId,
                HeatPictureUrl = checklistDto.HeatPictureUrl,
                SwitchersPictureUrl = checklistDto.SwitchersPictureUrl,
                Backbone = checklistDto.Backbone,
                Ventilation = checklistDto.Ventilation,
                Security = checklistDto.Security,
                Storage = checklistDto.Storage,
                State = true // Set initial state
            };

            await _db.Checkliste.AddAsync(checklist);
            await _db.SaveChangesAsync();

            // Retrieve current user ID (replace this with your actual user ID retrieval logic)
            int userId = 1;

            // Set pictures folder path (assuming a predefined structure)
            string picturesFolderPath = $"/images/checklists/{checklist.IdChecklist}";

            // Add historical record
            var historical = new Historical
            {
                DateTime = DateTime.Now,
                Id_user = userId,
                ChecklistId = checklist.IdChecklist,
                PicturesFolderPath = picturesFolderPath
            };

            await _db.Historical.AddAsync(historical);
            await _db.SaveChangesAsync();

            return Ok(checklist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Checklist>> GetChecklist(int id)
        {
            var checklist = await _db.Checkliste.FindAsync(id);

            if (checklist == null)
            {
                return NotFound();
            }

            return Ok(checklist);
        }
    }


        public class ChecklistDto
    {
        public int IdChecklist { get; set; }

        public int ServerRoomId { get; set; }

        public string HeatPictureUrl { get; set; }

        public string SwitchersPictureUrl { get; set; }

        public string Backbone { get; set; }

        public string Ventilation { get; set; }

        public string Security { get; set; }

        public string Storage { get; set; }

        public int UserId { get; set; }
 


    }
}
