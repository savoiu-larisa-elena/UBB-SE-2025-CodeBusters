use [MealPlanner]

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
	water_intake float
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
	i_id int foreign key references ingredients(i_id)
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
	sugar float
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