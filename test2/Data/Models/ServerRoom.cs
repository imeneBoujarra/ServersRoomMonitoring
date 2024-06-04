using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Test.Data.Models
{
    public class ServerRoom
    {
        [Key]
        public int? Id_Room { get; set; }

        public int Room_Number { get; set; }

        public int Servers_Numbers { get; set; }

        public int Machines { get; set; }

        public bool VerifyHeat { get; set; }

        public bool VerifySwitchers { get; set; }

        public bool VerifyBackbone { get; set; }

        public bool VerifyVentilation { get; set; }

        public bool VerifySecurity { get; set; }

        public bool VerifyStorage { get; set; }

        public List<Checklist> Checklists { get; set; }

        [MaxLength(255)]
        public string QRCodeUrl { get; set; }

    }
}
