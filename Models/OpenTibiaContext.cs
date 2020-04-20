using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyrlandAAC.Models
{
	public class OpenTibiaContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Player> Players {get;set;}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("server=192.168.1.131;database=myrland;user=root;password=myrland;port=3306");
		}

		

	}
}
