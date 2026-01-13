using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulation_2.Models;

namespace Simulation_2.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
            builder.Property(x => x.Degree).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Teachers).HasForeignKey(x => x.CourseID).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
