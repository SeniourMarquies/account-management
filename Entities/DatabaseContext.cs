using Microsoft.EntityFrameworkCore;

namespace nop.gg.Entities
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			// The constructor for the DatabaseContext class. It takes DbContextOptions as a parameter and passes it to the base constructor.
		}

		public DbSet<User> Users { get; set; }
		// DbSet representing the "Users" table in the database.

		// You can define additional DbSet properties for other database entities here if needed.
	}
}
