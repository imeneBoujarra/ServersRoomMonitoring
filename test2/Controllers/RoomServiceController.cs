using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Test.Data;
using Test.Data.Models;

namespace test2.Controllers
{
    public class ServerRoomController : ControllerBase
    {
        private readonly AppDb_Context _db;
        private readonly string _qrCodeDirectory;

        public ServerRoomController(AppDb_Context db)
        {
            _db = db;
            _qrCodeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "qrcodes");
        }

        [HttpPost]
        [Route("CreateServerRoom")]
        public async Task<IActionResult> CreateServerRoom([FromBody] ServerRoomDTO serverRoomDto)
        {
            if (serverRoomDto == null)
            {
                return BadRequest("Invalid server room data.");
            }

            var serverRoom = new ServerRoom
            {
                Room_Number = serverRoomDto.Room_Number,
                Servers_Numbers = serverRoomDto.Servers_Numbers,
                Machines = serverRoomDto.Machines,
                VerifyHeat = serverRoomDto.VerifyHeat,
                VerifySwitchers = serverRoomDto.VerifySwitchers,
                VerifyBackbone = serverRoomDto.VerifyBackbone,
                VerifyVentilation = serverRoomDto.VerifyVentilation,
                VerifySecurity = serverRoomDto.VerifySecurity,
                VerifyStorage = serverRoomDto.VerifyStorage
            };

            string qrCodeUrl = GenerateQRCode(serverRoom.Room_Number.ToString());
            serverRoom.QRCodeUrl = qrCodeUrl;

            await _db.ServersRoom.AddAsync(serverRoom);
            await _db.SaveChangesAsync();

            return Ok(serverRoom);
        }

        [HttpGet("byqrcode")]
        public async Task<IActionResult> GetServerRoomByQrCode(string qrCode)
        {
            if (string.IsNullOrEmpty(qrCode))
            {
                return BadRequest("QR code URL is required.");
            }

            var serverRoom = await _db.ServersRoom
                .Include(sr => sr.Checklists)
                .FirstOrDefaultAsync(sr => sr.Room_Number.ToString() == qrCode);

            if (serverRoom == null)
            {
                return NotFound("Server room not found.");
            }

            return Ok(serverRoom);
        }

        private string GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
            {
                if (!Directory.Exists(_qrCodeDirectory))
                {
                    Directory.CreateDirectory(_qrCodeDirectory);
                }

                string fileName = $"{Guid.NewGuid()}.png";
                string filePath = Path.Combine(_qrCodeDirectory, fileName);
                qrCodeImage.Save(filePath, ImageFormat.Png);

                return $"/qrcodes/{fileName}";
            }
        }
    }

    public class ServerRoomDTO
    {
        public int Room_Number { get; set; }

        public int Servers_Numbers { get; set; }

        public int Machines { get; set; }

        public bool VerifyHeat { get; set; }

        public bool VerifySwitchers { get; set; }

        public bool VerifyBackbone { get; set; }

        public bool VerifyVentilation { get; set; }

        public bool VerifySecurity { get; set; }

        public bool VerifyStorage { get; set; }
    }
}
