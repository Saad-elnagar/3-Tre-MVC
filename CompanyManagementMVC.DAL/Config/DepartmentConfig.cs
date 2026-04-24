using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Config
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.Property(D=> D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasMaxLength(50).IsRequired();
            builder.Property(D => D.Description).HasMaxLength(50);
            builder.HasKey(D => D.Id);
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(D => D.UpdatedOn).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();




        }
    }
}
