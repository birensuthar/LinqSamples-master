<Query Kind="Program">
  <Connection>
    <ID>8ef1b04e-0e18-4640-9787-aed2cef41ae8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	//A list of bill counts for all waiters.
	//This query will create a flat dataset.
	//The columns are native datatypes (ie: int, string, etc...)
	//One is not concerned with repeated data in a column.
	//Instead of using an anonymous datatype (new{...})
	//we wish to use a refined class definition.
	var BestWaiter = from x in Waiters
					select new WaiterBillCounts
					{
						Name = x.FirstName + " " + x.LastName,
						TCount = x.Bills.Count()
					};
	BestWaiter.Dump();
	
	var paramMonth = 4;
	var paramYear = 2014;
	var WaiterBills = from x in Waiters
				where x.LastName.Contains("k")
				orderby x.LastName, x.FirstName
				select new WaiterBills
				{
					Name = x.LastName + ", " + x.FirstName,
					TotalBillCount = x.Bills.Count(),
					BillInfo = (
									from y in x.Bills
									where y.BillItems.Count() > 0
									where y.BillDate.Month == DateTime.Today.Month - paramMonth 
									&& y.BillDate.Year == paramYear
									select new BillItemSummary
									{
											BillID = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
									}		
								).ToList()
				};
	WaiterBills.Dump();
}

// Define other methods and classes here
//An example of a POCO Class

public class WaiterBillCounts
{
	//Whatever receiving field on your query in your select appers as a property in this class.
	public string Name{get; set;}
	public int TCount{get; set;}
}

public class BillItemSummary
{
	public int BillID {get; set;}
	public DateTime BillDate {get; set;}
	public int? TableID {get; set;}
	public decimal Total {get; set;}
}

//An example of a DTO class (structured)
public class WaiterBills
{
	public string Name{get; set;}
	public int TotalBillCount {get; set;}
	//public IEnumerable<BillItemSummary> BillInfo {get; set;}
	public List<BillItemSummary> BillInfo {get; set;}
}