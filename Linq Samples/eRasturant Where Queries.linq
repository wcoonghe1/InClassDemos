<Query Kind="Expression">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//where claus

//list all tables that hold more than 3 people
//query syntax

from row in Tables
where row.Capacity > 3
select row

//methond syntax
Tables.Where(row => row.Capacity>3)

//list all items with more than  500 calories

from row in Items
where row.Calories > 500
select row

//list all items with more than  500 calories and selling for more than $10

from row in Items
where row.Calories > 500 && row.CurrentPrice > 10.00m
select row

//list all ited witn more than 500 calories and are entrees on the menue

from food in Items
where food.Calories > 500 &&
		food.MenuCategory.Description.Equals("Entree")
select food