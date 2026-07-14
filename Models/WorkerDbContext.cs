using Microsoft.EntityFrameworkCore;

namespace WMSB.Models;

public partial class WorkerDbContext : DbContext
{
    public WorkerDbContext()
    {
    }

    public WorkerDbContext(DbContextOptions<WorkerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkerListDto> WorkerListDtos { get; set; }

    public virtual DbSet<PunchRecord> PunchRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("positions");

            entity.HasIndex(e => e.Position1, "Position").IsUnique();

            entity.Property(e => e.Position1)
                .HasMaxLength(50)
                .HasColumnName("Position");

            entity.Property(e => e.Colour)
                .HasMaxLength(7)
                .HasColumnName("Colour");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Contact, "Contact").IsUnique();

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.GoogleId, "GoogleId").IsUnique();

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.Contact).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("workers");

            entity.HasIndex(e => e.PositionId, "positionRef_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssociateId)
                .HasMaxLength(10)
                .HasColumnName("associateId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isDeleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.PositionId).HasColumnName("positionId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Position).WithMany(p => p.Workers)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Workers_Positions");
        });

        modelBuilder.Entity<PunchRecord>(entity =>
        {
            entity.ToTable("punch_records");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.PunchType).HasMaxLength(3).IsRequired();

            entity.Property(e => e.PunchDate).HasColumnType("date");

            entity.Property(e => e.PunchTime).HasColumnType("time");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User).WithMany(u => u.PunchRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PunchRecords_Users");
        });

        modelBuilder.Entity<WorkerListDto>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
