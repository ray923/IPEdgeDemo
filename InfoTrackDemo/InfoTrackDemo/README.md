# InfoTrackSearch

# Requirement

A small web-based application for him that will automatically perform this operation and return the result to the screen. The application prompts for a string of keywords to search, and a URL to find in the search results. The input values are then processed to return a string of numbers for where the URL is found in the search engine’s results.

# Design
1. Using .net Core as backend to request the static web pages, processed the response
2. Filter and remove the ads and vedio blocks etc. Split the true results in to list and anchor the result's title, content block. 
3. Compare the url to find whether url appears in each result.
4. Assemble the response date model which contain: SearchEngine name, appears at the which number of result, appears place of the result (in title or content).
5. Using react hook and typescript to write the front end
6. Writing own ajax calls function (senior) to send and get the post data.

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


