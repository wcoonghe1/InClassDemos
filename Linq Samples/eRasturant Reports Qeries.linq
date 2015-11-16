<Query Kind="Expression">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from abillrow in Bills
where abillrow.BillDate.Month == 5
orderby abillrow.BillDate, abillrow.Waiter.LastName, abillrow.Waiter.FirstName
select new 
{
	BillDate = new DateTime(abillrow.BillDate.Year, abillrow.BillDate.Month, abillrow.BillDate.Day),//this removes the Time from datetime
	WaiterName =  abillrow.Waiter.LastName + ", " + abillrow.Waiter.FirstName,
	BillID = abillrow.BillID,
	BillTotal = abillrow.BillItems.Sum(eachbillitemrow => eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
	PartySize = abillrow.NumberInParty,
	Contact = abillrow.Reservation.CustomerName
}