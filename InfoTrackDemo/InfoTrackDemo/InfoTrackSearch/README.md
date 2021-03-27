# InfoTrackSearch
 
# Design patterns
Using simple factory patterns to management the search services.

How to add new search enginge:
1. Create new search service inherit the ISearchService.
2. Add new search engine name in appsettings.json "SearchEngine" keyword.
3. Add new statement in SearchFactory.cs to create the search service instance.

# How to run
Run it in Visual Studio Code:
1. go to InfoTrackSearch/InfoTrackSearch/ClientApp
2. run: npm install in Terminal
3. back to InfoTrackSearch/InfoTrackSearch
4. run: dotnet run in Terminal

Run test cases in Visual Studio Code:
1. go to InfoTrackSearch/SearchServiceTests
2. run: dotnet test in Terminal