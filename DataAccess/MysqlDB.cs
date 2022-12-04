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
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.IsAdmin)
                  .HasDefaultValue(false);
            entity.Property(e => e.Salt)
                  .IsRequired();
            entity.Property(e => e.RegistrationDate)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP()");
        });

        modelBuilder.Entity<RecipeIngredient>()
           .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        modelBuilder.Entity<RecipeIngredient>()
           .HasOne(ri => ri.Recipe)
           .WithMany(r => r.RecipeIngredients)
           .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
}
