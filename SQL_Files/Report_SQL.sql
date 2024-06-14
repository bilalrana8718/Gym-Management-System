use Project;

-------------------- query 1 -----------------------
CREATE OR Alter VIEW member_appointment As
select member.MemberID as memberid, Name as name, Height, Weight, Gender, member.Email as email, trainerId, member.GymId as gymId
from Member
inner join Apppointment
on Apppointment.MemberId = member.MemberID;


CREATE OR ALTER PROCEDURE Query1 @Gym_name varchar(20), @Trainer_name varchar(30)
As
Begin
	
	select name, Height, Weight, Gender, email
	from member_appointment
	where TrainerID = (select TrainerID from Trainer where name = @Trainer_name)
	and gymId = (select gymId from Gym where name = @Gym_name)

End


Exec Query1 @Gym_name = 'fitZone', @Trainer_name = 'afnan'


----------------query 2------------
CREATE OR Alter VIEW member_dietPlan As
select Member.Name as name, Height, Weight, Gender, member.Email as email, DietPlanId, member.GymId as gymId
from Member
inner join Select_DietPlan
on Select_DietPlan.MemberId = member.MemberID;


CREATE OR ALTER PROCEDURE Query2 @Gym_name varchar(20), @Diet_name varchar(30)
As
Begin
	
	select name, Height, Weight, Gender, email
	from member_dietPlan
	where member_dietPlan.DietPlanId = (select DietPlanID from DietPlan where DietPlan.DietPlan_Name = @Diet_name)
	and gymId = (select gymId from Gym where Gym.Gym_Name = @Gym_name)

End

Exec Query2 @Gym_name = 'fitZone', @Diet_name = 'Lean and Fit'



----------------query 3------------
CREATE OR Alter VIEW member_dietPlan_Trainer As
select name, Height, Weight, Gender, email, trainerid, DietPlanId
from member_appointment
inner join Select_DietPlan
on Select_DietPlan.MemberId = member_appointment.memberID;


CREATE OR ALTER PROCEDURE Query3 @trainer_name varchar(20), @Diet_name varchar(30)
As
Begin
	
	select name, Height, Weight, Gender, email
	from member_dietplan_Trainer
	where DietplanID = (select DietPlanID from DietPlan where DietPlan.DietPlan_Name = @Diet_name)
	and TrainerID = (select TrainerID from Trainer where Trainer.Name = @Trainer_name)

End


EXEC Query3 @trainer_name = 'John Smith', @Diet_name = 'Lean and Fit'



------------------query 5--------------------	
create or alter view dietplanMeals_Meal_calories as
select DietplanID, (carbs+protein+fats+fibre) as calories 
from DietPlanMeals
inner join Meal
on DietPlanMeals.MealID = meal.MealID
where Time_ = 'Breakfast'

create or alter procedure query5
AS
Begin
	select DietPlan.DietPlanID, DietPlan.DietPlan_Name, calories
	from DietPlan
	inner join dietplanMeals_Meal_calories
	on dietplanMeals_Meal_calories.DietPlanID = DietPlan.DietPlanID
	where calories < 500
End

Exec query5

-----------------query 6--------------

create or alter view dietplanMeals_Meal_Carbs as
select DietplanID, carbs 
from DietPlanMeals
inner join Meal
on DietPlanMeals.MealID = meal.MealID

create or alter procedure query6
AS
Begin
	select dietplanid, sum(carbs) as total_carbs
	from dietplanMeals_Meal_Carbs
	group by DietPlanID
	having sum(carbs) < 300
end

Exec query6

---------------query 7--------------

create or alter view exercise_set_rep_machine as
select Machine, exercise_set_repID
from exercise_set_rep
inner join Exercise
on exercise_set_rep.ExerciseID = Exercise.ExerciseID

create or alter view machine_workoutplan_exercise as
select Machine, WorkoutPlanID
from WorkoutPlan_Exercise
inner join exercise_set_rep_machine
on exercise_set_rep_machine.exercise_set_repID = WorkoutPlan_Exercise.WorkoutPLanID

CREATE OR ALTER PROCEDURE Query7 @machine_name varchar(20)
As
Begin
		select WorkoutPlanId, PlanName
		from WorkoutPlan
		where WorkoutPLanID not in (select WorkoutPlanID from machine_workoutplan_exercise where Machine = @machine_name)
End

Exec Query7 @machine_name = 'Treadmill'


------------------------query 8--------------------
--diet plans which have "grilled chicken salad" as one of its meals

select * from meal
select * from dietplan
select * from DietPlanMeals

create or alter view dietplan_DietPlanMeals as
select dietplan.DietPlanID  as dietid, dietplan.DietPlan_Name as dietname, DietPlanMeals.MealID as mealid
from DietPlan
inner join DietPlanMeals
on dietplan.DietPlanID = DietPlanMeals.DietPlanID

create procedure query8 @Meal_name varchar(40)
as 
Begin
	
		select dietplan_DietPlanMeals.DietID, dietplan_DietPlanMeals.dietname
		from dietplan_DietPlanMeals
		where dietplan_DietPlanMeals.mealid = (select meal.mealId from Meal where Meal.name = @Meal_name)
	
end

EXEC query8 @Meal_name = 'Grilled Chicken Salad'

----------------------query 09----------------
Select Member.Name, Member.Gender, Member.Email , Member_Report.total_time
from Member, Member_Report
where Member.MemberID = Member_Report.memberId and total_time < 3;

----------------------query 10----------------
create or alter procedure query10
as
begin
		select member.GymId, Gym.Gym_Name, count(member.MemberID) as total_Members
		from member
		inner join gym
		on member.GymID = gym.GymID
		group by member.GymID, gym.Gym_Name
end

EXEC query10



-- 11
Select gym.GymID, gym.Gym_Name, (Gym_Performance.Revenue * Gym_Performance.TotalMembers) as Total_Revenue
from Gym, Gym_Performance
where gym.GymID = 3 and Gym.PerformanceID = Gym_Performance.PerformanceID;


-- 12
Select Trainer.TrainerID, Trainer.Name, Trainer.Experience
From Trainer
where Experience > 5;


--13
Select top 5 gym.GymID, gym.Gym_Name, Gym_Performance.Rating
from Gym, Gym_Performance
where Gym.PerformanceID = Gym_Performance.PerformanceID
order by Gym_Performance.Rating desc;


--14
Select Trainer.Name, Trainer.Experience, WorkoutPlan.PlanName, WorkoutPlan.Purpose
from Trainer, WorkoutPlan
where Trainer.TrainerID = WorkoutPlan.TrainerID and WorkoutPlan.Purpose = 'Functional Fitness';


--15
Select Member.MemberID, Member.Name, Member.Gender, Member.Height, Member.Weight, table1.progress
From Member
inner join (Select Workout_Report.memberId, Workout_Report.progress from Workout_Report where Workout_Report.progress > 90) as table1
on Member.MemberID = table1.memberId;

--16
Select Gym.GymID, Gym.Gym_Name, Gym.Location, AVG(Member.Weight) as Average_Weight
from Gym
inner join Member
on Gym.GymID = Member.GymID
group by Gym.GymID, Gym.Gym_Name, Gym.Location; 

--17
Select Table_1.MemberID, Table_1.Name, Table_1.Email
From	(Select Member.MemberID, Member.Name, Member.Email, Workout_Report.progress
		From Member
		inner Join Workout_Report
		on Member.MemberID = Workout_Report.memberId
		where Workout_Report.progress > 80) as Table_1
inner join 
(Select DietPlan.DietPlanID, DietPlan.Purpose, Select_dietplan.MemberID
		from DietPlan
		inner join select_dietplan
		on DietPlan.DietPlanID = select_dietplan.DietPlanID
		where Purpose = 'Muscle Building') as Table_2
on Table_1.MemberID = Table_2.MemberID;


--18
Select Member.Name, Member.Gender,Member.Height, Member.Weight, Member_Report.progress, Member_Report.total_time
From Member, Member_Report
where Member.MemberID = Member_Report.memberId and Member_Report.progress > 80;


--19
Select Trainer.Name, COUNT(Apppointment.TrainerID) as Number_of_Appointments
from Trainer
inner join Apppointment
on Trainer.TrainerID = Apppointment.TrainerID
group by Trainer.TrainerID, Trainer.Name;


--20
Select top 1 Trainer.TrainerID, Trainer.Name, Trainer.Experience, AVG(Feedback.rating) AS Average_Rating
From Trainer
inner join Feedback
on Trainer.TrainerID = Feedback.TrainerID
group by Trainer.TrainerID, Trainer.Name, Trainer.Experience
order by Average_Rating desc;






