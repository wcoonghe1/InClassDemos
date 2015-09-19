<Query Kind="Program">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	////groupby
	//
	//from food in Items
	//group food by food.MenuCategory.Description
	////this creates a key with a value and the row collectoin for that value
	//
	////more than one field
	//from food in Items
	//group food by new(food.MenuCategory.Description, food.CurrentPrice)
	//
	////anonimus data types
	
	var results = from food in Items
				where food.MenuCategory.Description.Equals("Entree")
						&& food.Active
				orderby food.CurrentPrice descending
				select new FoodMargins()
				{
				Description = food.Description,
				Price = food.CurrentPrice,
				Cost = food.CurrentCost,
				Profit =food.CurrentPrice - food.CurrentCost
				};
		results.Dump();
		
		// get all the bill and bill items for waiters in spe of 2014. get only those bills which were paid
		
		var results2 = from orders in Bills 
					where orders.PaidStatus && (orders.BillDate.Month == 9 && orders.BillDate.Year == 2014)
					orderby orders.Waiter.LastName, orders.Waiter.FirstName
					select new
					{
						BillID = orders.BillID, 
						WaiterName = orders.Waiter.LastName + ", " + orders.Waiter.FirstName,
						Orders = orders.BillItems					
					};
			results2.Dump();
					
		
	} //end of program
		//define other classes and methods here
	public class FoodMargins //POCO date set, premitive data sets,Flat
				{
					public string Description {get;set;}
					public decimal Price {get;set;}
					public decimal Cost {get;set;}
					public decimal Profit {get;set;}
				}
	

//this is a  DTO class
public class BillOrders
				{
					public int BillID{get;set;}
					public string WaiterName {get;set;}
					public BillItems Orders{get;set;}
				}
