using API.Constants;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Config for User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                entity.Property(e => e.Username)
                    .IsUnicode(false)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(e => e.Password)
                    .IsUnicode()
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.UserType)
                    .HasDefaultValue(UserTypes.Customer)
                    .IsRequired();
            });

            // Config for Question entity
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                entity.Property(e => e.Content)
                    .IsUnicode()
                    .HasMaxLength(500)
                    .IsRequired();
                entity.Property(e => e.OptionA)
                    .IsUnicode()
                    .HasMaxLength(500);
                entity.Property(e => e.OptionB)
                    .IsUnicode()
                    .HasMaxLength(500);
                entity.Property(e => e.OptionC)
                    .IsUnicode()
                    .HasMaxLength(500);
            });

            // Config for Answer entity and relationships
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                entity.Property(e => e.AnswerContent)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired();

                // Foreign Key to Question
                entity.HasOne(a => a.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(a => a.QuestionId)
                    .IsRequired();
            });
        }
    }
}
