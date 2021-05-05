using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense
{
    public class PirSensorModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public DateTime TimeOfDetection { get; set; }
    }
}
