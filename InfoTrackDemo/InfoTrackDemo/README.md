# InfoTrackSearch && For React Component Test Demo in ./CLientApp
1. ToDo list test

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


# About the Requirement
InfoTrack Development Project
To gain a better understanding of your technical, design and logic skills, we would like you to create a web-based project using C# with the following requirements.
Task
The CEO from InfoTrack is very interested in SEO and how this can improve Sales. Every morning he logs in to his favourite search engine and types in the same key words "online title search" and counts down to see where and how many times their company, https://www.infotrack.com.au sits on the list.
To make this task less tedious the CEO reaches out to you to write a small web-based application for him that will automatically perform this operation and return the result to the screen. The application prompts for a string of keywords to search, and a URL to find in the search results. The input values are then processed to return a string of numbers for where the URL is found in the search engine’s results.
For example, "1, 10, 33" or "0".
The CEO is only interested if their URL appears in the first 50 results.
Files
For the results to be consistently reproduced we have created a library of static web pages for you to use in place of a live search engine. (We have done the “Online Title Search” search part for you, but please still include the input on the UI and pass it back to the server)
The static pages can be found at: https://infotrack-tests.infotrack.com.au/Google/Page{01-10}.html https://infotrack-tests.infotrack.com.au/Bing/Page{01-10}.html Example: https://infotrack-tests.infotrack.com.au/Google/Page01.html https://infotrack-tests.infotrack.com.au/Google/Page02.html
Candidates
Junior Level:
We know you are new to this so give us your best effort. We look forward to seeing what you can deliver. We prefer that you do not use any 3rd party libraries for scraping the HTML pages. For your test you only need to search the Google pages. Feel free to have a go at the mid/senior requirements below
Mid Level:
You have been at this for a while now, so we want you to follow the guidelines given to the juniors and also show that you know how to structure, decouple and test your code. Showcase what you have learnt thus far and feel free to have a go at some of the senior requirements below.
Seniors:
Follow the guidelines given above. In addition provide a solution that uses Ajax calls from the UI to the server (no full post backs). We would also like you to search both Google and Bing pages and show us that you know how to use design patterns and the SOLID design principle to structure and write your code. Your app should be able to easily add another search engine if needed.
   
Submissions
Please complete the project and send a link to where we can download your code to your contact at InfoTrack. If you have any questions, please do not hesitate to contact us. 