using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense.Model
{
    public class TimerModel
    {
        public int Id { get; set; }
        public DateTime ActiveStart { get; set; }
        public DateTime ActiveEnd { get; set; }
        public bool TimerActive { get; set; }
    }
}
