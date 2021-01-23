using System.Collections.Generic;
using System.Threading.Tasks;
using IPedgeProject.Data.Models;

namespace IPedgeProject.Data.Services
{
  public interface IEmpolyeeService
  {
    List<Employee> GetAllEmployee();
    Task<PagedEmployees> GetPagedEmployee(int pageindex, int pagesize);
    Task<Employee> GetEmploeebyNumber(int employeeNumber);
    void UpdateEmpolyee(int EmpolyeeNumber, Employee employee);
    void DeleteEmployee(int EmpolyeeNumber);
    void AddEmployee(Employee employee);
  }
}
