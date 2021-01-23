using System.Collections.Generic;
using IPedgeProject.Data.Models;

namespace IPedgeProject.Data.Services
{
  public class ServicesUtils
  {
    public ServicesUtils()
    {

    }
    //TO DO 把oject对象转为实体类的通用方法
    public List<Employee> LstObjToLstDs(List<object> obj)
    {
      List<Employee> list = new List<Employee>();
      foreach (object ob in obj)
      {
        Employee lstob = (Employee)ob;//强制转化为实体List
        list.Add(lstob);
      }
      return list;
    }
  }
}
