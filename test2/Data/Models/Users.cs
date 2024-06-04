using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Test.Data.Models
{
    public class Users
    {
        [Key]
        public int? Id_user { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Password { get; set; }

        [MaxLength(50)]
        public string first_name { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        [MaxLength(20)]
        public string Tel { get; set; }

        [MaxLength(50)]
        public string email { get; set; }

        public List <Historical> historcals { get; set; }

    }

}
