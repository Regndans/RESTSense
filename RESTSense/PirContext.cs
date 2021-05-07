using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RESTSense
{
    public class PirContext : DbContext
    {
        public PirContext(DbContextOptions<PirContext> options) : base(options)
        { }
        public DbSet<MotionModel> MotionList { get; set; }
        public DbSet<SensorModel> SensorList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MotionModel>().HasKey(o =>  o.MotionId).HasName("PrimaryKey_MotionId");
        }
    }
}
