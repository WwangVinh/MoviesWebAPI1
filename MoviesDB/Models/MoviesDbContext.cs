using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoviesDB.Models;

public partial class MoviesDbContext : DbContext
{
    public MoviesDbContext()
    {
    }

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GP36FMA;User ID=sa;Password=Djchip123@;Database=movieDB;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorsId).HasName("PK__Actors__E60C9472F3553F8F");

            entity.Property(e => e.ActorsId).HasColumnName("ActorsID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.NameAct).HasMaxLength(225);
            entity.Property(e => e.Nationlity).HasMaxLength(100);
            entity.Property(e => e.Professional).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoriesId).HasName("PK__Categori__EFF907B0D9781D2D");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0AEEB05AB").IsUnique();

            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__26C69E26B9ECA503");

            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.NameDir).HasMaxLength(225);
            entity.Property(e => e.Nationlity).HasMaxLength(100);
            entity.Property(e => e.Professional).HasMaxLength(255);
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.EpisodeId).HasName("PK__Episodes__AC6676155B9F4946");

            entity.Property(e => e.EpisodeId).HasColumnName("EpisodeID");
            entity.Property(e => e.LinkFilmUrl)
                .HasMaxLength(255)
                .HasColumnName("LinkFilmURL");
            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Series).WithMany(p => p.Episodes)
                .HasForeignKey(d => d.SeriesId)
                .HasConstraintName("FK__Episodes__Series__4BAC3F29");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2943A16B19E5F");

            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURl");
            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.LinkFilmUrl)
                .HasMaxLength(255)
                .HasColumnName("LinkFilmURl");
            entity.Property(e => e.PosterUrl)
                .HasMaxLength(255)
                .HasColumnName("PosterURL");
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Movies__Director__44FF419A");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorsId")
                        .HasConstraintName("FK__MovieActo__Actor__5BE2A6F2"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__MovieActo__Movie__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorsId").HasName("PK__MovieAct__75B25D7DDD8F73A9");
                        j.ToTable("MovieActor");
                        j.IndexerProperty<int>("MovieId").HasColumnName("MovieID");
                        j.IndexerProperty<int>("ActorsId").HasColumnName("ActorsID");
                    });

            entity.HasMany(d => d.Categories).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoriesId")
                        .HasConstraintName("FK__MovieCate__Categ__5441852A"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK__MovieCate__Movie__534D60F1"),
                    j =>
                    {
                        j.HasKey("MovieId", "CategoriesId").HasName("PK__MovieCat__552D044108191110");
                        j.ToTable("MovieCategories");
                        j.IndexerProperty<int>("MovieId").HasColumnName("MovieID");
                        j.IndexerProperty<int>("CategoriesId").HasColumnName("CategoriesID");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.SubpaymentId).HasName("PK__Payment__81380600A65B8D6A");

            entity.ToTable("Payment");

            entity.Property(e => e.SubpaymentId).HasColumnName("SubpaymentID");
            entity.Property(e => e.PlanName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payment__UserID__4F7CD00D");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("PK__Series__F3A1C101728B2429");

            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURl");
            entity.Property(e => e.DirectorId).HasColumnName("DirectorID");
            entity.Property(e => e.PosterUrl)
                .HasMaxLength(255)
                .HasColumnName("PosterURL");
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.Season).HasDefaultValue(1);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Director).WithMany(p => p.Series)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__Series__Director__48CFD27E");

            entity.HasMany(d => d.Actors).WithMany(p => p.Series)
                .UsingEntity<Dictionary<string, object>>(
                    "SeriesActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorsId")
                        .HasConstraintName("FK__SeriesAct__Actor__5FB337D6"),
                    l => l.HasOne<Series>().WithMany()
                        .HasForeignKey("SeriesId")
                        .HasConstraintName("FK__SeriesAct__Serie__5EBF139D"),
                    j =>
                    {
                        j.HasKey("SeriesId", "ActorsId").HasName("PK__SeriesAc__CDC10846EB0DA98F");
                        j.ToTable("SeriesActor");
                        j.IndexerProperty<int>("SeriesId").HasColumnName("SeriesID");
                        j.IndexerProperty<int>("ActorsId").HasColumnName("ActorsID");
                    });

            entity.HasMany(d => d.Categories).WithMany(p => p.Series)
                .UsingEntity<Dictionary<string, object>>(
                    "SeriesCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoriesId")
                        .HasConstraintName("FK__SeriesCat__Categ__5812160E"),
                    l => l.HasOne<Series>().WithMany()
                        .HasForeignKey("SeriesId")
                        .HasConstraintName("FK__SeriesCat__Serie__571DF1D5"),
                    j =>
                    {
                        j.HasKey("SeriesId", "CategoriesId").HasName("PK__SeriesCa__ED5E517A43F52C68");
                        j.ToTable("SeriesCategories");
                        j.IndexerProperty<int>("SeriesId").HasColumnName("SeriesID");
                        j.IndexerProperty<int>("CategoriesId").HasColumnName("CategoriesID");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACF14E7C61");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4019F32C1").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105340287D668").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
