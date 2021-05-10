using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense
{
    public class SensorModel
    {
        [Key]
        public int SensorId { get; set; }//TODO Skal vi bruge set??
        public string SensorName { get; set; }
        public bool Active { get; set; }
    }
}
