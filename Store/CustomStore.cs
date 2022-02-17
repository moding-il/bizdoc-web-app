using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class CustomStore : DbContext
    {
    public CustomStore(DbContextOptions<CustomStore> options) : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
    }
    public class Account
    {
    [MaxLength(50)]
        public string Id { get; set; }
    [MaxLength(120)]
    public string Name { get; set; }
    }
}
