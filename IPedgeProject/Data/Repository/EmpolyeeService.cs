using IPedgeProject.Data.AccessData;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPedgeProject.Data.Services
{
    public class EmpolyeeService:IEmpolyeeService
  {
    private ProjectConnection _dbConnection;
    private ProjectContext _dbContext;
        public EmpolyeeService(ProjectConnection dbConnection, ProjectContext dbContext)
        {
            _dbConnection = dbConnection;
            _dbContext = dbContext;
        }
        public List<Employee> GetAllEmployee()
    {
            // EF Linq GET list
            var employeeList = _dbContext.Employee.ToList<Employee>();
            // Dapper sql GET list
            var employeeList_sql = _dbConnection.Query<Employee>("select * from dbo.employee");
            return employeeList;
        }
        public PagedEmployees GetPagedEmployee(int pageindex, int pagesize)
        {
            PagedEmployees employees = new PagedEmployees();
            employees.TotalCount = _dbContext.Employee.Count();
            employees.PageIndex = pageindex;
            employees.PageCount = employees.TotalCount/pagesize;
            employees.Employees = _dbContext.Employee.OrderBy(u=>u.EmployeeID).Skip(pagesize*(pageindex-1)).Take(pagesize).ToList<Employee>();
            return employees;
        }
        public async Task<Employee> GetEmploeebyNumber(int employeeNumber)
        {
            // EF Linq GET object
            var employee = _dbContext.Employee.Single(u => u.EmployeeID == employeeNumber);
            // Dapper sql GET object with param
            var param = new
            {
                id = employeeNumber
            };
            var employee_sql = await _dbConnection.QuerySingle<Employee>("select * from dbo.employee where EmployeeId = @id", param);
            return employee;
        }
        public void UpdateEmpolyee(int id, Employee employee)
        {
            employee.EmployeeID = id;
            _dbContext.Employee.Update(employee);
            _dbContext.SaveChanges();
        }
        public void DeleteEmployee(int EmpolyeeNumber)
        {
            var employee = new Employee { EmployeeID = EmpolyeeNumber };
            _dbContext.Employee.Attach(employee);
            _dbContext.Employee.Remove(employee);
            _dbContext.SaveChanges();
        }
        public void AddEmployee(Employee employee)
        {
            _dbContext.Employee.Add(employee);
            _dbContext.SaveChanges();
        }   
    }
}
