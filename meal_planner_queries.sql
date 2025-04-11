use [Meal_Planner];

create table goals (
	g_id int primary key identity (1,1),
	g_description varchar(100) not null
)

create table cooking_skills (
	cs_id int primary key identity(1,1),
	cs_description varchar(150) not null
)

create table allergies (
	a_id int primary key identity(1,1),
	a_description varchar(150) not null
)

create table dietary_preferences (
	dp_id int primary key identity(1,1),
	dp_description varchar(150) not null
)

create table activity_levels (
	al_id int primary key identity(1,1),
	al_description varchar(150) not null
)

create table users (
	u_id int primary key identity (1,1),
	u_name varchar(150) not null,
	dob date,
	email varchar(250),
	u_height float not null,
	u_weight float not null,
	g_id int foreign key references goals(g_id),
	cs_id int foreign key references cooking_skills(cs_id),
	dp_id int foreign key references dietary_preferences(dp_id),
	a_id int foreign key references allergies(a_id),
	al_id int foreign key references activity_levels(al_id),
	target_weight float
)

create table calorie_trackers(
	u_id int foreign key references users(u_id),
	today date,
	daily_intake float,
	calories_consumed float,
	calories_burned float
)

create table water_trackers(
	u_id int foreign key references users(u_id),
	water_intake float,
    water_goal float
)

create table ingredients (
	i_id int primary key identity(1,1),
	u_id int foreign key references users(u_id), -- can be null
	i_name varchar(150) not null,
	category varchar(150) not null,
	protein float,
	calories float,
	carbohydrates float,
	fat float,
	fiber float,
	sugar float
)

create table grocery_lists(
	u_id int foreign key references users(u_id),
	i_id int foreign key references ingredients(i_id),
	is_checked bit
)

create table meal_types(
	mt_id int primary key identity(1,1),
	m_description varchar(150) not null
)

create table meals(
	m_id int primary key identity(1,1),
	u_id int foreign key references users(u_id), --can be null
	m_name varchar(150) not null,
	recipe varchar(2000) not null,
	cs_id int foreign key references cooking_skills(cs_id),
	dp_id int foreign key references dietary_preferences(dp_id),
	mt_id int foreign key references meal_types(mt_id),
	preparation_time float,
	servings float,
	protein float,
	calories float,
	carbohydrates float,
	fat float,
	fiber float,
	sugar float,	
	photo_link varchar(1000)
)

create table meal_ingredient(
	m_id int foreign key references meals(m_id),
	i_id int foreign key references ingredients(i_id),
	quantity float
)

create table favourite_meal(
	m_id int foreign key references meals(m_id),
	u_id int foreign key references users(u_id),
	is_fav bit
)

create table calorie_trackers(
	u_id int foreign key references users(u_id),
	today date,
	daily_intake float, -- goal
	calories_consumed float, -- food
	calories_burned float -- exercise
)

CREATE TABLE serving_units (
    unit_name VARCHAR(50) PRIMARY KEY,  -- Unique key for each unit, 'unit_name' as the primary key
);

CREATE TABLE daily_meals (
    dm_id INT PRIMARY KEY IDENTITY(1,1),  -- Unique entry for each meal logged
    u_id INT NULL,  -- The user who ate the meal, now optional (can be NULL)
    m_id INT NOT NULL FOREIGN KEY REFERENCES meals(m_id),  -- The meal they ate
    date_eaten DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE), -- The day they ate it
    servings FLOAT NOT NULL DEFAULT 1,  -- How many servings were eaten
    unit_name VARCHAR(50) NOT NULL,  -- The serving unit type (now referring to 'unit_name' from 'serving_units')
    
    -- Foreign key reference to the serving_units table using unit_name
    FOREIGN KEY (unit_name) REFERENCES serving_units(unit_name),

    total_calories FLOAT,  -- Total calories based on servings
    total_protein FLOAT,   -- Total protein based on servings
    total_carbohydrates FLOAT,  -- Total carbohydrates based on servings
    total_fat FLOAT,       -- Total fat based on servings
    total_fiber FLOAT,     -- Total fiber based on servings
    total_sugar FLOAT,     -- Total sugar based on servings
    FOREIGN KEY (u_id) REFERENCES users(u_id)  -- User foreign key can be null now
);

-- INSERTS

use [Meal_Planner];

insert into goals(g_description) values 
('Lose weight'),
('Gain weight'),
('Maintain weight'),
('Body recomposition'),
('Improve overall health');

insert activity_levels (al_description) values 
('Sedentary'),
('Lightly Active'),
('Moderately Active'),
('Very Active'),
('Super Active');

insert into cooking_skills (cs_description) values 
('I am a beginner'),
('I cook sometimes'),
('I love cooking'),
('I prefer quick meals'),
('I meal prep');

insert into dietary_preferences (dp_description) values
('None'),
('Mediterranean'),
('Low-Fat'),
('Diabetic-Friendly'),
('Kosher'),
('Halal');

insert into allergies (a_description) values
('None'),
('Peanuts'),
('Tree Nuts'),
('Dairy'),
('Eggs'),
('Gluten'),
('Shellfish'),
('Soy'),
('Fish'),
('Sesame');

INSERT INTO meal_types (m_description) VALUES 
('Breakfast'),
('Lunch'),
('Dinner'),
('Snack'),
('Dessert');

INSERT INTO meals (m_name, recipe, cs_id, dp_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) VALUES 
('Tomato and Feta Baked Eggs', 'Bake eggs with tomatoes and feta cheese.', 2, 1, 1, 25, 2, 20, 350, 15, 10, 3, 5, 'https://hips.hearstapps.com/hmg-prod/images/ghk050124tomatofetabakedeggs-66194dcb8115d.jpg?crop=1.00xw:0.669xh;0,0.221xh&resize=1200:*'),
('Double Apple Baked Oatmeal', 'Mix oats, apples, and bake.', 1, 2, 1, 30, 4, 10, 280, 40, 5, 6, 12, 'https://hips.hearstapps.com/hmg-prod/images/double-apple-baked-oatmeal-64c95ef3000a6.jpg?crop=1.00xw:0.668xh;0,0.0932xh&resize=1200:*'),
('Sweet Potato Breakfast Burritos', 'Wrap scrambled eggs and sweet potatoes in a tortilla.', 3, 1, 1, 20, 2, 18, 400, 35, 12, 7, 6, 'https://hips.hearstapps.com/hmg-prod/images/sweet-potato-breakfast-burritos-64fa102ec946c.jpg?crop=0.901xw:0.602xh;0.0815xw,0.165xh&resize=1200:*'),
('Loaded Pancake Tacos', 'Make pancakes and use them as taco shells with fillings.', 2, 2, 2, 15, 2, 12, 450, 55, 10, 5, 9, 'https://hips.hearstapps.com/hmg-prod/images/loaded-pancake-tacos-1675720416.jpg?crop=0.669xw:1.00xh;0.172xw,0&resize=1200:*'),
('Sausage and Egg Sandwiches', 'Cook sausage and eggs, place in a bun.', 2, 3, 2, 10, 1, 22, 500, 45, 15, 4, 2, 'https://hips.hearstapps.com/hmg-prod/images/sausage-and-egg-sandwiches-1647548891.jpg?crop=1xw:0.9989806320081549xh;center,top&resize=1200:*'),
('Rice With Salmon and Broccoli', 'Cook rice and serve with grilled salmon and steamed broccoli.', 4, 3, 3, 25, 2, 40, 600, 50, 15, 7, 4, 'https://hips.hearstapps.com/hmg-prod/images/creamy-kale-pasta-6400be3282ed9.jpg?crop=0.738xw:0.493xh;0,0.311xh&resize=1200:*'),
('Sheet Pan Roasted Chicken', 'Roast chicken with vegetables on a sheet pan.', 3, 1, 3, 35, 3, 50, 700, 20, 20, 6, 3, 'https://hips.hearstapps.com/hmg-prod/images/easy-rice-with-ginger-soy-salmon-and-broccoli-64c95ab03fbfe.jpg?crop=1.00xw:0.670xh;0,0.190xh&resize=1200:*'),
('Air Fryer Strawberry-Thyme Scones', 'Mix dough with strawberries and bake in an air fryer.', 1, 4, 4, 20, 6, 6, 250, 35, 8, 2, 15, 'https://hips.hearstapps.com/hmg-prod/images/sheet-pan-roasted-chicken-64d1462c915de.jpg?crop=0.881xw:0.588xh;0.119xw,0.258xh&resize=1200:*'),
('Manchego Devils on Horseback', 'Wrap Manchego cheese with bacon and bake.', 2, 2, 4, 15, 4, 14, 300, 10, 18, 3, 2, 'https://hips.hearstapps.com/hmg-prod/images/fall-mushroom-pizza-1638485679.jpg?crop=0.923xw:0.616xh;0.0765xw,0.136xh&resize=1200:*'),
('Creamy Kale Pasta', 'Cook pasta and mix with creamy kale sauce.', 3, 3, 3, 25, 2, 25, 480, 50, 12, 5, 6, 'https://hips.hearstapps.com/hmg-prod/images/strawberry-thyme-mini-scones-65bd45d31ab00.jpg?crop=0.857xw:0.572xh;0.0816xw,0.299xh&resize=1200:*'),
('Cheesy Mushroom Pizza', 'Prepare pizza dough, add mushrooms and cheese, then bake.', 3, 4, 3, 30, 3, 28, 550, 60, 18, 4, 7, 'https://hips.hearstapps.com/hmg-prod/images/manchego-devils-on-horseback-654e8ce46109e.jpg?crop=1.00xw:0.667xh;0,0.143xh&resize=1200:*'),
('Parmesan Seeded Crackers', 'Mix dough with parmesan and seeds, bake until crispy.', 2, 5, 4, 20, 8, 5, 200, 25, 8, 3, 10, 'https://hips.hearstapps.com/hmg-prod/images/parmesan-seeded-crackers-1667927056.jpg?crop=1.00xw:0.667xh;0,0.0999xh&resize=1200:*'); 

-- INSERT PROCEDURES

go
create procedure InsertNewUser(@u_name varchar(100))
as
begin
set nocount on;
insert into users(u_name, u_height, u_weight) values (@u_name, 0, 0);
end;

GO
CREATE PROCEDURE InsertMealIngredient
    @mealId INT,
    @ingredientId INT,
    @quantity FLOAT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO meal_ingredient (m_id, i_id, quantity)
    VALUES (@mealId, @ingredientId, @quantity);
    RETURN @@ROWCOUNT;
END;
GO

CREATE PROCEDURE sp_GetUserGroceryList
    @UserId INT
AS
BEGIN
    SELECT g.i_id, i.i_name, g.is_checked
    FROM grocery_lists g
    JOIN ingredients i ON g.i_id = i.i_id
    WHERE g.u_id = @UserId
END

GO
CREATE OR ALTER PROCEDURE sp_AddIngredientToUserList
    @UserId INT,
    @IngredientName NVARCHAR(255)
AS
BEGIN
    DECLARE @IngredientId INT;

    SELECT @IngredientId = i_id FROM ingredients WHERE i_name = @IngredientName;
    
	IF @IngredientId IS NULL
    BEGIN
        INSERT INTO ingredients(i_name, category) VALUES (@IngredientName, 'Uncategorized');
        SET @IngredientId = SCOPE_IDENTITY();
    END

    IF NOT EXISTS (
        SELECT 1 FROM grocery_lists WHERE u_id = @UserId AND i_id = @IngredientId
    )
    BEGIN
        INSERT INTO grocery_lists(u_id, i_id, is_checked)
        VALUES (@UserId, @IngredientId, 0);
    END

    SELECT @IngredientId AS i_id;
END

exec InsertNewUser @u_name = 'Andrei Bob'
select * from users

-- GETTERS

go
create function GetUserByName(@u_name nvarchar(500))
--returns the id of the user
returns int
as
begin
	declare @u_id int
	select @u_id = u_id from Users where u_name = @u_name
	return @u_id
end

go
create function GetIngredientByName(@i_name nvarchar(50))
--returns the id of the ingredient
returns int
as
begin
	declare @i_id int
	select @i_id = i_id from Ingredients where i_name = @i_name
	return @i_id
end

go
create function GetAllIngredients()
--returns all the ingredients
returns table
as
return (select * from Ingredients)

go
create function GetGoalByDescription(@g_description nvarchar(100))
--returns the id of the goal
returns int
as
begin
	declare @g_id int
	select @g_id = g_id from goals where g_description = @g_description
	return @g_id
end

go
create function GetActivityByDescription(@a_description nvarchar(100))
--returns the id of the activity
returns int
as
begin
	declare @a_id int
	select @a_id = al_id from activity_levels where al_description = @a_description
	return @a_id
end	

go
create function GetCookingSkillByDescription(@cs_description nvarchar(150))
--returns the id of the cooking skill
returns int
as
begin
	declare @cs_id int
	select @cs_id = cs_id from cooking_skills where cs_description = @cs_description
	return @cs_id
end

go
create function GetDietaryPreferencesByDescription(@dp_description nvarchar(150))
--returns the id of the dietary preferences
returns int
as
begin
	declare @dp_id int
	select @dp_id = dp_id from dietary_preferences where dp_description = @dp_description
	return @dp_id
end

go
create function GetAllergiesByDescription(@a_description nvarchar(150))
----returns the id of the allergy
returns int
as 
begin
	declare @a_id int
	select @a_id = a_id from allergies where a_description = @a_description
	return @a_id
end

go
CREATE PROCEDURE GetMealsByCategory
    @category VARCHAR(50)
AS
BEGIN
    SELECT 
        m.m_name as Name,
        m.recipe as Recipe,
        m.calories as Calories,
        mt.m_description as Category,
        m.protein as Protein,
        m.carbohydrates as Carbohydrates,
        m.fat as Fat,
        m.fiber as Fiber,
        m.sugar as Sugar,
        m.photo_link as PhotoLink,
        m.preparation_time as PreparationTime,
        m.servings as Servings
    FROM meals m
    JOIN meal_types mt ON m.mt_id = mt.mt_id
    WHERE mt.m_description = @category;
END

-- UPDATES

go
create procedure UpdateUserBodyMetrics(@u_id int, @u_weight float, @u_height float, @u_goal float)
as
--if the user is newly created the bodymetrics are going to be null but here this is changed
begin 
set nocount on;
update users set  
	u_weight = @u_weight,
	u_height = @u_height,
	target_weight = @u_goal
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserGoals(@u_id int, @g_id int)
as
begin
set nocount on;
update users set g_id = @g_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserActivity(@u_id int, @a_id int)
as
begin
set nocount on;
update users set al_id = @a_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserCookingSkill(@u_id int, @cs_id int)
as
begin
set nocount on;
update users set cs_id = @cs_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserDietaryPreference(@u_id int, @dp_id int)
as
begin
set nocount on;
update users set dp_id = @dp_id 
where u_id = @u_id;
return @@ROWCOUNT
end;

go
create procedure UpdateUserAllergies(@u_id int, @a_id int)
as
begin
set nocount on;
update users set a_id = @a_id 
where u_id = @u_id;
return @@ROWCOUNT
end;


GO
CREATE PROCEDURE sp_UpdateGroceryItemChecked
    @UserId INT,
    @IngredientId INT,
    @IsChecked BIT
AS
BEGIN
    UPDATE grocery_lists
    SET is_checked = @IsChecked
    WHERE u_id = @UserId AND i_id = @IngredientId
END

select * from users
delete from users
exec InsertNewUser @u_name = 'Andrei Bob'

create function get_water_intake(@UserId int)
returns float
as
begin
	return (SELECT water_intake FROM water_trackers WHERE u_id = @UserId)
end
go


go
create procedure update_water_intake(@UserId int, @NewIntake float)
as
begin
	UPDATE water_trackers SET water_intake = @NewIntake WHERE u_id = @UserId
end
go

go
create function get_water_goal(@UserId int)
returns float
as
begin
	return (select water_goal from water_trackers where u_id = @UserId)
end
go

create function get_calorie_goal(@UserId int)
returns float
as
begin
	return (SELECT daily_intake FROM calorie_trackers WHERE u_id = @UserId)
end
go

go
create function get_calorie_exercise(@UserId int)
returns float
as
begin
	return (SELECT calories_burned FROM calorie_trackers WHERE u_id = @UserId)
end
go

GO
CREATE OR ALTER FUNCTION get_calorie_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalCal FLOAT;
    SELECT @TotalCal = SUM(total_calories) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalCal;
END
GO

SELECT dbo.get_calorie_food(1)

GO
CREATE OR ALTER FUNCTION get_protein_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalProtein FLOAT;
    SELECT @TotalProtein = SUM(total_protein) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalProtein;
END
GO

GO
CREATE OR ALTER FUNCTION get_carbohydrates_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalCarbs FLOAT;
    SELECT @TotalCarbs = SUM(total_carbohydrates) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalCarbs;
END
GO

GO
CREATE OR ALTER FUNCTION get_fat_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalFat FLOAT;
    SELECT @TotalFat = SUM(total_fat) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalFat;
END
GO

GO
CREATE OR ALTER FUNCTION get_fiber_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalFiber FLOAT;
    SELECT @TotalFiber = SUM(total_fiber) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalFiber;
END
GO

GO
CREATE OR ALTER FUNCTION get_sugar_food(@UserId INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalSugar FLOAT;
    SELECT @TotalSugar = SUM(total_sugar) 
    FROM daily_meals 
    WHERE u_id = @UserId 
      AND CONVERT(DATE, date_eaten) = CONVERT(DATE, GETDATE());
    RETURN @TotalSugar;
END
GO



CREATE OR ALTER PROCEDURE dbo.get_last_6_unique_meals_pr
    @UserId INT
AS
BEGIN
    SELECT TOP 6 
        m.m_name AS RecipeName,
        dm.total_calories AS Calories,
        dp.dp_description AS Diet,  -- Fetching diet name
        cs.cs_description AS Level,  -- Fetching cooking skill level name
        m.preparation_time AS Time,  -- Fetching preparation time from meals
        mt.m_description AS MealType  -- Fetching meal type name
    FROM daily_meals dm
    JOIN meals m ON dm.m_id = m.m_id
    JOIN dietary_preferences dp ON m.dp_id = dp.dp_id  -- Join for diet name
    JOIN cooking_skills cs ON m.cs_id = cs.cs_id  -- Join for cooking skill level name
    JOIN meal_types mt ON m.mt_id = mt.mt_id  -- Join for meal type name
    WHERE dm.u_id = @UserId
    ORDER BY dm.date_eaten DESC, dm.dm_id DESC
END