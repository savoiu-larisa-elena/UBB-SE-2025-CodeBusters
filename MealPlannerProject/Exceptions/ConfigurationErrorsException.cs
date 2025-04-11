using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Exceptions
{
    public class ConfigurationErrorsException : Exception
    {
        public ConfigurationErrorsException() { }
        public ConfigurationErrorsException(string message) : base(message) { }
        public ConfigurationErrorsException(string message, Exception inner) : base(message, inner) { }
    }
}
