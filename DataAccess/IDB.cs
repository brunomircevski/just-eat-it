using JEI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JEI.DataAccess;

public interface IDB
{
    public DbSet<User> Users { get; set; }
    public int SaveChanges();
    public EntityEntry Remove(object entity);
}
