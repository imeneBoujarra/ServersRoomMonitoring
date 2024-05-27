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
        public async Task<IActionResult> AddUser(string Name, string pass, string confirmPass, string f_n, string r, string ph, string mail)
        {
            if (pass != confirmPass)
            {
                return BadRequest("Passwords do not match");
            }

            Users x = new() { Name = Name, Password = pass, first_name = f_n, Role = r, Tel = ph, email = mail };
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



         [HttpPost]
         [Route("Login")]
         public async Task<ActionResult<Users>> Login([FromForm] LoginModel login)
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

     public class LoginModel
     {
         public string Email { get; set; }
         public string Password { get; set; }
     }

                
}
