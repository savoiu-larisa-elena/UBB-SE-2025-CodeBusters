using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.Models
{
    public class BodyMetricsModel
    {
        public int Id { get; set; }  // bm_id
        public int Height { get; set; }
        public int Weight { get; set; }
        public int TargetWeight { get; set; }
    }
}
