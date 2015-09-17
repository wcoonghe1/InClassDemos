<Query Kind="Expression">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//orderby

from food in Items
orderby food.Description
select food


from food in Items
orderby food.CurrentPrice descending, food.Calories ascending 
select food

//combine where and orderby
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending 
where food.MenuCategory.Description.Equals("Entree")
select food
