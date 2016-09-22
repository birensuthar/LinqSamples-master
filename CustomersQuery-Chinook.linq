<Query Kind="Expression">
  <Connection>
    <ID>bcf2ad41-de52-4d5e-880d-e4dded70e4f0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//List all the customers served by employee named "Jane Peacock show the customer Last Name, First Name, City, State, Phone, Email"
//Can use either the customer table or the employee table. But you should use the customer table since most of the data is coming from the customer table.

//Query Syntax
//Sample for entity subset
//Sample of entity navigation from child to parent on Where
//reminder that code is C# and thus appropriate methods can be used .Equals
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") && x.SupportRepIdEmployee.LastName.Equals("Peacock")
select new
{
		Name = x.LastName + ", " + x.FirstName,
		City = x.City,
		State = x.State,
		Phone = x.Phone,
		Email = x.Email
}

//Use of aggregates in Queries.
//List all album Titles and the # of tracks for the album. List Titles alphabetically.
//What is the total track price for the album.

//Count() count the number of instances of the collection reference.
//Sum() totals a specific field. Thus you will likely need to use a delegate to indicate the collection instant attribute to be used.
//Average()
from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new
{
	Title = x.Title,
	NumberofAlbumTracks = x.Tracks.Count(),
	TotalAlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
	AvgTrackLengthInSeconds = x.Tracks.Average(y => y.Milliseconds/1000)
}