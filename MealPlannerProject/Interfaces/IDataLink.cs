<<<<<<< HEAD
﻿﻿using System;
=======
﻿using System;
>>>>>>> d94c7e742c2c933962afdd1d7e5b6c25110af836
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces
{
    public interface IDataLink
    {
        [Obsolete]
        T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);
        [Obsolete]
        int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters);
        [Obsolete]
        DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters);
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> d94c7e742c2c933962afdd1d7e5b6c25110af836
