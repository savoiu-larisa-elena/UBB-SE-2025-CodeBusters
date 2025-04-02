USE [MealPlanner];

INSERT INTO goals (g_description) VALUES 
('Lose weight'),
('Gain weight'),
('Maintain weight'),
('Body recomposition'),
('Improve overall health');

select* from goals

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


INSERT INTO users (u_name, dob, email, u_height, u_weight, u_target_weight, g_id, cs_id, dp_id, a_id, al_id) VALUES 
('John Doe', '1990-05-15', 'john.doe@email.com', 1.75, 70, 65.00, 1, 3, 1, 1, 3), -- Lose weight (target < current weight)
('Jane Smith', '1985-08-22', 'jane.smith@email.com', 1.68, 65, 75.99, 2, 2, 2, 4, 2), -- Gain weight (target > current weight)
('Michael Brown', '1993-11-10', 'michael.b@email.com', 1.80, 85, 85.12, 3, 5, 3, 1, 4), -- Maintain weight (target = current weight)
('Emily White', '2000-07-30', 'emily.w@email.com', 1.60, 55, 58.12, 4, 1, 4, 2, 5), -- Body recomposition (small weight adjustments)
('Chris Johnson', '1992-04-18', 'chris.j@email.com', 1.82, 90, 88.12, 5, 2, 5, 3, 1); -- Improve overall health (minor weight changes)


select* from users

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

select* from water_trackers

-- Insert ingredients
INSERT INTO ingredients (u_id, i_name, category, protein, calories, carbohydrates, fat, fiber, sugar) VALUES
(1, 'Chicken Breast', 'Protein', 31, 165, 0, 3.6, 0, 0),
(2, 'Oats', 'Carbs', 6, 150, 27, 3.2, 4, 0),
(3, 'Salmon', 'Protein', 25, 232, 0, 13.6, 0, 0),
(4, 'Tofu', 'Protein', 8, 144, 2, 8, 1, 0),
(5, 'Whey Protein Powder', 'Protein', 25, 120, 3, 1, 2, 1);


INSERT INTO grocery_lists (u_id, i_id) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- Insert meal ingredients
INSERT INTO meal_ingredient (m_id, i_id, quantity) VALUES
(1, 1, 200),  -- Grilled Chicken Salad uses 200g Chicken Breast
(2, 2, 50),   -- Oatmeal with Berries uses 50g Oats
(3, 3, 200),  -- Salmon with Quinoa uses 200g Salmon
(4, 4, 150),  -- Vegan Stir Fry uses 150g Tofu
(5, 5, 30);   -- Protein Pancakes uses 30g Whey Protein Powder

SELECT * FROM meal_ingredient;



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
-- Insert into meals first


INSERT INTO meals (u_id, m_name, recipe, cs_id, dp_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) 
VALUES 
(1, 'Grilled Chicken Salad', 'Mix all ingredients and serve.', 3, 1, 1, 20, 2, 30, 350, 15, 5, 6, 3, 'https://example.com/chicken_salad.jpg'),
(2, 'Oatmeal with Berries', 'Cook oatmeal and add toppings.', 2, 2, 2, 10, 1, 8, 220, 35, 3, 4, 8, 'https://example.com/oatmeal_berries.jpg'),
(3, 'Salmon with Quinoa', 'Grill salmon and serve with quinoa.', 4, 3, 3, 25, 2, 40, 500, 35, 12, 5, 2, 'https://example.com/salmon_quinoa.jpg'),
(4, 'Vegan Stir Fry', 'Saute vegetables and tofu with soy sauce.', 1, 3, 8, 15, 2, 20, 320, 45, 6, 7, 5, 'https://example.com/vegan_stir_fry.jpg'),
(5, 'Protein Pancakes', 'Blend ingredients and cook in a pan.', 5, 6, 9, 15, 1, 25, 290, 30, 8, 4, 3, 'https://example.com/protein_pancakes.jpg');



-- Now insert into favourite_meal
INSERT INTO favourite_meal (m_id, u_id, is_fav) VALUES
(1, 1, 1),  -- John Doe marks Grilled Chicken Salad as favorite
(2, 2, 0),  -- Jane Smith does not mark Oatmeal with Berries as favorite
(3, 3, 1),  -- Michael Brown marks Salmon with Quinoa as favorite
(4, 4, 1),  -- Emily White marks Vegan Stir Fry as favorite
(5, 5, 0);  -- Chris Johnson does not mark Protein Pancakes as favorite



-- Insert sample data into body_metrics table
INSERT INTO body_metrics (height, weight, target_weight)
VALUES 
(175, 70, 65),  -- Example data: height = 175 cm, weight = 70 kg, target weight = 65 kg
(160, 55, 50),  -- Example data: height = 160 cm, weight = 55 kg, target weight = 50 kg
(180, 85, 80),  -- Example data: height = 180 cm, weight = 85 kg, target weight = 80 kg
(165, 60, 58),  -- Example data: height = 165 cm, weight = 60 kg, target weight = 58 kg
(170, 75, 70);  -- Example data: height = 170 cm, weight = 75 kg, target weight = 70 kg
