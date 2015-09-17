<Query Kind="Expression">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//groupby

from food in Items
group food by food.MenuCategory.Description
//this creates a key with a value and the row collectoin for that value

//more than one field
from food in Items
group food by new{food.MenuCategory.Description, food.CurrentPrice)

//anonimus data types

from food in Items
where food.MenuCategory.Description.Equals("Entree")
&& food.Active
orderby food.CurrentPrice descending
select new
	{
	Description = food.Description,
	Price = food.CurrentPrice,
	Cost = food.CurrentCost,
	Profit =food.CurrentPrice - food.CurrentCost
	}
	