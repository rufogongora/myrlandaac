using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyrlandAAC.Models
{
    public class OpenTibiaContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Player> Players { get; set; }

        private string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }

        public OpenTibiaContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection("ConnectionString").Value;
        }



    }
}
