using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Test.Data.Models
{
    public class Historical
    {
        [Key]
        public int Id { get; set; }  // Added primary key

        public DateTime DateTime { get; set; }  // Combined date and time

        [ForeignKey(nameof(User))]
        public int Id_user { get; set; }
        public Users User { get; set; }

        [ForeignKey(nameof(Checklist))]
        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }

        [MaxLength(500)]
        public string PicturesFolderPath { get; set; }
    }
}
