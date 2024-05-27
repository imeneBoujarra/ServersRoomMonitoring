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
        public String Date { get; set; }

        [MaxLength(50)]
        public string Hour { get; set; }

        [ForeignKey(nameof(User))]
        public int Id_user { get; set; }

        public Users User { get; set; }

        [ForeignKey(nameof(Checklist))]
        public int ChecklistId { get; set; }
        public List<Checklist> Checklist { get; set; }

        [MaxLength(500)]
        public String PicturesFolderPath { get; set; }

    }
}
