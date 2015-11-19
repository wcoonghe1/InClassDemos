<Query Kind="Statements">
  <Connection>
    <ID>15120971-3f94-4230-b577-cc7bf5204633</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

//question 1
from SkillRow in Skills
where SkillRow.RequiresTicket == true
orderby SkillRow.Description
select new 
{
	Description = SkillRow.Description,
	Employees = from EmployeeRow in EmployeeSkills
				where EmployeeRow.SkillID == SkillRow.SkillID
				orderby EmployeeRow.YearsOfExperience descending
				select new
				{
					Name = EmployeeRow.Employee.FirstName + " " + EmployeeRow.Employee.LastName,
					Level = EmployeeRow.Level,
					YearsExperiance = EmployeeRow.YearsOfExperience
				}
}

//question 2
from SkillRow in Skills
orderby SkillRow.Description
select new
{
	
	Skill = SkillRow.Description
	
}

//questoin 3
from skill in Skills
where !(from skillid in EmployeeSkills   
            select skillid.SkillID)    
           .Contains(skill.SkillID)
select new 
{
	Skill = skill.Description
}


//question 4

var results3 = 
(from s in Shifts
			  where s.PlacementContractID == 3
			  group s.NumberOfEmployees by s.DayOfWeek into g
              select new 
			  { 			  				  
			  	DayOfWeek = g.Key,
				NumberOfEmployee = g
			  });
			  
results3.Dump();

//question 5
from emp in EmployeeSkills
where emp.YearsOfExperience == EmployeeSkills.Max(x =>x.YearsOfExperience)
select new
{
	name = emp.Employee.FirstName + " " + emp.Employee.LastName	
}