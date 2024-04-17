using Microsoft.EntityFrameworkCore;

namespace SmsApp.Data;

public partial class ShortMessagesDbContext : DbContext
{
    public ShortMessagesDbContext()
    {
    }

    public ShortMessagesDbContext(DbContextOptions<ShortMessagesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ShortMessage> ShortMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Greek_100_CI_AI");

        modelBuilder.Entity<ShortMessage>(entity =>
        {
            entity.HasKey(e => e.MessagesId).HasName("PK__Messages__157641324EF86F95");

            entity.Property(e => e.MessagesId).HasColumnName("messages_id");
            entity.Property(e => e.MessageBody)
                .HasMaxLength(512)
                .HasColumnName("message_body");
            entity.Property(e => e.Recipient)
                .HasMaxLength(50)
                .HasColumnName("recipient");
            entity.Property(e => e.RecipientCountryCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("recipient_country_code");
            entity.Property(e => e.Sender)
                .HasMaxLength(50)
                .HasColumnName("sender");
            entity.Property(e => e.SenderCountryCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sender_country_code");
            entity.Property(e => e.Vendor)
                .HasMaxLength(50)
                .HasColumnName("vendor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
