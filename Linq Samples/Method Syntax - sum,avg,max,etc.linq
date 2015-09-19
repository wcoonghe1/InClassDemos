<Query Kind="Expression">
  <Connection>
    <ID>3f2d80f1-b91b-4513-b183-c697fb731a97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//find the avarage paind bill
(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Average()

//what is the avarage number of items per paind bill
//we need a list of numbers representing the items per bill
//then take an avarage of the list

(from customer in Bills
where customer.PaidStatus
select customer.BillItems.Count()).Average()

