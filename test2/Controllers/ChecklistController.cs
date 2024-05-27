using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
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

        [HttpPost("CreateChecklist")]
        public async Task<IActionResult> CreateChecklist([FromBody] Checklist checklist)
        {
            if (checklist == null)
            {
                return BadRequest("Invalid checklist data");
            }

            // Generate QR code for the checklist
            string qrCodeUrl = await GenerateQRCode(checklist.IdChecklist.ToString());
            checklist.QRCodeUrl = qrCodeUrl;

            // Save the checklist to the database
            await _db.Checkliste.AddAsync(checklist);
            await _db.SaveChangesAsync();

            return Ok(checklist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChecklistById(int id)
        {
            var checklist = await _db.Checkliste.FindAsync(id);
            if (checklist == null)
            {
                return NotFound($"Checklist with ID {id} not found");
            }

            return Ok(checklist);
        }

        [HttpGet("QRCode/{id}")]
        public async Task<IActionResult> GetQRCode(int id)
        {
            var checklist = await _db.Checkliste.FindAsync(id);
            if (checklist == null)
            {
                return NotFound($"Checklist with ID {id} not found");
            }

            string qrCodeUrl = checklist.QRCodeUrl;
            if (string.IsNullOrEmpty(qrCodeUrl))
            {
                return NotFound("QR code not available for this checklist");
            }

            // Return the QR code image URL
            return Ok(new { QRCodeUrl = qrCodeUrl });
        }

        private async Task<string> GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var qrCodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "qrcodes", $"{Guid.NewGuid()}.png");
            qrCodeImage.Save(qrCodePath);

            return qrCodePath;
        }
    }
}
