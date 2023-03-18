namespace Datalagring.Contexts;

using Datalagring.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Git\GitHub\Datalagring\Contexts\local_db.mdf;Integrated Security=True;Connect Timeout=30";
    
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<CustomerEntity> Customer { get; set; } = null!;

    public DbSet<CommentEntity> Comment { get; set; } = null!;

    public DbSet<CommentsEntity> Comments { get; set; } = null!;

    public DbSet<StatusEntity> Status { get; set; } = null!;

    public DbSet<TicketEntity> Ticket { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}