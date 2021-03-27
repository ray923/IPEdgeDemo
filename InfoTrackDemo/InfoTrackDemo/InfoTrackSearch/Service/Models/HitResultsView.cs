using System;
using System.Collections.Generic;

namespace InfoTrackSearch.Data.Models
{
  public class HitResultsView
  {
    public string SearchEngineName {get; set;}
    public List<HitResult> ResultList {get; set;}
  }
}