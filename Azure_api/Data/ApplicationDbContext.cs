using Azure_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = STOREPROCEDURES; Trusted_Connection = True; MultipleActiveResultSets = true");
        }


        //"Server=tcp:igorsouza0489.database.windows.net,1433;Initial Catalog=igorsouza0489;Persist Security Info=False;User ID=igorsouza0489;Password=Dragon123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Friends> Friends { get; set; }
    }
}
