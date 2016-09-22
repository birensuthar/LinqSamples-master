<Query Kind="Statements">
  <Connection>
    <ID>8ef1b04e-0e18-4640-9787-aed2cef41ae8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//Media types with the most tracks
//When you need to use multiple steps to solve a problem, switch your language choice to either statement(s) or program.
//The results of eeach query will now be saved in a variable. The variable can then be used in future queries.

var maxcount = (from x in MediaTypes
	select x.Tracks.Count()).Max();
//To display the contents of a variable in LinqPad, you use the method .Dump()
maxcount.Dump();


//Use a value in a preceeding create variable
var popularMediaType = from x in MediaTypes
							where x.Tracks.Count() == maxcount
							select new
							{
								Type = x.Name,
								TCount = x.Tracks.Count()
							};
popularMediaType.Dump();

//Can this set of statements be done as one complete query?
//The answer is possibly, and in this case, Yes it can be done.
//b = a / c * (y + z)
//In this example, maxcount could be exchanged for the query that actually created the value in the first place.
//This subsituted query is a subquery.

var popularMediaTypeSubQuery = from x in MediaTypes
							where x.Tracks.Count() == (from y in MediaTypes
														select y.Tracks.Count()).Max()
							select new
							{
								Type = x.Name,
								TCount = x.Tracks.Count()
							};
popularMediaTypeSubQuery.Dump();

//Using the method syntax to determine the count value for the where expression.
//This demonstrates that queries can be constructed using both query syntax and method
var popularMediaTypeSubMethod = from x in MediaTypes
							where x.Tracks.Count() == MediaTypes.Select (mt => mt.Tracks.Count()).Max()
							select new
							{
								Type = x.Name,
								TCount = x.Tracks.Count()
							};
popularMediaTypeSubMethod.Dump();