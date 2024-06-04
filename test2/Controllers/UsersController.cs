using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Data;
using Test.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(AppDb_Context db)
        {
            _db = db;
        }

        private readonly AppDb_Context _db;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {   
            var us = await _db.User.ToListAsync();
            return Ok(us);
        }

        [HttpPost]
        [Route("Add users")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto.pass != userDto.confirmPass)
            {
                return BadRequest("Passwords do not match");
            }

            Users x = new()
            {
                Name = userDto.Name,
                Password = userDto.pass,
                first_name = userDto.f_n,
                Role = userDto.r,
                Tel = userDto.ph,
                email = userDto.mail
            };

            await _db.User.AddAsync(x);
            _db.SaveChanges();

            return Ok(x);
        }


        [HttpDelete]
        [Route("RemoveUserbyId/{id}")]
        public async Task <IActionResult> RemoveUserbyId(int Id)
        {
            var c=await _db.User.SingleOrDefaultAsync(x=> x.Id_user==Id);
            if (c==null)
            {
                return NotFound($"User Id {Id} das not exists");

            }
            _db.User.Remove(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpGet("GetUsersById/{id}")]
        public async Task<IActionResult> GetUsersById(int id)
        {

            var c = await _db.User.SingleOrDefaultAsync(x => x.Id_user == id);
            if (c == null)
            {
                return NotFound($"User Id {id} das not exists");

            }
            return Ok(c);
        }


        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var user = await _db.User.SingleOrDefaultAsync(x => x.Id_user == id);
            if (user == null)
            {
                return NotFound($"User Id {id} does not exist");
            }

            user.Name = userDto.Name;
            user.Password = userDto.pass;
            user.first_name = userDto.f_n;
            user.Role = userDto.r;
            user.Tel = userDto.ph;
            user.email = userDto.mail;

            _db.User.Update(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }



        [HttpPost]
         [Route("Login")]
         public async Task<ActionResult<Users>> Login([FromBody] LoginModel login)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }


            var user = await _db.User.FirstOrDefaultAsync(u => u.email == login.Email && u.Password == login.Password);
 

            if (user == null)
             {
                 return NotFound("User not found password or mail adress are incorrect.");
             }

             // Vérifiez le rôle de l'utilisateur
             if (user.Role == "Admin")
             {
                 return Ok(new { User = user, Role = "Admin" });
             }
             else if (user.Role == "Technician")
             {
                 return Ok(new { User = user, Role = "Technician" });
             }

             else
             {
                 return Unauthorized("Rôle non autorisé.");
             }
         }

     }
    public class UserDto
    {
        public string Name { get; set; }
        public string pass { get; set; }
        public string confirmPass { get; set; }
        public string f_n { get; set; }
        public string r { get; set; }
        public string ph { get; set; }
        public string mail { get; set; }
    }

    public class LoginModel
     {
         public string Email { get; set; }
         public string Password { get; set; }
     }

                
}
