using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.Models
{
    public class IngredientModel
    {

        //i_id int primary key identity(1,1),
        //u_id int foreign key references users(u_id), -- can be null
        //i_name varchar(150) not null,
        //category varchar(150) not null,
        //protein float,
        //calories float,
        //carbohydrates float,
        //fat float,
        //fiber float,
        //sugar float


        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public float Protein { get; set; }
        public float Calories { get; set; }
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }
        public float Fiber { get; set; }
        public float Sugar { get; set; }

    }
}
