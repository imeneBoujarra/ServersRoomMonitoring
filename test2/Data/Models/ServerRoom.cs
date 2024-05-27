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

        public List<Checklist> checklists { get; set; }


    }
}
