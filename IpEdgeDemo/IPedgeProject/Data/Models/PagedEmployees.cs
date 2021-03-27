using System;
using System.Collections.Generic;

namespace IPedgeProject.Data.Models
{
  public class PagedEmployees
  {
    public List<Employee> Employees { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
  }
}
