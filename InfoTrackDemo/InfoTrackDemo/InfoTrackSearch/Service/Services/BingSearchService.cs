using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InfoTrackSearch.Data.Models;

namespace InfoTrackSearch.Service.Services
{
  public class BingSearchService: ISearchService
  {
    //Not using keyowrd as the static web page, but get the value from the front end
    public List<SearchResult> GetSearchResults(string keyword)
    {
      var pageIndex = new string[]{"01","02","03","04","05","06","07","08","09","10"};
      var elementList = new List<string>();
      for (int i = 0; i <= pageIndex.Length -1; i++)
      {
        //Get the whole html contetn as response from the url
        var result = GetSearchContent(string.Format("https://infotrack-tests.infotrack.com.au/Bing/Page{0}.html", pageIndex[i]));
        elementList.AddRange(SplitResultBlock(result.Result.ToString()));
        
        //Only need first 50 results, remove others. If less than 50 results, continue to get next page till the end.
        if(elementList.Count == 50)
        {
          break;
        }
        if (elementList.Count > 50)
        {
          elementList = elementList.Take(50).ToList();
          break;
        }
      }
      var resultList = new List<SearchResult>();

      //To find the title and content block in each result's position.
      elementList.ForEach( element => {
        var searchResult = new SearchResult();
        searchResult.ResultContent = element;
        searchResult.TitleIndex = element.IndexOf("</cite>");
        resultList.Add(searchResult);
      });
      return resultList;
    }

    public List<HitResult> GetHitResults(string url, List<SearchResult> resultList)
    {
      var hitLists = new List<HitResult>();
      if (string.IsNullOrEmpty(url)) return hitLists;
      //Calculate the result's order.
      int i = 1;
      // To find whether the input url from the front end contain in the each result, and find it appears position.
      resultList.ForEach(element => {
        // Ignore the case of the letter.
        var keywordIndex = element.ResultContent.IndexOf(url, System.StringComparison.OrdinalIgnoreCase);
        var hitResult = new HitResult();
        //If contain the url, get the position, if not contain skip to next one
        if(keywordIndex > 0)
        {
          if(keywordIndex <= element.TitleIndex)
          {
            hitResult.ResultPlace = "Bing Title";
          }
          else if(keywordIndex > element.TitleIndex)
          {
            hitResult.ResultPlace = "Bing Brief Content";
          }
          else{}
        }
        hitResult.ResultOrder = i;
        hitLists.Add(hitResult);
        i ++;
      });
      return hitLists;
    }
    
    //Get the html content for the static web page.
    private Task<string> GetSearchContent (string url)
    {
      var client = new HttpClient();
      var response = client.GetAsync(url);
      response.Result.EnsureSuccessStatusCode();
      return response.Result.Content.ReadAsStringAsync();
    }

    //Remove the unnecessary content, and split the real result in to list.
    private List<string> SplitResultBlock (string content)
    {
      int startTag = content.IndexOf("<ol id=");
      int endTag =  content.IndexOf("</ol>");
      int startTagLength = "<ol id=".Length;
      var resultsBlock = content.Substring(startTag+startTagLength,endTag-startTag);
      var blockStartTag = @"<li class=";
      var resultList = resultsBlock
        .Split(blockStartTag,System.StringSplitOptions.RemoveEmptyEntries)
        .Where(s=> (s.Contains("b_algo") && s.Contains("b_caption")))
        .ToList();
      return resultList;
    }
  }
}