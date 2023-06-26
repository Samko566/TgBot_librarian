using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TgBot_librarian;

namespace TgBot_librarian
{
    public class DebtDbContext : DbContext
    {
        public DbSet<DebtDb> Debts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "debts.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}

