using Microsoft.EntityFrameworkCore;

namespace ModularPipelines.EFForTests;

internal partial class Program
{
	public class TestDbContext1: DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(
					"Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;");
			}
		}
	}
	public class TestDbContext2 : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(
					"Server=(localdb)\\MSSQLLocalDB;Database=TestDb;Trusted_Connection=True;");
			}
		}
	}
}
