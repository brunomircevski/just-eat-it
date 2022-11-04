using Microsoft.EntityFrameworkCore;
using JEI.Models;

namespace JEI.DataAccess;

public class MysqlDB : DbContext, IDB
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString: @"Data Source=178.213.141.124;port=3306;Initial Catalog=jei;User Id=jei;password=jei2022",
            new MySqlServerVersion(new Version(10, 6, 7)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ;
    }
    
    public DbSet<User> Users { get; set; }
}
