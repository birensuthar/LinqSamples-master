<Query Kind="Expression">
  <Connection>
    <ID>8ef1b04e-0e18-4640-9787-aed2cef41ae8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//Multi column group.
//Grouping data placed in a local temp dataset for further processing.
//.Key allows you to have access to the value(s) in your group key(s)
//If you have multiple group columns they must be in an anonymous datatype
//To create a DTO type collection you can use .ToList() on the temp data set
//You can have a custom anonymous data collection by using a nestes query.

from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategoryID = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItems = from x in tempdataset
							select new{
										ItemID = x.ItemID,
										FoodDescription = x.Description,
										TimesServed = x.BillItems.Count()
							}
				}