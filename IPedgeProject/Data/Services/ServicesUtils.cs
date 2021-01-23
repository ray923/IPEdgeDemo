using System.Collections.Generic;
using IPedgeProject.Data.Models;

namespace IPedgeProject.Data.Services
{
    public class ServicesUtils
    {
        public ServicesUtils()
        {

        }
        public List<Employee> LstObjToLstDs(List<object> obj)
        {
            List<Employee> list = new List<Employee>();
            foreach (object ob in obj)
            {
                Employee lstob=(Employee) ob;//强制转化为实体List
                list.Add(lstob);
            }
            return list;
        }
    }
}
