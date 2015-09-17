<Query Kind="Statements">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//simple query syntax
from person in Waiters
select person

//simple method  sytax
Waiters.Select(person => person)

//in side our project we will be writing C# statements
var results = from person in Waiters 
				select person;
				
//to displ;ay the contents of a vatiable in LINQ pad use the .Dump() method
results.Dump();

//implement inside a VS prject's class library BLL method
[DataObjectMethod(DataObjectMEthodType.Select,false)]
public List<Waiter> SomeMethodName()
{
	//you will need to connect to your DAL object , this will be done using a new xxxx() constructor
	//Assume your connection variable is called contextvaribale.

	//do your query
	var results = from person in contextvariable.Waiters 
				select person;
	//return Results 
	return results.ToList();
}