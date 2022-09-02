using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data;

public sealed class GameDbContext : DbContext
{
    public DbSet<GameModel> Games { get; set; } = null!;

    public GameDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseInMemoryDatabase("GamesDB");
}