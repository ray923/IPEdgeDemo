using Microsoft.AspNetCore.Mvc;
using InfoTrackSearch.Data.Models;
using InfoTrackSearch.Service.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace InfoTrackSearch.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class SearchController : ControllerBase
  {
    private readonly string _engineList;
    public SearchController(IConfiguration config)
    {
      _engineList = config[$"SearchEngines"];
    }

    [HttpPost]
    public IActionResult GetHitResults ([FromBody] SearchCondition condition)
    {
      if(string.IsNullOrEmpty(_engineList))
      {
        return BadRequest("No search engine");
      }
      var engingList = _engineList.Split(",");
      var totalResults = new List<HitResultsView>();

      if(string.IsNullOrEmpty(condition.SearchUrl)) return Ok(totalResults);

      foreach (var engine in engingList)
      {
        // Simple Factory pattern to create the search enginge services
        var service = SearchFactory.GetSearchService(engine);
        if(service != null)
        {
          var searchResults = service.GetSearchResults(condition.SearchKeyword);
          var hitResults = service.GetHitResults(condition.SearchUrl, searchResults);
          var HitResultsView = new HitResultsView() {
            SearchEngineName = engine,
            ResultList = hitResults
          };
          totalResults.Add(HitResultsView);
        }
      };
      return Ok(totalResults);
    }
  }
}