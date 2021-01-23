using IPedgeProject.Data.AccessData;
using IPedgeProject.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPedgeProject.Data.Services
{
  public class EmpolyeeService : IEmpolyeeService
  {
    private ProjectConnection _dbConnection;
    private ProjectContext _dbContext;
    private EmployeeRepository _employeeRespository;
    public EmpolyeeService(EmployeeRepository employeeRespository)
    {
      _employeeRespository = employeeRespository;
    }
    public List<Employee> GetAllEmployee()
    {
      return _employeeRespository.GetAllEmployee();
    }
    public async Task<PagedEmployees> GetPagedEmployee(int pageindex, int pagesize)
    {
      return await _employeeRespository.GetPagedEmployee(pageindex, pagesize);
    }
    public async Task<Employee> GetEmploeebyNumber(int employeeNumber)
    {
      return await _employeeRespository.GetEmploeebyNumber(employeeNumber);
    }
    public void UpdateEmpolyee(int id, Employee employee)
    {
      _employeeRespository.UpdateEmpolyee(id, employee);
    }
    public async void DeleteEmployee(int empolyeeNumber)
    {
      _employeeRespository.DeleteEmployee(empolyeeNumber);
    }
    public void AddEmployee(Employee employee)
    {
      _employeeRespository.AddEmployee(employee);
    }
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
