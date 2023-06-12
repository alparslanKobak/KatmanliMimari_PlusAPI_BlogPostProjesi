using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013KatmanliBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data.Configurations
{
    internal class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);

            builder.Property(x => x.Surname).HasColumnType("nvarchar(50)").HasMaxLength(50);

            builder.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(70)").HasMaxLength(70);

            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(70)").HasMaxLength(70);

            builder.Property(x => x.Phone).HasMaxLength(20);

            builder.Property(x => x.ProfilePicture).HasMaxLength(100);


            builder.Property(x => x.UserName).IsRequired().HasColumnType("nvarchar(35)").HasMaxLength(35);
        }
    }
}
