using System.Collections.Generic;

namespace IPedgeProject.Data.Services
{
    public interface IEmpolyeeService
    {
        List<Employee> GetAllEmployee();
        PagedEmployees GetPagedEmployee(int pageindex, int pagesize);
        Employee GetEmploeebyNumber(int employeeNumber);
        void UpdateEmpolyee(int EmpolyeeNumber, Employee employee);
        void DeleteEmployee(int EmpolyeeNumber);
        void AddEmployee(Employee employee);
    }
}
