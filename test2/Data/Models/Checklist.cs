using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Data.Models
{
    public class Checklist
    {
        [Key]
        public int IdChecklist { get; set; }
         
        public int ServerRoomId { get; set; }
        public ServerRoom ServerRoom { get; set; }

        // Existing properties...
        [MaxLength(255)]
        public string HeatPictureUrl { get; set; }

        [MaxLength(255)]
        public string SwitchersPictureUrl { get; set; }

        [MaxLength(255)]
        public string Backbone { get; set; }

        [MaxLength(255)]
        public string Ventilation { get; set; }

        [MaxLength(255)]
        public string Security { get; set; }

        [MaxLength(255)]
        public string Storage { get; set; }

        // New property for QR code URL
        [MaxLength(255)]
        public string QRCodeUrl { get; set; }

        public List<Historical> Historicals { get; set; }

    }
}
