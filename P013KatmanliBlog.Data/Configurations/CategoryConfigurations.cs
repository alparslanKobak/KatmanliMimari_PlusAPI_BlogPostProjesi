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
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           

            builder.Property(x => x.Name).HasColumnType("nvarchar(50)").HasMaxLength(50);

            
        }
    }
}
