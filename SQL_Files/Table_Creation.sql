

use Project;

Create Table Owner(
	OwnerID int primary key,
	Name Varchar(30),
	Email Varchar(50),
	Password Varchar(20)
);

Create Table Admin(
	AdminID int primary key,
	Name Varchar(30),
	Email Varchar(50),
	Password Varchar(20),
	
);

Create table Gym_Performance(
	PerformanceID int primary key,
	TotalMembers int,
	Active_Attendence int,
	Revenue int,
	Rating int
);

Create Table Gym(
	GymID int primary key,
	Gym_Name varchar(20),
	Location varchar(30),
	PerformanceID int foreign key references Gym_Performance(PerformanceID),
	OwnerID int foreign key references Owner(OwnerID),
	AdminID int foreign key references Admin(AdminID)
);

Create Table Trainer(
	TrainerID int primary key,
	Name Varchar(30),
	Email Varchar(50),
	Password Varchar(20),
	Experience int,
	TotalClients int
);


Create Table WorksIn(
	TrainerID int foreign key references Trainer(TrainerID),
	GymID int foreign key references Gym(GymID)
); 


create table Machines(
	MacID int PRIMARY KEY,
	Name varchar(40)
);

Create Table Exercise(
	ExerciseID int primary key,
	Target_Muscle varchar(40),
	Machine varchar(40),
	Rest_Intervals int,
	Experience_Required int
);

Create Table WorkoutPlan(
	WorkoutPLanID int primary key,
	PlanName varchar(50),
	Purpose varchar(50),
	TrainerID int foreign key references  trainer(trainerID) 
);

create table exercise_set_rep(
	exercise_set_repID int,
	exerciseNum INT,
	ExerciseID int foreign key references Exercise(ExerciseID),
	Sets int,
	reps int
	PRIMARY KEY (exercise_set_repID, exerciseNum)
);

create table WorkoutPlan_Exercise(
	exercise_set_repID int,
	exerciseNum int,
	WorkoutPlanID int foreign key references WorkoutPlan(WorkoutPlanID),
	day varchar(10),
	FOREIGN KEY (exercise_set_repID, exerciseNum) REFERENCES exercise_set_rep
      (exercise_set_repID, exerciseNum)
);

Create table DietPlan(
	DietPlanID int primary key,
	DietPlan_Name varchar(50),
	Type_of_Diet varchar(40),
	Purpose varchar(50),
	TrainerID int foreign key references trainer(trainerID)
);

create table Meal(
	MealID int primary key,
	name varchar(60),
	carbs int,
	protein int,
	fats int,
	fibre int,
	portionSize int
);

create table DietPlanMeals(
	DietPlanID int foreign key references DietPlan(DietPlanID),
	MealID int foreign key references Meal(MealID),
	Time_ Varchar(15)
);

CREATE TABLE Member(
	MemberID int primary key,
	Name Varchar(30),
	Height int,
	Weight decimal (10, 2),
	Gender Varchar(15),
	Email Varchar(50),
	Password Varchar(20),
	GymID int foreign key references Gym(GymID)
);

create table Workout_Report(
	ReportID int primary key,
	progress int ,
	total_time int,
	memberId int foreign key references Member(MemberID)
);

create table Diet_Report(
	ReportID int primary key,
	progress int ,
	total_time int,
	memberId int foreign key references Member(MemberID)
);

create table Member_Report(
	ReportID int primary key,
	progress int ,
	total_time int,
	memberId int foreign key references Member(MemberID)
);



create table Feedback(
	MemberID int foreign key references Member(MemberID),
	TrainerID int foreign key references Trainer(TrainerID),
	rating int,
	Review varchar(100)
);

create table Apppointment(
	AppointmentID int primary key,
	MemberID int foreign key references Member(MemberID),
	TrainerID int foreign key references Trainer(TrainerID),
	GymID int foreign key references Gym(GymID), 
	Start_Time time,
	End_Time time
);

