using Infrastructure.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Mappings.EntityFramework
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(increment:1,seed:1);
            builder.HasIndex(x=>x.TcNo).IsUnique();
            builder.Property(x => x.PasswordHash).HasColumnType("varbinary(max)");
            builder.Property(x => x.PasswordSalt).HasColumnType("varbinary(max)");
        }
    }
}
