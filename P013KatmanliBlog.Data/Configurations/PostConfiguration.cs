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
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasColumnType("nvarchar(75)").HasMaxLength(75);

            builder.Property(x => x.PostMessage).HasColumnType("nvarchar(500)").HasMaxLength(500);
            
            builder.Property(x => x.PostImage).HasMaxLength(100);

            builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x=> x.UserId);

            builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(x=> x.CategoryId);

            
            
            
        }
    }
}
