using Dapper;
using IPedgeProject.Data.AccessData;
using IPedgeProject.Data.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IPedgeProject.Data.Services
{
  public class EmployeeRepository
  {
    private ProjectConnection _dbConnection;
    private ProjectContext _dbContext;
    public EmployeeRepository(ProjectConnection dbConnection, ProjectContext dbContext)
    {
      _dbConnection = dbConnection;
      _dbContext = dbContext;
    }
    public List<Employee> GetAllEmployee()
    {
      // EF Linq GET list
      var employeeList = _dbContext.Employee.ToList<Employee>();
      // Dapper sql GET list 单句sql语句执行获得对象
      var employeeList_sql = _dbConnection.Query<Employee>("select * from dbo.employee");
      return employeeList;
    }
    public async Task<PagedEmployees> GetPagedEmployee(int pageindex, int pagesize)
    {
      //EF 的方法取出包含分页数据的employee集合
      //var employees = new PagedEmployees();
      //employees.TotalCount = _dbContext.Employee.Count();
      //employees.PageIndex = pageindex;
      //employees.PageCount = employees.TotalCount/pagesize;
      //employees.Employees = _dbContext.Employee.OrderBy(u => u.EmployeeID).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<Employee>();

      //使用SP方法并且包含output参数的数据集接收方法
      var param = new DynamicParameters();
      param.Add("@PageSize", pagesize, DbType.Int32, ParameterDirection.Input);
      param.Add("@PageIndex", pageindex, DbType.Int32, ParameterDirection.Input);
      param.Add("@TotalPage", null, DbType.Int32, ParameterDirection.Output);
      param.Add("@TotalRecord", null, DbType.Int32, ParameterDirection.Output);

      var result = (await _dbConnection.StoredProcedure<Employee>("usp_GetPagedEmployee", param)).ToList();
      var employees = new PagedEmployees();
      employees.Employees = result;
      employees.TotalCount = param.Get<int>("@TotalRecord");
      employees.PageIndex = pageindex;
      employees.PageCount = param.Get<int>("@TotalPage");
      //employees.Employees = LstObjToLstDs((List<object>)result[0]);
      return employees;
    }
    public async Task<Employee> GetEmploeebyNumber(int employeeNumber)
    {
      // EF Linq GET object
      var employee = _dbContext.Employee.Single(u => u.EmployeeID == employeeNumber);
      // Dapper sql GET object with param 带参数sql执行获得单个对象
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
    public async void DeleteEmployee(int EmpolyeeNumber)
    {
      // Linq delete function
      // var employee = new Employee { EmployeeID = EmpolyeeNumber };
      // _dbContext.Employee.Attach(employee);
      // _dbContext.Employee.Remove(employee);
      // _dbContext.SaveChanges();

      // SP delete function 执行带参数SP
      var param = new
      {
        EmployeeId = EmpolyeeNumber
      };
      await _dbConnection.StoredProcedure("usp_DeleteEmployee", param);
    }
    public void AddEmployee(Employee employee)
    {
      _dbContext.Employee.Add(employee);
      _dbContext.SaveChanges();
    }
  }
}

