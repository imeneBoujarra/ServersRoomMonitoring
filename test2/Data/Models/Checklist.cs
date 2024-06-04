using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Data.Models
{
    public class Checklist
    {
        [Key]
        public int IdChecklist { get; set; }

        [ForeignKey(nameof(ServerRoom))]
        public int ServerRoomId { get; set; }

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

        public bool State { get; set; }

        public List<Historical> Historicals { get; set; }

    }
}
