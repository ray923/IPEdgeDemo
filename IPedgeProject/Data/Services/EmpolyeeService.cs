using IPedgeProject.Data.AccessData;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq;

namespace IPedgeProject.Data.Services
{
    public class EmpolyeeService:IEmpolyeeService
    {
        private EmpolyeeDbContext _context;
        public EmpolyeeService(EmpolyeeDbContext context)
        {
            _context = context;
        }
        public List<Employee> GetAllEmployee()
        {
            return _context.Employee.ToList<Employee>();
        }
        public PagedEmployees GetPagedEmployee(int pageindex, int pagesize)
        {
            PagedEmployees employees = new PagedEmployees();
            employees.TotalCount = _context.Employee.Count();
            employees.PageIndex = pageindex;
            employees.PageCount = employees.TotalCount/pagesize;
            employees.Employees = _context.Employee.OrderBy(u=>u.EmployeeID).Skip(pagesize*(pageindex-1)).Take(pagesize).ToList<Employee>();
            return employees;
        }
        public Employee GetEmploeebyNumber(int employeeNumber)
        {
            var employee = _context.Employee.Single(u => u.EmployeeID == employeeNumber);
            return employee;
        }
        public void UpdateEmpolyee(int id, Employee employee)
        {
            employee.EmployeeID = id;
            _context.Employee.Update(employee);
            _context.SaveChanges();
        }
        public void DeleteEmployee(int EmpolyeeNumber)
        {
            var employee = new Employee { EmployeeID = EmpolyeeNumber };
            _context.Employee.Attach(employee);
            _context.Employee.Remove(employee);
            _context.SaveChanges();
        }
        public void AddEmployee(Employee employee)
        {
           _context.Employee.Add(employee);
            _context.SaveChanges();
        }   
    }
}
