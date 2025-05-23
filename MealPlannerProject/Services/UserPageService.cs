﻿using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace MealPlannerProject.Services
{
    class UserPageService
    {
        public bool userHasAnAccount(string name)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", name)
            };

            int? userId = DataLink.Instance.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
            return userId.HasValue && userId.Value > 0;
        }

        public void insertNewUser(string name)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", name)
            };
            DataLink.Instance.ExecuteNonQuery("InsertNewUser", parameters);
        }
    }
}
