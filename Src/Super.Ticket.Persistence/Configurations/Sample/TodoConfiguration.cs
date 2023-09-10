using Super.Ticket.Domain.Entities.Sample;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Super.Ticket.Persistence.Configurations.Sample
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.ModifiedDate)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");            

            builder.Property(c => c.Name)
                .HasMaxLength(50);

            builder.HasMany(c => c.TodoItems)
                .WithOne(c => c.Todo)
                .HasForeignKey(c => c.TodoId);

            builder.ToTable("Todo");
        }
    }
}
