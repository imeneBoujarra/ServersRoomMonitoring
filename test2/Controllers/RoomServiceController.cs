using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Models;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomServiceController : ControllerBase
    {
        private readonly AppDb_Context _db;

        public RoomServiceController(AppDb_Context db)
        {
            _db = db;
        }

        [HttpPost("AddRoomWithChecklist")]
        public async Task<IActionResult> AddRoomWithChecklist([FromBody] ServerRoomChecklistModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var serverRoom = new ServerRoom
            {
                Room_Number = model.Room_Number,
                Servers_Numbers = model.Servers_Numbers,
                Machines = model.Machines
            };

            var checklist = new Checklist
            {
                HeatPictureUrl = model.Checklist.HeatPictureUrl,
                SwitchersPictureUrl = model.Checklist.SwitchersPictureUrl,
                Backbone = model.Checklist.Backbone,
                Ventilation = model.Checklist.Ventilation,
                Security = model.Checklist.Security,
                Storage = model.Checklist.Storage,
                ServerRoom = serverRoom
            };

            await _db.ServersRoom.AddAsync(serverRoom);
            await _db.Checkliste.AddAsync(checklist);
            await _db.SaveChangesAsync();

            return Ok(new { ServerRoom = serverRoom, Checklist = checklist });
        }

        [HttpPut("UpdateRoomWithChecklist/{id}")]
        public async Task<IActionResult> UpdateRoomWithChecklist(int id, [FromBody] ServerRoomChecklistModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var serverRoom = await _db.ServersRoom.Include(sr => sr.checklists).FirstOrDefaultAsync(sr => sr.Id_Room == id);
            if (serverRoom == null)
            {
                return NotFound($"Server room with ID {id} not found.");
            }

            // Update server room properties
            serverRoom.Room_Number = model.Room_Number;
            serverRoom.Servers_Numbers = model.Servers_Numbers;
            serverRoom.Machines = model.Machines;

            // Update checklist properties
            var checklist = serverRoom.checklists.FirstOrDefault();
            if (checklist != null)
            {
                checklist.HeatPictureUrl = model.Checklist.HeatPictureUrl;
                checklist.SwitchersPictureUrl = model.Checklist.SwitchersPictureUrl;
                checklist.Backbone = model.Checklist.Backbone;
                checklist.Ventilation = model.Checklist.Ventilation;
                checklist.Security = model.Checklist.Security;
                checklist.Storage = model.Checklist.Storage;
            }

            _db.ServersRoom.Update(serverRoom);
            _db.Checkliste.Update(checklist);
            await _db.SaveChangesAsync();

            return Ok(new { ServerRoom = serverRoom, Checklist = checklist });
        }

        [HttpDelete("DeleteRoomWithChecklist/{id}")]
        public async Task<IActionResult> DeleteRoomWithChecklist(int id)
        {
            var serverRoom = await _db.ServersRoom.Include(sr => sr.checklists).FirstOrDefaultAsync(sr => sr.Id_Room == id);
            if (serverRoom == null)
            {
                return NotFound($"Server room with ID {id} not found.");
            }

            var checklist = serverRoom.checklists.FirstOrDefault();
            if (checklist != null)
            {
                _db.Checkliste.Remove(checklist);
            }

            _db.ServersRoom.Remove(serverRoom);
            await _db.SaveChangesAsync();

            return Ok($"Server room with ID {id} and its checklist have been deleted.");
        }
    }

    public class ServerRoomChecklistModel
    {
        public int Room_Number { get; set; }
        public int Servers_Numbers { get; set; }
        public int Machines { get; set; }
        public ChecklistModel Checklist { get; set; }
    }

    public class ChecklistModel
    {
        public string HeatPictureUrl { get; set; }
        public string SwitchersPictureUrl { get; set; }
        public string Backbone { get; set; }
        public string Ventilation { get; set; }
        public string Security { get; set; }
        public string Storage { get; set; }
    }
}
