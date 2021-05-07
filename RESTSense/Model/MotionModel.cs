using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense
{
    public class MotionModel
    {
        
        public int MotionId { get; set; }
        public int SensorId { get; set; }
        public string Status { get; set; }
        public DateTime TimeOfDetection { get; set; }
    }
}
