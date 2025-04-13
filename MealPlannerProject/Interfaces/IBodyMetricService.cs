using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces
{
    public interface IBodyMetricService
    {
        void UpdateUserBodyMetrics(string firstName, string lastName, string weight, string height, string targetGoal);
    }

}
