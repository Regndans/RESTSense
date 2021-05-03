using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PirSensor;

namespace RESTSense
{
    public class PirContext : DbContext
    {
        public PirContext(DbContextOptions<PirContext> options) : base(options)
        { }
        public DbSet<PirSensorModel> Pir { get; set; }
    }
}
