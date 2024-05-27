using Microsoft.EntityFrameworkCore;
using Test.Data.Models;

namespace Test.Data
{
    public class AppDb_Context :DbContext 
    {
        public AppDb_Context (DbContextOptions<AppDb_Context> options) : base(options) 
        { 

        }

        public DbSet<Users> User {  get; set; }
        public object Users { get; internal set; }

        public DbSet<Checklist> Checkliste { get; set; }
        public DbSet<Historical> Historical { get; set; }
        public DbSet<ServerRoom> ServersRoom { get; set; }
    }
}
