using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowellMemorialHospital
{
    internal class Meds
    {
        public string MedID { get; set; }
        public string PID { get; set; }
        public string MedName { get; set; }
        public string MedAMT { get; set; }
        public string MedUnit { get; set; }
        public string Instructions { get; set; }
        public string MedStart { get; set; }
        public string MedEnd { get; set; }
        public string Prescription { get; set; }
        public int IsDeleted { get; set; }
    }
}
