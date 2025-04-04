using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Exceptions
{
    class DatabaseOperationException : Exception
    {
        public DatabaseOperationException() { }
        public DatabaseOperationException(string message) : base(message) { }
        public DatabaseOperationException(string message, Exception inner) : base(message, inner) { }
    }
}
