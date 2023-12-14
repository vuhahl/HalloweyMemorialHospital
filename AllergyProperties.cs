using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HollowellMemorialHospital
{
    public class AllergyProperties
    {
        public string AllergyID { get; set; }
        public string PID { get; set; }
        public string Allergen { get; set; }
        public string AllergyStart { get; set; }
        public string AllergyEnd { get; set; }
        public string Description { get; set; }
        public int IsDeleted { get; set; }
    }
}
