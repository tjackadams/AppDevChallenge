using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityMonitor.Core;
using SecurityMonitor.Core.Models;

namespace SecurityMonitor.Data
{
    public class SecurityMonitorDbContext : DbContext
    {
        public SecurityMonitorDbContext(DbContextOptions<SecurityMonitorDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(SecurityMonitorDbContext)));
        }

        private class DeviceConfiguration : IEntityTypeConfiguration<Device>
        {
            public void Configure(EntityTypeBuilder<Device> builder)
            {
                builder
                    .ToTable("Devices");

                builder
                    .HasKey(p => p.Id);
            }
        }
    }
}
