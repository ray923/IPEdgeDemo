using System;

namespace InfoTrackSearch.Service.Services
{
  public class SearchFactory
  {
    public static ISearchService GetSearchService(string type)
    {
      ISearchService searchService = null;
      //Simple Factory pattern to create the search services on demand
      if (type.Equals("Google", StringComparison.OrdinalIgnoreCase))
      {
        searchService = new GoogleSearchService();
      }
      else if (type.Equals("Bing", StringComparison.OrdinalIgnoreCase))
      {
        searchService = new BingSearchService();
      }
      return searchService;
    }
  }
}