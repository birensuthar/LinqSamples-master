<Query Kind="Statements">
  <Connection>
    <ID>8ef1b04e-0e18-4640-9787-aed2cef41ae8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var maxbills = (from x in Waiters
					select x.Bills.Count()).Max();
					
//maxbills.Dump();

var BestWaiter = from x in Waiters
					//where x.Bills.Count() == maxbills
					select new
					{
						Name = x.FirstName + " " + x.LastName,
						tbills = x.Bills.Count()
						//phone = x.Phone,
						//address = x.Address,
						//hiredate = x.HireDate,
						//releaseDate = x.ReleaseDate
					};
BestWaiter.Dump();

//Create a dataset that contains the summary bill info by the waiter
var WaiterBills = from x in Waiters
				orderby x.LastName, x.FirstName
				select new
				{
					Name = x.LastName + ", " + x.FirstName,
					BillInfo = (
									from y in x.Bills
									where y.BillItems.Count() > 0
									select new
									{
											BillID = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
									}		
								)
				};
WaiterBills.Dump();