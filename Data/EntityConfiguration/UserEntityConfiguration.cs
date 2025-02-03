using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Email).IsRequired().HasMaxLength(200);

        // builder.HasMany(user => user.Posts).WithOne(post => post.User).HasForeignKey(post => post.UserId);

        builder.Property(user => user.Password).IsRequired();

        builder.Property(user => user.Name).IsRequired();   
    }
}