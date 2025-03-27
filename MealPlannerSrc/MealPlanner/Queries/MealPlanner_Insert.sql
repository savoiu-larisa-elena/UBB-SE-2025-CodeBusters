USE [MealPlanner];

INSERT INTO goals (g_description) VALUES 
('Lose weight'),
('Gain weight'),
('Maintain weight'),
('Body recomposition'),
('Improve overall health');

INSERT INTO cooking_skills (cs_description) VALUES 
('Beginner'),
('Cook sometimes'),
('Love cooking'),
('Quick meals'),
('Meal prep');

INSERT INTO allergies (a_description) VALUES 
('None'),
('Peanuts'),
('Tree Nuts'),
('Dairy'),
('Eggs'),
('Shellfish'),
('Soy'),
('Wheat'),
('Gluten'),
('Sesame'),
('Fish');

INSERT INTO dietary_preferences (dp_description) VALUES 
('None'),
('Vegetarian'),
('Vegan'),
('Pescatarian'),
('Gluten-Free'),
('Dairy-Free'),
('High-Protein'),
('Keto'),
('Paleo'),
('Low carb');

INSERT INTO activity_levels (al_description) VALUES 
('Sedentary'),
('Lightly Active'),
('Moderately Active'),
('Very Active'),
('Super Active');

INSERT INTO users (u_name, dob, email, u_height, u_weight, g_id, cs_id, dp_id, a_id, al_id) VALUES 
('John Doe', '1990-05-15', 'john.doe@email.com', 1.75, 70, 1, 3, 1, 1, 3),
('Jane Smith', '1985-08-22', 'jane.smith@email.com', 1.68, 65, 2, 2, 2, 4, 2),
('Michael Brown', '1993-11-10', 'michael.b@email.com', 1.80, 85, 3, 5, 3, 1, 4),
('Emily White', '2000-07-30', 'emily.w@email.com', 1.60, 55, 4, 1, 4, 2, 5),
('Chris Johnson', '1992-04-18', 'chris.j@email.com', 1.82, 90, 5, 2, 5, 3, 1);

INSERT INTO calorie_trackers (u_id, today, daily_intake, calories_consumed, calories_burned) VALUES 
(1, '2024-03-27', 2000, 1800, 400),
(2, '2024-03-27', 2200, 1900, 500),
(3, '2024-03-27', 2500, 2400, 600),
(4, '2024-03-27', 1800, 1700, 300),
(5, '2024-03-27', 2300, 2200, 700);

INSERT INTO water_trackers (u_id, water_intake) VALUES 
(1, 2.5),
(2, 2.0),
(3, 3.0),
(4, 1.8),
(5, 2.7);

INSERT INTO grocery_lists (u_id, i_id) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

INSERT INTO meal_types (m_description) VALUES 
('Breakfast'),
('Lunch'),
('Dinner'),
('Snack'),
('Dessert'),
('Post-Workout'),
('Pre-Workout'),
('Vegan Meal'),
('High-Protein Meal'),
('Low-Carb Meal');

INSERT INTO meals (u_id, m_name, recipe, cs_id, dp_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) VALUES 
(1, 'Grilled Chicken Salad', 'Mix all ingredients and serve.', 3, 1, 1, 20, 2, 30, 350, 15, 5, 6, 3, 'https://example.com/chicken_salad.jpg'),
(2, 'Oatmeal with Berries', 'Cook oatmeal and add toppings.', 2, 2, 2, 10, 1, 8, 220, 35, 3, 4, 8, 'https://example.com/oatmeal_berries.jpg'),
(3, 'Salmon with Quinoa', 'Grill salmon and serve with quinoa.', 4, 3, 3, 25, 2, 40, 500, 35, 12, 5, 2, 'https://example.com/salmon_quinoa.jpg'),
(4, 'Vegan Stir Fry', 'Saute vegetables and tofu with soy sauce.', 1, 3, 8, 15, 2, 20, 320, 45, 6, 7, 5, 'https://example.com/vegan_stir_fry.jpg'),
(5, 'Protein Pancakes', 'Blend ingredients and cook in a pan.', 5, 6, 9, 15, 1, 25, 290, 30, 8, 4, 3, 'https://example.com/protein_pancakes.jpg');
