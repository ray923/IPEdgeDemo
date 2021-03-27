using System.Collections.Generic;
using InfoTrackSearch.Data.Models;

namespace InfoTrackSearch.Service.Services
{
  public interface ISearchService
  {
    List<SearchResult> GetSearchResults(string keyword);
    List<HitResult> GetHitResults(string url, List<SearchResult> resultList);
  }
}